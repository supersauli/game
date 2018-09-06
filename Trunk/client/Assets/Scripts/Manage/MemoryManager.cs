using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 根据引用计时以及引用计数和机器内存进行自动化内存回收
/// </summary>
public class MemoryManager : MonoBehaviour
{
    //private float UNLOAD_TIME_300 = 300f;
    //private float UNLOAD_TIME_1200 = 1200f;

    //private int UNLOAD_ALL_MEMORY = 100;//剩余内存小于100M 直接释放所有
    //private int UNLOAD_MEMORY_300 = 300;//300M剩余内存以上 那么只有最近20分钟之内都没有被引用的将会释放 其他都不释放

    /*
    void Awake()
    {
        
        return;//这里暂时先不用了 好像异步处理有些问题
        float releaseTime = UNLOAD_TIME_300;
        float availableMemory = Mathf.FloorToInt(PluginsManager.Instance.GetAvailableMemorySize());
        if (availableMemory >= UNLOAD_MEMORY_300)
        {
            releaseTime = UNLOAD_TIME_1200;

        }
        else if (availableMemory < UNLOAD_MEMORY_300 && availableMemory > UNLOAD_ALL_MEMORY)//100-300M 5分钟未被引用的资源则释放
        {
            releaseTime = UNLOAD_TIME_300;
        }
        else//100M以内  释放所有除UI资源之外的资源
        {
            releaseTime = 0f; 
        }
        
        Dictionary<string, CacheObjectData> CacheObjectDic = CacheManager.Instance.mCacheObjectDic;

        List<string> keys = new List<string>();

        foreach (KeyValuePair<string, CacheObjectData> kp in CacheObjectDic)
        {
            CacheObjectData cod = kp.Value;
            string path = kp.Key;

            if (Time.realtimeSinceStartup - cod.time > releaseTime)
            {
                keys.Add(path);
            }
        }
        for (int i = 0; i < keys.Count; ++i)
        {
            CacheObjectData cod = CacheObjectDic[keys[i]];
            CacheObjectDic.Remove(keys[i]);
            cod.obj = null;
            cod = null;
            //LogManager.Instance.LogError("unload:", keys[i]);
            //IResManager.Instance.UnLoad(keys[i]);
        }
        if (keys.Count > 0)
        {
            StartCoroutine(IDispose(keys));
        }
        
    }
    */
    /*
    IEnumerator IDispose(List<string> keys)
    {
        LoadingManager.Instance.DisposingAB = true;
        while (keys.Count > 0)
        {
            string path = keys[0];
            keys.RemoveAt(0);
            IResManager.Instance.UnLoad(path);
            yield return null;
        }
        LoadingManager.Instance.DisposingAB = false;
        GameTools.GameGC();
    }
    */
}