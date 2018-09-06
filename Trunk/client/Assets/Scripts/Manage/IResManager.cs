using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
/// <summary>
/// 资源处理器接口
/// </summary>
//[XLua.CSharpCallLua]
public delegate void ResUpdateDelegate();
//[XLua.CSharpCallLua]
public delegate void PrepareComplete();
//[XLua.CSharpCallLua]
public delegate void PrepareError();
//[XLua.CSharpCallLua]
public delegate void LoadComplete(Object o, object obj);
//[XLua.CSharpCallLua]
public delegate void LoadTaskComplete();
//[XLua.CSharpCallLua]
public delegate void LoadTaskProgress(float progress);

public delegate void LoadSceneComplete(AsyncOperation asyncOper);



public class IResManager : MonoBehaviour
{
    public static IResManager Instance = null;

    void Awake()
    {
        if (GlobalData.mDebug)
        {

#if UNITY_EDITOR
            gameObject.AddComponent<AssetDataManager>();
            //gameObject.AddComponent<ResourcesManager>();
#elif UNITY_STANDALONE_WIN||UNITY_ANDROID_TEST
            gameObject.AddComponent<ResourcesManager>();
#endif

        }
        else
        {
            gameObject.AddComponent<AssetsBundleManager>();
        }
    }

    public virtual void Prepare(PrepareComplete cb, PrepareError error)//加载前准备 manifest数据读取
    {

    }

    public virtual void LoadAsset(string filePath, LoadComplete cb, object obj = null, string extend = IResExtend.Prefab,bool cacheObj = true)//加载单个资源
    {

    }

    public virtual void LoadUIAsset(string filePath, LoadComplete cb, object obj = null, string extend = IResExtend.Prefab)//加载UI单个资源
    {

    }

    public virtual void LoadTask(List<string> filePathList, LoadTaskComplete cbComplete, LoadTaskProgress cbProgress)//加载任务队列
    {

    }
    public virtual void LoadScene(string sceneName, LoadSceneComplete cb)//加载场景
    {

    }

    public virtual void UnLoadScene(string sceneName)
    {
        //SceneManager.UnloadSceneAsync(sceneName);
    }
    public void LoadVideo(string filePath, LoadComplete cb, object obj = null)
    {
        Object o = null;
        o = Resources.Load(filePath);
        if (o == null)//没有加载成功 可能是低配高配导致 也可能是资源缺失
        {
            LogManager.Instance.LogError("res null:", filePath);
        }

        cb.Invoke(o, obj);
        o = null;

    }
    
    public virtual void SetLuaData()
    {

    }

    public virtual string LoadLua(string key)
    {
        return null;
    }

    public virtual void OnUpdate()
    {

    }

    public virtual void UnLoad(string filePath)
    {

    }

    public virtual void UnLoad(List<string> list)
    {

    }
}

public class IResExtend
{
    public const string Prefab = ".prefab";
    public const string Bytes = ".bytes";
    public const string Lua = ".lua.txt";
    public const string Png = ".png";
    public const string Jpg = ".jpg";
    public const string Asset = ".asset";
    public const string Json = ".json";
    public const string MP3 = ".mp3";
    public const string MP4 = ".mp4";
    public const string RenderTexture = ".renderTexture";
    public const string Mat = ".mat";
    public const string Anim = ".anim";
    public const string Ogg = ".ogg";
}