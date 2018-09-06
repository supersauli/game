using UnityEngine;
using System.Collections.Generic;

public class ResourcesManager : IResManager 
{
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

        o = Resources.Load(filePath);
        if (o == null)//没有加载成功 可能是低配高配导致 也可能是资源缺失
        {
            LogManager.Instance.LogError("res null:", filePath);
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
        if (extend == IResExtend.Png|| extend == IResExtend.Jpg)
        {
            o = Resources.Load<Sprite>(filePath);
        }
        else
        {
            o = Resources.Load(filePath, typeof(Object));
        }
        
        if (o == null)//没有加载成功 可能是低配高配导致 也可能是资源缺失
        {
            LogManager.Instance.LogError("res null:", filePath);
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

    
    /// <summary>
    /// 加载任务队列
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
                    UnityEngine.Object o = Resources.Load(filePath);
                    if (o == null)//没有加载成功 可能是低配高配导致 也可能是资源缺失
                    {
                        LogManager.Instance.LogError("res null:", filePath);
                    }

                    mCurrentLoadTaskInfo.mIndex += 1;
                    CacheManager.Instance.AddCacheObject(filePath, o);
                }
               
                float progress = (mCurrentLoadTaskInfo.mIndex + 0f) / (mCurrentLoadTaskInfo.list.Count + 0f);
                mCurrentLoadTaskInfo.callBackProgress.Invoke(progress);
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
        string path = GameTools.StringBuilder("Lua/Game/UI/", key, ".lua");
        TextAsset text = Resources.Load(path) as TextAsset;
        if (text == null)
        {
            LogManager.Instance.LogError("lua error:", path);
        }
        return text.text;
    }
}