using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.IO;
using UnityEngine.SceneManagement;
/// <summary>
/// assetbundle资源管理器
/// </summary>
public class AssetsBundleManager : IResManager
{
    /// <summary>
    /// 本地资源目录路径
    /// </summary>
    private string mLocalResRootPath = null;

    /// <summary>
    /// StreamAssets资源路径
    /// </summary>
    private string mStreamResRootPath = null;

    /// <summary>
    /// manifest信息准备结束
    /// </summary>
    private PrepareComplete OnAssetsPrepareComplete = null;

    /// <summary>
    /// manifest信息准备错误
    /// </summary>
    private PrepareError OnAssetsPrepareError = null;

    /// <summary>
    /// 相应的平台主AB路径
    /// </summary>
    private string mMainABPath = null;

    /// <summary>
    /// 根据资源名，得到md5码和路径
    /// </summary>
    private Dictionary<string, VersionAssetData> mNameMD5Dic = null;

    //private Dictionary<string, AssetsBundleData> mAssetsDpDic = null;//资源依赖记录集合

    /// <summary>
    /// ab资源计数器集合
    /// </summary>
    private Dictionary<string, AssetsBundleData> mAssetsABDic = null;

    //private Dictionary<string, List<AssetsLoadedData>> mAssetsLoaded = null;//已经加载的资源集合,包括被依赖的资源

    /// <summary>
    /// 版本信息比对记录信息
    /// </summary>
    private string RVL = "ResVersion.xml";

    /// <summary>
    /// 版本xml尝试加载次数
    /// </summary>
    //private int mIndex = 0;

    private AssetBundle mLuaAssetBundle = null;

    class LoadTaskInfo
    {
        public LoadTaskComplete callBackComplete;
        public LoadTaskProgress callBackProgress;
        public int mIndex = 0;
        public List<string> list;
    }

    /// <summary>
    /// 加载任务队列
    /// </summary>
    private List<LoadTaskInfo> mLoadQueue = null;

    /// <summary>
    /// 队列加载的时候 当前正在加载的任务
    /// </summary>
    private LoadTaskInfo mCurrentLoadTaskInfo = null;

    /// <summary>
    /// AssetBundleManifest 文件 记录ab的信息
    /// </summary>
    private AssetBundleManifest mMainManifest = null;

    void Awake()
    {
        Instance = this;
        mLocalResRootPath = Application.persistentDataPath;
        //mStreamResRootPath = GameTools.StringBuilder(Application.dataPath, "/StreamingAssets");
        mStreamResRootPath = Application.streamingAssetsPath;
        mMainABPath = "StandaloneWindow";
#if UNITY_STANDALONE_WIN || UNITY_EDITOR
        mLocalResRootPath = GameTools.StringBuilder(Application.dataPath, "/../AssetBundle/Local");
        //mStreamResRootPath = GameTools.StringBuilder(Application.dataPath, "/StreamingAssets");
        mStreamResRootPath = Application.streamingAssetsPath;
        mMainABPath = "StandaloneWindow";
#elif UNITY_IPHONE
        mLocalResRootPath =  Application.persistentDataPath;
        //mStreamResRootPath = GameTools.StringBuilder("File:///", Application.dataPath, "/Raw");
        mStreamResRootPath = GameTools.StringBuilder(Application.dataPath, "/Raw");
        //mStreamResRootPath = Application.streamingAssetsPath;
        mMainABPath = "IOS";
#elif UNITY_ANDROID
        mLocalResRootPath = Application.persistentDataPath;
        mStreamResRootPath = GameTools.StringBuilder(Application.dataPath, "!/assets");//同步读取只能这样
        //mStreamResRootPath = GameTools.StringBuilder("jar:File://", Application.dataPath, "!/assets");
        //mStreamResRootPath = Application.streamingAssetsPath;
        mMainABPath = "Android";
#endif
        mAssetsABDic = new Dictionary<string, AssetsBundleData>();

        // 加载队列
        mLoadQueue = new List<LoadTaskInfo>();
    }

    /// <summary>
    /// 解析准备 用于资源更新结束之后调用
    /// </summary>
    /// <param name="cbPrepareComplete">解析准备结束</param>
    /// <param name="cbError"></param>
    public override void Prepare(PrepareComplete cbPrepareComplete, PrepareError cbError)
    {
        OnAssetsPrepareComplete = cbPrepareComplete;
        OnAssetsPrepareError = cbError;
        StartCoroutine(SetMD5AssetsList());
    }

    /// <summary>
    /// 解析资源对应的md5信息
    /// </summary>
    /// <returns></returns>
    IEnumerator SetMD5AssetsList()
    {
        mNameMD5Dic = new Dictionary<string, VersionAssetData>();
        string localPath = mLocalResRootPath + "/" + RVL;
#if UNITY_STANDALONE_WIN || UNITY_EDITOR
        localPath = mLocalResRootPath + "/" + RVL;
#elif UNITY_ANDROID
        localPath = mLocalResRootPath + "/" + RVL;
#elif UNITY_IPHONE
        localPath = mLocalResRootPath + "/" + RVL;
#endif
        //WWW localWWW = new WWW("File://" + localPath);

        if (!File.Exists(localPath))
        {
            Debug.LogError("SetMD5AssetsList file not exist:" + localPath);
            OnAssetsPrepareError.Invoke();
        }
        else
        {
            FileStream fs = new FileStream(localPath, FileMode.Open);
            int fsLen = (int)fs.Length;
            byte[] heByte = new byte[fsLen];
            //int r = fs.Read(heByte, 0, heByte.Length);
            fs.Read(heByte, 0, heByte.Length);
            string text = System.Text.Encoding.UTF8.GetString(heByte);

            XmlDocument document = new XmlDocument();
            document.LoadXml(text);
            XmlNode root = document.SelectSingleNode("root");
            foreach (XmlNode _node in root.ChildNodes)
            {
                XmlElement node = _node as XmlElement;
                if (node == null) { continue; }
                if (node.Name.Equals("asset"))
                {
                    VersionAssetData loadAsset = new VersionAssetData();
                    loadAsset.name = node.GetAttribute("name");
                    loadAsset.md5 = node.GetAttribute("md5");
                    loadAsset.size = System.Convert.ToInt32(node.GetAttribute("size"));
                    loadAsset.path = node.GetAttribute("path");
                    if (!mNameMD5Dic.ContainsKey(loadAsset.name))
                    {
                        mNameMD5Dic.Add(loadAsset.name, loadAsset);
                    }
                }
            }
            yield return StartCoroutine(WWWLoadMainAssets());
            fs.Close();
        }

    }

    private AssetBundle mLastSceneAB = null;
    private AssetBundle mCurrentSceneAB = null;

    public override void LoadScene(string sceneName, LoadSceneComplete cb)
    {
        string scenePath = GameTools.StringBuilder("levels/", sceneName, ".unity");
        VersionAssetData vad = mNameMD5Dic[scenePath];
        string url = GetAssetsFilePath(vad.path, vad.md5);
        AssetBundle ab = AssetBundle.LoadFromFile(url);
        mCurrentSceneAB = ab;
        mLastSceneAB = mCurrentSceneAB;

        AsyncOperation asyncOp = SceneManager.LoadSceneAsync(sceneName);
        cb.Invoke(asyncOp);
    }

    public override void UnLoadScene(string sceneName)
    {
        base.UnLoadScene(sceneName);
        if (mLastSceneAB != null)
        {
            mLastSceneAB.Unload(true);
            mLastSceneAB = null;
        }
    }
    /*
    public override void LoadCGAsset(string filePath, LoadComplete cb, object obj = null, string extend = IResExtend.Prefab)
    {
        filePath = filePath.ToLower();//low ab名称系统默认小写
        if (cb == null)
        {
            return;
        }

        if (string.IsNullOrEmpty(filePath))
        {
            cb(null, obj);
            return;
        }

        LoadAssets(filePath, cb, obj, true, false);
    }
    */
    public override void LoadUIAsset(string filePath, LoadComplete cb, object obj = null, string extend = IResExtend.Prefab)
    {
        filePath = filePath.ToLower();//low ab名称系统默认小写
        if (cb == null)
        {
            return;
        }

        if (string.IsNullOrEmpty(filePath))
        {
            cb(null, obj);
            return;
        }

        UnityEngine.Object o = CacheManager.Instance.GetUICacheObject(filePath);
        if (o != null)
        {
            cb(o, obj);
            return;
        }
        LoadAssets(filePath, cb, obj, true, true);
    }
    /// <summary>
    /// 加载单个资源
    /// </summary>
    /// <param name="filePath">资源路径</param>
    /// <param name="cb">回调</param>
    /// <param name="obj">参数</param>
    public override void LoadAsset(string filePath, LoadComplete cb, object obj = null, string extend = IResExtend.Prefab,bool isCache = true)
    {
        filePath = filePath.ToLower();//low ab名称系统默认小写
        if (cb == null)
        {
            return;
        }

        if (string.IsNullOrEmpty(filePath))
        {
            cb(null, obj);
            return;
        }

        UnityEngine.Object o = CacheManager.Instance.GetCacheObject(filePath);
        if (o != null)
        {
            cb(o, obj);
            return;
        }
        if (extend == IResExtend.Png || extend == IResExtend.Jpg)
        {
            LoadSprites(filePath, cb, obj);
        }
        else
        {
            LoadAssets(filePath, cb, obj, false);
        }
        
    }

    /// <summary>
    /// 队列任务加载
    /// </summary>
    /// <param name="filePathList">任务列表</param>
    /// <param name="cbComplete">完毕回调</param>
    /// <param name="cbProgress">进度回调</param>
    public override void LoadTask(List<string> filePathList, LoadTaskComplete cbComplete, LoadTaskProgress cbProgress)
    {
        LoadTaskInfo loadTask = new LoadTaskInfo();
        loadTask.callBackComplete = cbComplete;
        loadTask.callBackProgress = cbProgress;
        loadTask.list = filePathList;
        mLoadQueue.Add(loadTask);

        if (mCurrentLoadTaskInfo == null)
        {
            SetCurrentTask();
        }
    }

    private void SetCurrentTask()
    {
        if (mLoadQueue.Count <= 0)
        {
            return;
        }

        mCurrentLoadTaskInfo = mLoadQueue[0];
        mLoadQueue.RemoveAt(0);
    }

    public override void OnUpdate()
    {
        if (mCurrentLoadTaskInfo != null)
        {
            if (mCurrentLoadTaskInfo.mIndex >= mCurrentLoadTaskInfo.list.Count)
            {
                mCurrentLoadTaskInfo.callBackComplete.Invoke();
                mCurrentLoadTaskInfo = null;
                SetCurrentTask();
            }
            else
            {
                string filePath = mCurrentLoadTaskInfo.list[mCurrentLoadTaskInfo.mIndex];
                if (CacheManager.Instance.InCache(filePath))
                {
                    mCurrentLoadTaskInfo.mIndex += 1;
                }
                else
                {
                    LoadAsset(filePath, null);
                    mCurrentLoadTaskInfo.mIndex += 1;
                }

                float progress = (mCurrentLoadTaskInfo.mIndex + 0f) / (mCurrentLoadTaskInfo.list.Count + 0f);
                mCurrentLoadTaskInfo.callBackProgress.Invoke(progress);
            }
        }
    }

    /// <summary>
    /// 加载资源
    /// </summary>
    /// <param name="path">路径</param>
    /// <param name="cb"></param>
    /// <param name="obj"></param>
    /// <returns></returns>
    void LoadAssets(string path, LoadComplete cb, object obj, bool isUI = false,bool cacheObj = true)
    {
        if (mAssetsABDic.ContainsKey(path))
        {
            AssetsBundleData abd = mAssetsABDic[path];
            abd.depIndex += 1;
            UnityEngine.Object o = GetObj(abd.ab, path);
            if (cb != null)
            {
                cb.Invoke(o, obj);
            }
        }
        else
        {
            SynLoadAssets(path, false, cb, obj, isUI, cacheObj);
        }
        
    }

    void LoadSprites(string path, LoadComplete cb, object obj)
    {
        if (mAssetsABDic.ContainsKey(path))
        {
            AssetsBundleData abd = mAssetsABDic[path];
            abd.depIndex += 1;
            UnityEngine.Object o = GetSprite(abd.ab, path);
            if (cb != null)
            {
                cb.Invoke(o, obj);
            }
        }
        else
        {
            if (mNameMD5Dic.ContainsKey(path))
            {
                VersionAssetData vad = mNameMD5Dic[path];
                string url = GetAssetsFilePath(vad.path, vad.md5);
                AssetBundle ab = null;
                AssetsBundleData abd = null;
                if (mAssetsABDic.ContainsKey(path))//已经被加载过 计数器自增
                {
                    abd = mAssetsABDic[path];
                    abd.depIndex += 1;
                    ab = abd.ab;
                }
                else
                {
                    // 新加载的
                    ab = AssetBundle.LoadFromFile(url);
                    abd = new AssetsBundleData();
                    abd.name = path;
                    abd.depIndex = 1;
                    abd.ab = ab;

                    mAssetsABDic.Add(path, abd);
                }
                UnityEngine.Object o = GetSprite(abd.ab, path);
                if (cb != null)
                {
                    cb.Invoke(o, obj);
                }
            }
            else
            {
                Debug.LogError("not in md5:"+ path);
            }
        }
    }

    /// <summary>
    /// 加载各个平台的总记录的ab的 manifest信息 这是加载ab的前提
    /// </summary>
    /// <returns></returns>
    IEnumerator WWWLoadMainAssets()
    {
        string url = GetMainABPath();
        VersionAssetData vad = mNameMD5Dic[mMainABPath];
        AssetBundle ab = AssetBundle.LoadFromFile(url);
        yield return null;

        mMainManifest = (AssetBundleManifest)ab.LoadAsset("AssetBundleManifest");
        ab.Unload(false);

        //开始加载任务队列
        if (OnAssetsPrepareComplete != null)
        {
            OnAssetsPrepareComplete();
            OnAssetsPrepareComplete = null;
        }
        else
        {
            LogManager.Instance.LogError("OnAssetsPrepareComplete is null");
        }
    }


    /// <summary>
    /// 真正加载的地方
    /// </summary>
    /// <param name="path"></param>
    /// <param name="dp"></param>
    /// <param name="cb"></param>
    /// <param name="obj"></param>
    /// <returns></returns>

    void SynLoadAssets(string path, bool dp, LoadComplete cb, object obj, bool isUI = false,bool cacheObj = true)
    {
        
        if (mAssetsABDic.ContainsKey(path))
        {
            AssetsBundleData abd = mAssetsABDic[path];
            abd.depIndex += 1;
        }
        else
        {
            if (mNameMD5Dic.ContainsKey(path))
            {
                VersionAssetData vad = mNameMD5Dic[path];
                string url = GetAssetsFilePath(vad.path, vad.md5);
                AssetBundle ab = null;
                if (!dp)
                {
                    // 作为主资源加载的
                    AssetsBundleData abd = null;
                    if (mAssetsABDic.ContainsKey(path))//已经被加载过 计数器自增
                    {
                        abd = mAssetsABDic[path];
                        abd.depIndex += 1;
                        ab = abd.ab;
                    }
                    else
                    {
                        // 新加载的
                        ab = AssetBundle.LoadFromFile(url);
                        abd = new AssetsBundleData();
                        abd.name = path;
                        abd.depIndex = 1;
                        abd.ab = ab;

                        mAssetsABDic.Add(path, abd);
                    }
                    string[] dpAbs = mMainManifest.GetAllDependencies(path);//取这个资源的所有依赖资源
                    #region
                    if (dpAbs.Length == 0)//没有依赖 获取资源
                    {
                        UnityEngine.Object o = GetObj(ab, path);
                        if (isUI)
                        {
                            if (cacheObj)
                            {
                                CacheManager.Instance.AddUICacheObject(path, o);
                            }
                            
                        }
                        else
                        {
                            if (cacheObj)
                            {
                                CacheManager.Instance.AddCacheObject(path, o);
                            }
                                
                        }

                        if (cb != null)
                        {
                            cb.Invoke(o, obj);
                        }

                    }
                    #endregion
                    #region
                    else
                    {
                        //存在依赖资源
                        #region    
                        for (int i = 0; i < dpAbs.Length; i++)
                        {
                            if (mAssetsABDic.ContainsKey(dpAbs[i]))
                            {
                                // 依赖计数器中已经存在了 计数器+1
                                abd = mAssetsABDic[dpAbs[i]];
                                abd.depIndex += 1;
                            }
                            else
                            {
                                SynLoadAssets(dpAbs[i], true, cb, obj, isUI);
                            }
                        }
                        #endregion

                        //依赖都加载完毕了 再看主资源
                        UnityEngine.Object o = GetObj(ab, path);

                        if (isUI)
                        {
                            if (cacheObj)
                            {
                                CacheManager.Instance.AddUICacheObject(path, o);
                            }
                            
                        }
                        else
                        {
                            if (cacheObj)
                            {
                                CacheManager.Instance.AddCacheObject(path, o);
                            }
                                
                        }

                        if (cb != null)
                        {
                            cb.Invoke(o, obj);
                        }
                    }
                }
                #endregion
                else
                {
                    // 作为被依赖资源下载
                    AssetsBundleData abd = null;
                    if (mAssetsABDic.ContainsKey(path))
                    {
                        abd = mAssetsABDic[path];
                        abd.depIndex += 1;
                    }
                    else//被依赖的 缓存
                    {
                        ab = AssetBundle.LoadFromFile(url);
                        abd = new AssetsBundleData();
                        abd.name = path;
                        abd.depIndex = 1;
                        abd.ab = ab;

                        mAssetsABDic.Add(path, abd);
                    }
                }
            }
            else
            {
                LogManager.Instance.LogError("cannot find:", path);
            }
        }
    }
    /// <summary>
    /// 取出一个assets
    /// </summary>
    /// <param name="ab"></param>
    /// <param name="path"></param>
    /// <returns></returns>
    private UnityEngine.Object GetObj(AssetBundle ab, string path)
    {
        string name = ComTool.GetABNameByPath(path);
        return ab.LoadAsset(name);
    }
    private Object GetSprite(AssetBundle ab, string path)
    {
        string name = ComTool.GetABNameByPath(path);
        return ab.LoadAsset(name, typeof(Sprite));
    }


    /// <summary>
    /// 卸载
    /// </summary>
    /// <param name="filePath"></param>
    public override void UnLoad(string filePath)
    {
        StartCoroutine(DisposeDPAB(filePath));
        //DisposeDPAB(filePath);
    }

    /// <summary>
    /// 批量卸载
    /// </summary>
    /// <param name="list"></param>
    public override void UnLoad(List<string> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            UnLoad(list[i]);
        }
    }

    /// <summary>
    /// 迭代unload资源加载的ab
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    
    IEnumerator DisposeDPAB(string filePath)
    {

        if (mAssetsABDic.ContainsKey(filePath))
        {
            AssetsBundleData abd = mAssetsABDic[filePath];

            abd.depIndex -= 1;
            if (abd.depIndex <= 0)
            {
                if (abd.ab != null)
                {
                    abd.ab.Unload(false);
                    abd.ab = null;
                }
                mAssetsABDic.Remove(filePath);
                abd = null;
            }
        }

        // 取这个资源的所有依赖资源
        string[] dpAbs = mMainManifest.GetAllDependencies(filePath);
        
        if (dpAbs.Length > 0)
        {
            for (int i = 0; i < dpAbs.Length; i++)
            {
                if (!dpAbs[i].Contains("_"))
                {
                    
                    yield return StartCoroutine(DisposeDPAB(dpAbs[i]));
                }
            }
        }
    }
    
    /// <summary>
    /// 获取md5对应的真实名字
    /// </summary>
    /// <param name="path"></param>
    /// <param name="md5"></param>
    /// <returns></returns>
    private string GetAssetsFilePath(string path, string md5)
    {
        string url = string.Empty;
        if (string.IsNullOrEmpty(path))
        {
            url = GameTools.StringBuilder(mLocalResRootPath, "/", md5);
        }
        else
        {
            url = GameTools.StringBuilder(mLocalResRootPath, "/", path, "/", md5);
        }

        if (File.Exists(url))
        {
            return url;
        }
        else
        {
            if (string.IsNullOrEmpty(path))
            {
                if (GlobalData.isiPhone)
                {
                    url = GameTools.StringBuilder( mStreamResRootPath, "/", md5);
                }
                else if (GlobalData.isAndroid)
                {
                    url = GameTools.StringBuilder("jar:file://", mStreamResRootPath, "/", md5);
                }
                
            }
            else
            {
                if (GlobalData.isiPhone)
                {
                    url = GameTools.StringBuilder(mStreamResRootPath, "/", path, "/", md5);
                }
                else if (GlobalData.isAndroid)
                {
                    url = GameTools.StringBuilder("jar:file://", mStreamResRootPath, "/", path, "/", md5);
                }

                
            }
            return url;
        }
    }

    /// <summary>
    /// 获取主ab资源地址
    /// </summary>
    /// <returns></returns>
    private string GetMainABPath()
    {
        string url = string.Empty;
        VersionAssetData vad = mNameMD5Dic[mMainABPath];
        if (File.Exists(GameTools.StringBuilder(mLocalResRootPath, "/", vad.md5)))
        {
#if UNITY_STANDALONE_WIN || UNITY_EDITOR
            url = GameTools.StringBuilder(mLocalResRootPath, "/", vad.md5);
#elif UNITY_ANDROID
            url = GameTools.StringBuilder(mLocalResRootPath, "/", vad.md5);
#elif UNITY_IPHONE
            url = GameTools.StringBuilder(mLocalResRootPath, "/", vad.md5);
#endif
            return url;
        }
        else
        {
            if (GlobalData.isiPhone)
            {
                url = GameTools.StringBuilder( mStreamResRootPath, "/", vad.md5);
            }
            else if (GlobalData.isAndroid)
            {
                url = GameTools.StringBuilder("jar:file://", mStreamResRootPath, "/", vad.md5);
            }
            
            return url;
        }
    }

    public override void SetLuaData()
    {
        string path = "luascript";
        if (mNameMD5Dic.ContainsKey(path))
        {
            VersionAssetData vad = mNameMD5Dic[path];
            string url = GetAssetsFilePath(vad.path, vad.md5);
            mLuaAssetBundle = AssetBundle.LoadFromFile(url);
        }
    }

    public override string LoadLua(string key)
    {
        //LogManager.Instance.Log("LoadLua:", GameTools.StringBuilder(key, ".lua"));
        TextAsset text = mLuaAssetBundle.LoadAsset(GameTools.StringBuilder(key, ".lua")) as TextAsset;
        if (text == null)
        {
            return "";
        }
        return text.text;
    }
}