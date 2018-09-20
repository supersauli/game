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
public class UGUIImageCD : MonoBehaviour {
    private Image mImage = null;
    private float mTime = 0f;
    private float mTotalTime = 0f;
    public bool InCD { get; private set; }
    private bool mFront = false;
    Action mCallBack;

    void Awake () {
        mImage = GetComponent<Image>();

    }

    public void SetCD(float usedTime, float totalTime, bool front = false, Action func = null)
    {
        if (mImage == null)
        {
            mImage = GetComponent<Image>();
        }
        InCD = true;
        mTime = usedTime;
        mTotalTime = totalTime;
        float left = (totalTime - mTime) / totalTime;
        mImage.fillAmount = left;
        mFront = front;
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

            if (mFront)
            {
                mImage.fillAmount = 1f;
            }
            else
            {
                mImage.fillAmount = 0f;
            }

            if (mCallBack != null)
            {
                mCallBack();
            }
        }
        else
        {
            float left = 0;
            if (mFront)
            {
                left = mTime / mTotalTime;
            }
            else
            {
                left = (mTotalTime - mTime) / mTotalTime;
            }
            mImage.fillAmount = left;
        }
    }

    private void OnDestroy()
    {
        mImage = null;
        mTime = 0f;
    }
}
