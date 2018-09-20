using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Suitable;

[XLua.Hotfix]
[XLua.LuaCallCSharp]
public class S_CB : MonoBehaviour {
    RectTransform rect = null;
    public bool mOffset = false;
    public bool mScale = false;
    void Start()
    {

        rect = GetComponent<RectTransform>();
        Suitable.SDEFINE.Suit(rect, mScale, 0);

    }

}
