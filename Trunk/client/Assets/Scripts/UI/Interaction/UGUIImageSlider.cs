using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//----------------------------------------------
//            UGUI: kit[通用带九宫格的slider组件]
// Copyright © sf.sz liugr
//----------------------------------------------
[XLua.Hotfix]
[XLua.LuaCallCSharp]
public class UGUIImageSlider : MonoBehaviour {
    private RectTransform rect = null;
    private float mWidth = 0f;
    private float mHeight = 0f;


    private float mOldWidth = 0f;
    private float mNewWidth = 0f;
    private float mSumWidth = 0f;
    private float mGoWidth = 0f;

    private bool mIsStart = false;
    private float mWidthTime = 0f;
    private System.Action mCallBack = null;

    void Awake()
    {
        rect = GetComponent<RectTransform>();
        mWidth = rect.sizeDelta.x;
        mHeight = rect.sizeDelta.y;
    }

    public void SetSlider(float slider)
    {
        if (rect == null)
        {
            rect = GetComponent<RectTransform>();
            mWidth = rect.sizeDelta.x;
            mHeight = rect.sizeDelta.y;
        }

        float width = mWidth * slider;
        rect.sizeDelta = new Vector2(width, mHeight);
    }

    public void SetSliderByAnimator(int oldLevel, int newLevel, float oldSlider, float newSlider, float widthTime, System.Action func = null)
    {
        if (rect == null)
        {
            Awake();
        }

        mOldWidth = mWidth * oldSlider;
        mNewWidth = mWidth * newSlider;
        mWidthTime = widthTime / 1000;
        mGoWidth = 0f;
        mCallBack = func;

        if (newSlider < oldSlider)
        {
            mSumWidth = mWidth * (1 - oldSlider + newSlider);
        }
        else
        {
            mSumWidth = mWidth * (newSlider - oldSlider);
        }

        if (newLevel > oldLevel)
        {
            mSumWidth += mWidth * (newLevel - oldLevel - 1);
        }

        rect.sizeDelta = new Vector2(mOldWidth, mHeight);
        mIsStart = true;
    }

    private void Update()
    {
        if (!mIsStart)
        {
            return;
        }

        mGoWidth += Time.deltaTime * (mWidth / mWidthTime);
        if (mGoWidth >= mSumWidth)
        {
            mGoWidth = 0f;
            mIsStart = false;
            rect.sizeDelta = new Vector2(mNewWidth, mHeight);

            if (mCallBack != null)
            {
                mCallBack();
            }
            return;
        }

        float width = 0;
        int someTimes = (int)((mOldWidth + mGoWidth) / mWidth);
        if (someTimes > 0)
        {
            width = (mOldWidth + mGoWidth) - mWidth * someTimes;
        }
        else
        {
            width = mOldWidth + mGoWidth;
        }
        rect.sizeDelta = new Vector2(width, mHeight);
    }
}
