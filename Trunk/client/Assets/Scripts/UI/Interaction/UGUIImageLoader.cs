using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
//----------------------------------------------
//            UGUI: kit
// Copyright © sf.sz liugr
//----------------------------------------------

[XLua.Hotfix]
[XLua.LuaCallCSharp]
public class UGUIImageLoader : MonoBehaviour
{
    private Image mImage = null;
    private string mImagePath = string.Empty;
    //Vector2 pivot = Vector2.one * 0.5f;

    public static Dictionary<string, Sprite> mCache = new Dictionary<string, Sprite>();

    public static void UnLoad()
    {
        mCache.Clear();
    }

    private void Awake()
    {
        mImage = GetComponent<Image>();
    }

    public Image GetImage()
    {
        return mImage;
    }

    //适应加载的图片大小
    public void AdaptImageSize()
    {
        if (mImage == null)
        {
            return;
        }

        mImage.SetNativeSize();
    }

    public static void AddCache(string path, Sprite sp)
    {

        if (!mCache.ContainsKey(path))
        {
            mCache.Add(path, sp);
        }

    }
    public void SetImagePath(string path, VoidDelegate callback = null)
    {
        if (mCache == null)
        {
            mCache = new Dictionary<string, Sprite>();
        }

        if (mImage == null)
        {
            mImage = GetComponent<Image>();
        }
        if (mImagePath != path)
        {
            mImagePath = path;
        }
        else
        {
            return;
        }

        if (mCache.ContainsKey(path))
        {
            if (mCache[path] == null || mCache[path].name.Equals("null"))
            {
                LogManager.Instance.LogError("UGUIImageLoader SetImagePath() cache is null !!!!, path = ", mImagePath);
            }

            mImage.overrideSprite = mCache[path];


            if (callback != null)
            {
                callback.Invoke();
            }
        }
        else
        {
            //print(path);
            IResManager.Instance.LoadAsset(path, LoadModelComplete, new object[] { path, callback }, IResExtend.Png);
        }
    }

    private void LoadModelComplete(Object o, object obj)
    {
        if (o == null)
        {
            LogManager.Instance.LogError("UGUIImageLoader LoadModelComplete() o is null!!!!", mImagePath);

            return;
        }

        if (obj == null)
        {
            LogManager.Instance.LogError("UGUIImageLoader LoadModelComplete() obj is null!!!!");

            return;
        }

        object[] objs = (object[])obj;
        //string path = (string)objs[0];
        VoidDelegate callback = objs[1] as VoidDelegate;
        if (callback != null)
        {
            callback.Invoke();
        }

        //print(o);
        //Texture2D t2d = o as Texture2D;
        mImage.sprite = o as Sprite;
        
        /*Texture2D t2d = o as Texture2D;
        Sprite sp = Sprite.Create(t2d, new Rect(0, 0, t2d.width, t2d.height), pivot);
        aimImageSize.x = t2d.width;
        aimImageSize.y = t2d.height;
        if (sp == null)
        {
            LogManager.Instance.LogError("Sprite.Create Failed!!!! Path:", path);
        }

        if (!mCache.ContainsKey(path))
        {
            mCache.Add(path, sp);
        }
        
        mImage.overrideSprite = sp;*/
    }

    private void OnDestroy()
    {
        mImage = null;
    }
}