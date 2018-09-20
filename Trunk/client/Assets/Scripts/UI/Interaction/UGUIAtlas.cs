using UnityEngine;
using System;
using System.Collections.Generic;

[XLua.Hotfix]
[XLua.LuaCallCSharp]
public class UGUIAtlas : ScriptableObject
{
    [SerializeField]
    public List<string> UVS = null;

    public bool Serialized = false;

    [SerializeField]
    public Dictionary<string, UVData> UVDic = null;

    public void InitSerialize()
    {
        if (!Serialized)
        {
            UVDic = new Dictionary<string, UVData>();
            for (int i = 0; i < UVS.Count; ++i)
            {
                string[] arr = UVS[i].Split(',');
                UVData data = new UVData();
                data.x = int.Parse(arr[1]);
                data.y = int.Parse(arr[2]);
                data.width = int.Parse(arr[3]);
                data.height = int.Parse(arr[4]);
                Vector4 vec = new Vector4(int.Parse(arr[5]), int.Parse(arr[6]), int.Parse(arr[7]), int.Parse(arr[8]));
                data.border = vec;
                UVDic.Add(arr[0], data);
            }
            Serialized = true;
        }
    }
}
[Serializable]
[XLua.Hotfix]
[XLua.LuaCallCSharp]
public class UVData
{
    public int x;
    public int y;
    public int width;
    public int height;
    public Vector4 border; 
}