using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Suitable;

[XLua.Hotfix]
[XLua.LuaCallCSharp]
public class S_RT : MonoBehaviour {
    RectTransform rect = null;
    public bool mOffset = false;
    public bool mScale = false;
	void Start()
    {
        rect = GetComponent<RectTransform>();
        int offset = 0;
        if (PluginsManager.Instance.IsIPhoneX())
        {
            if (mOffset)
            {
                offset = Suitable.SDEFINE.PX_RT_OFFSET;
            } 
        }
        Suitable.SDEFINE.Suit(rect,mScale, offset);
    }

}
