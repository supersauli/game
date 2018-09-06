using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Security.Cryptography;
using UnityEditor;
public class LoadResourceManage {

    // Use this for initialization
    private Dictionary<string, Object> _objGroupDic = new Dictionary<string, Object>();
    public Object LoadResource(string name) {
        var res = GetResource(name);
        if (res != null) {
            return res;
        }
        //res = Resources.LoadAssetAtPath(name,typeof(Object));
        res =  Resources.Load(name);
        //res = AssetDatabase.LoadAssetAtPath(name, typeof(Object));
        if (res != null) {
            AddResource(name,res);
        }
        return res;
    }

    public void AddResource(string name,Object obj) {
        if (obj == null) {
            return;   
        }
        if (_objGroupDic.ContainsKey(name)) {
            return;
        }
        _objGroupDic.Add(name,obj);
        //MD5 md5 = MD5.Create();
        //var hashRet =  md5.ComputeHash(System.Text.Encoding.Default.GetBytes(name));
        //Hash128 hash = Hash128.Parse(name);
    }

    public  Object GetResource(string name) {
        if (_objGroupDic.ContainsKey(name)) {
            return _objGroupDic[name];
        }
        return null;
    }
	
}
