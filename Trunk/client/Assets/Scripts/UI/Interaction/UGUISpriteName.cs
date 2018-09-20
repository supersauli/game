using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[XLua.Hotfix]
[XLua.LuaCallCSharp]
public class UGUISpriteName : MonoBehaviour
{
    public UGUIAtlas mAtlas;
    private Image mImage;
    public string GroupName;
    private Dictionary<string, Sprite> mCache = null;
    private string mSpriteName;
    // Use this for initialization
    void Awake()
    {
        if (mImage == null)
        {
            mImage = GetComponent<Image>();
            mCache = new Dictionary<string, Sprite>();
        }
        /*
        if (!mAtlas.Serialized)
        {
            mAtlas.InitSerialize();
        }
        */
    }

    public string SpriteName
    {
        set
        {
            mSpriteName = value;
            SetSpriteName(value);
        }
        get
        {
            return mSpriteName;
        }
    }

    private void SetSpriteName(string name)
    {
        /*
        if (!mAtlas.Serialized)
        {
            mAtlas.InitSerialize();
        }
        */

        bool groupIsNull = string.IsNullOrEmpty(GroupName);

        if (mImage == null)
        {
            mImage = GetComponent<Image>();

            if (groupIsNull)
            {
                mCache = new Dictionary<string, Sprite>();
            }
            
        }

        if (mImage == null || mImage.sprite == null ||  mImage.sprite.name == name)
        {
            return;
        }

        if (groupIsNull)
        {
            if (mCache.ContainsKey(name))
            {
                mImage.sprite = mCache[name];
                return;
            }

            Texture2D t = mImage.sprite.texture;

            //UVData uvs = AtlasManager.Instance.GetUVData(mAtlas, name);
            //if (uvs != null)
            //{
            //    Rect rect = new Rect(uvs.x, uvs.y, uvs.width, uvs.height);
            //    Vector2 pivot = new Vector2(0.5f, 0.5f);
            //    Vector4 border = uvs.border;
            //    mImage.sprite = Sprite.Create(t, rect, pivot, 100.0f, 0, SpriteMeshType.Tight, border);
            //    mCache.Add(name, mImage.sprite);
            //}
        }
        else
        {
            //AsyncLoadSpriteManager.RemoveCallback(GroupName, name, OnSetSpriteCall);
            //AsyncLoadSpriteManager.AddNextCall(GroupName, name, OnSetSpriteCall, mImage.sprite.texture, mAtlas);
        }
        
        
        
        
        /*
        for (int i = 0; i < mAtlas.UVS.Count; i++)
        {
            if (mAtlas.UVS[i].StartsWith(name))
            {
                string[] arr = mAtlas.UVS[i].Split(',');
                if (arr[0] == name)
                {
                    //Rect rect = new Rect(int.Parse(arr[1]), t.height - int.Parse(arr[2]) - int.Parse(arr[4]), int.Parse(arr[3]), int.Parse(arr[4]));
                    Rect rect = new Rect(int.Parse(arr[1]), int.Parse(arr[2]), int.Parse(arr[3]), int.Parse(arr[4]));
                    Vector2 pivot = new Vector2(0.5f, 0.5f);
                    Vector4 border = new Vector4(int.Parse(arr[5]), int.Parse(arr[6]), int.Parse(arr[7]), int.Parse(arr[8]));
                    mImage.sprite = Sprite.Create(t, rect, pivot, 100.0f, 0, SpriteMeshType.Tight, border);
                    mCache.Add(name, mImage.sprite);
                    break;
                }
            }
        }
        */
    }

    private void OnSetSpriteCall(Sprite sp)
    {
        if (sp != null)
        {
            mImage.sprite = sp;
        }
        else
        {

        }
        
    }

    private void OnDestroy()
    {
        if (!string.IsNullOrEmpty(GroupName))
        {
            //AsyncLoadSpriteManager.Clear(GroupName);
        }

        if (mImage != null)
        {
            mImage.sprite = null;
        }

        if (mCache != null)
        {
            mCache.Clear();
            mCache = null;
        }
    }
}
