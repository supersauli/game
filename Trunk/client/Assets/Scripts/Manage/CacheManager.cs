using UnityEngine;
using System.Collections.Generic;

public delegate void InstantiateComplete(GameObject go);
public class CacheObjectData
{
    public string path;
    public float time;
    public Object obj;
}
/// <summary>
/// 内存镜像缓存管理
/// </summary>
public class CacheManager : MonoBehaviour
{
    public Dictionary<string, CacheObjectData> mCacheObjectDic = new Dictionary<string, CacheObjectData>();

    private Dictionary<string, Object> mUICacheDic = new Dictionary<string, Object>();//UI镜像资源永远不销毁

    // 300秒未使用的Object销毁。
    //private float releaseTime = 300.0f;

    private static CacheManager _Instance = null;
    public static CacheManager Instance
    {
        get { return _Instance; }
    }

    private void Awake()
    {
        _Instance = this;

        //TimerManager.Instance.AddRepeatTimer("CheckMemory", 10.0f, -1, CheckMemory, null);
        //Application.lowMemory += OnApplicationLowMemory;
    }
    
    public void OnUpdate()
    {

    }

    /// <summary>
    /// 加入UI镜像缓存
    /// </summary>
    /// <param name="path"></param>
    /// <param name="o"></param>
    public void AddUICacheObject(string path, UnityEngine.Object o)
    {
        if (o == null || mUICacheDic.ContainsKey(path))
        {
            return;
        }

        mUICacheDic.Add(path, o);
    }


    /// <summary>
    /// 加入普通镜像缓存
    /// </summary>
    /// <param name="path"></param>
    /// <param name="o"></param>
    public void AddCacheObject(string path, UnityEngine.Object o)
    {
        if (o == null || mCacheObjectDic.ContainsKey(path))
        {
            return;
        }
        CacheObjectData cod = new CacheObjectData();
        cod.path = path;
        cod.time = Time.realtimeSinceStartup;
        cod.obj = o;
        mCacheObjectDic.Add(path, cod);
    }

    public bool InUICache(string path)
    {
        bool b = false;
        if (mUICacheDic.ContainsKey(path))
        {
            b = true;
        }

        return b;
    }

    public bool InCache(string path)
    {
        bool b = false;
        if (mCacheObjectDic.ContainsKey(path))
        {
            b = true;
        }

        return b;
    }

    /// <summary>
    /// 取一个缓存对象
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public Object GetUICacheObject(string path)
    {
        Object o = null;
        if (string.IsNullOrEmpty(path))
        {
            return o;
        }

        if (mUICacheDic.ContainsKey(path))
        {
            return mUICacheDic[path];
        }

        return o;
    }

    public Object GetCacheObject(string path)
    {
        Object o = null;
        if (string.IsNullOrEmpty(path))
        {
            return o;
        }

        if (mCacheObjectDic.ContainsKey(path))
        {
            CacheObjectData cod = mCacheObjectDic[path];
            cod.time = Time.realtimeSinceStartup;
            return cod.obj;
        }

        return o;
    }

    public void ChangeCacheTime(string path)
    {
        if (mCacheObjectDic.ContainsKey(path))
        {
            CacheObjectData cod = mCacheObjectDic[path];
            cod.time = Time.realtimeSinceStartup;
        }
    }

    public void Clear()
    {
        foreach (KeyValuePair<string, CacheObjectData> kvp in mCacheObjectDic)
        {
            CacheObjectData cod = kvp.Value;
            if (cod == null)
            {
                continue;
            }

            cod.obj = null;
            cod = null;
        }

        mCacheObjectDic.Clear();

        GameTools.GameGC();
    }

    private void CheckMemory(object[] args)
    {
        if (GameTools.MemoryWarning())
        {
            Clear();
        }
    }

    private void OnDestroy()
    {
        Application.lowMemory -= OnApplicationLowMemory;
    }

    private void OnApplicationLowMemory()
    {
        Clear();
    }
}