using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//----------------------------------------------
//            UGUI: kit[通用cd组件]
// Copyright © sf.sz liugr
//----------------------------------------------
[XLua.Hotfix]
[XLua.LuaCallCSharp]
public class UGUIImageProg : MonoBehaviour {
    private Image mImage = null;
    private RectTransform mTrans = null;
    private float mTime = 0f;
    private float mTotalTime = 0f;
    public bool InCD { get; private set; }
    Action mCallBack;

    private float mWidth = 0;

    void Awake () {
        mImage = GetComponent<Image>();
        mTrans = GetComponent<RectTransform>();
        mWidth = mTrans.sizeDelta.x;
    }

    public void SetCD(float usedTime, float totalTime, Action func = null)
    {
        if (mImage == null)
        {
            mImage = GetComponent<Image>();
        }
        if (mTrans == null)
        {
            mTrans = GetComponent<RectTransform>();
        }

        InCD = true;
        mTime = usedTime;
        mTotalTime = totalTime;
        float left = (totalTime - mTime) / totalTime;
        mTrans.sizeDelta = new Vector2(mWidth * left, mTrans.sizeDelta.y);
        
        if (func != null)
        {
            mCallBack = func;
        }
    }

	void Update () {
        if (!InCD) return;
        mTime += Time.deltaTime;
        if (mTime >= mTotalTime)
        {
            InCD = false;
            mTime = 0f;
            mTrans.sizeDelta = new Vector2(mWidth, mTrans.sizeDelta.y);

            if (mCallBack != null)
            {
                mCallBack();
            }
        }
        else
        {
            float left = 0;
            left = mTime / mTotalTime;
            mTrans.sizeDelta = new Vector2(mWidth * left, mTrans.sizeDelta.y);
        }
    }

    private void OnDestroy()
    {
        mImage = null;
        mTime = 0f;
    }
}
