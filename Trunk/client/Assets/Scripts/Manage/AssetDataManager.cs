using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class AssetDataManager : IResManager {

    void Awake()
    {
        Instance = this;
        mLoadQueue = new List<LoadTaskInfo>();

    }
    public class LoadTaskInfo
    {
        public LoadTaskComplete callBackComplete;
        public LoadTaskProgress callBackProgress;
        public int mIndex = 0;
        public List<string> list;
    }

    private List<LoadTaskInfo> mLoadQueue = null;//加载任务队列
    private LoadTaskInfo mCurrentLoadTaskInfo = null;

    public override void Prepare(PrepareComplete cb, PrepareError cbError)
    {
        cb.Invoke();
    }

    public override void LoadUIAsset(string filePath, LoadComplete cb, object obj = null, string extend = IResExtend.Prefab)
    {
        if (cb == null)
        {
            return;
        }

        if (string.IsNullOrEmpty(filePath))
        {
            cb(null, obj);

            return;
        }

        Object o = null;
        if (CacheManager.Instance != null)
        {
            if (CacheManager.Instance.InUICache(filePath))
            {
                o = CacheManager.Instance.GetUICacheObject(filePath);
                if (o != null)
                {
                    cb(o, obj);
                    return;
                }
            }
        }

#if UNITY_EDITOR
        o = AssetDatabase.LoadAssetAtPath("Assets/AssetData/" + filePath + extend, typeof(Object));
#endif
        if (o == null)//没有加载成功 可能是低配高配导致 也可能是资源缺失
        {
            LogManager.Instance.LogError("res null:", filePath);
            return;
        }

        if (CacheManager.Instance != null)
        {
            CacheManager.Instance.AddUICacheObject(filePath, o);
        }

        cb.Invoke(o, obj);
        o = null;
    }

    /// <summary>
    /// 单资源加载
    /// </summary>
    /// <param name="filePath"></param>
    /// <param name="cb"></param>
    /// <param name="obj"></param>
    public override void LoadAsset(string filePath, LoadComplete cb, object obj = null, string extend = IResExtend.Prefab,bool cacheObj = true)
    {
        if (cb == null)
        {
            return;
        }

        if (string.IsNullOrEmpty(filePath))
        {
            cb(null, obj);

            return;
        }

        Object o = null;
        if (CacheManager.Instance != null)
        {
            if (CacheManager.Instance.InCache(filePath))
            {
                o = CacheManager.Instance.GetCacheObject(filePath);
                if (o != null)
                {
                    cb(o, obj);
                    return;
                }
            }
        }

#if UNITY_EDITOR
        if (extend == IResExtend.Png || extend == IResExtend.Jpg)
        {
            o = AssetDatabase.LoadAssetAtPath("Assets/AssetData/"+filePath + extend, typeof(Sprite));
        }
        else
        {
            o = AssetDatabase.LoadAssetAtPath("Assets/AssetData/"+filePath + extend, typeof(Object));
        }
        
#endif
        if (o == null)//没有加载成功 可能是低配高配导致 也可能是资源缺失
        {
            LogManager.Instance.LogError("res null:", filePath);
            cb(null, obj);
            return;
        }
        if (cacheObj)
        {
            if (CacheManager.Instance != null)
            {
                CacheManager.Instance.AddCacheObject(filePath, o);
            }
        }

        cb.Invoke(o, obj);
        o = null;
    }
    public override void LoadScene(string sceneName, LoadSceneComplete cb)
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);
        cb.Invoke(asyncOperation);
    }

    public override void UnLoadScene(string sceneName)
    {
        //base.UnLoadScene(sceneName);
    }

    /// <summary>
    /// 加载任务队列[只允许加载prefab]
    /// </summary>
    /// <param name="filePathList"></param>
    /// <param name="cbComplete"></param>
    /// <param name="cbProgress"></param>
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
        if (mLoadQueue.Count <= 0) return;
        mCurrentLoadTaskInfo = mLoadQueue[0];
        mLoadQueue.RemoveAt(0);
    }

    /// <summary>
    /// 每帧执行单个加载
    /// </summary>
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
                    UnityEngine.Object o = null;
#if UNITY_EDITOR
                    o = AssetDatabase.LoadAssetAtPath("Assets/AssetData/" + filePath + IResExtend.Prefab, typeof(Object));
#endif
                    if (o == null)//没有加载成功 可能是低配高配导致 也可能是资源缺失
                    {
                        LogManager.Instance.LogError("res null:", filePath);
                    }

                    mCurrentLoadTaskInfo.mIndex += 1;
                    CacheManager.Instance.AddCacheObject(filePath, o);
                }

                float progress = (mCurrentLoadTaskInfo.mIndex + 0f) / (mCurrentLoadTaskInfo.list.Count + 0f);
                if (mCurrentLoadTaskInfo.callBackProgress != null)
                {
                    mCurrentLoadTaskInfo.callBackProgress.Invoke(progress);
                }
                
            }
        }
    }

    /// <summary>
    /// 卸载
    /// </summary>
    /// <param name="filePath"></param>
    public override void UnLoad(string filePath)
    {

    }

    /// <summary>
    /// 队列卸载
    /// </summary>
    /// <param name="list"></param>
    public override void UnLoad(List<string> list)
    {

    }

    public override void SetLuaData()
    {

    }

    public override string LoadLua(string key)
    {
        //TextAsset text = Resources.Load(GameTools.StringBuilder("Lua/Game/UI/", key, ".lua")) as TextAsset;
        //LogManager.Instance.LogError(GameTools.StringBuilder("Assets/AssetData/Lua/Game/UI/", key, ".lua.txt"));
        TextAsset text = null;
#if UNITY_EDITOR
        string path = GameTools.StringBuilder("Assets/AssetData/Lua/UI/", key, ".lua.txt");
        
        text = AssetDatabase.LoadAssetAtPath(path,typeof(TextAsset)) as TextAsset;
        if (text == null)
        {
            LogManager.Instance.LogError("lua error:", path);
        }
#endif
        return text.text;
    }
}
