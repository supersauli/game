using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[XLua.Hotfix]
[XLua.LuaCallCSharp]
public class UGUITweenFill : MonoBehaviour {

    private Image mImage = null;
    private float mTotalTime = 0f;
    private float mLeftTime = 0f;
    private bool mPlaying = false;

    public bool mIsCD = true;
	void Awake () {
        Init();

    }

    private void Init()
    {
        if (mImage == null)
        {
            mImage = transform.GetComponent<Image>();
        }
    }
    public void ShowCDImage(bool show)
    {
        if (mImage != null)
        {
            mImage.gameObject.SetActive(show);
        }
    }
    public void Play(float totalTime,float leftTime)
    {
        Init();
        mTotalTime = totalTime;
        mLeftTime = leftTime;
        mPlaying = true;
        
    }
    public void Stop()
    {
        mPlaying = false;
    }
	
	void Update () {
        if (!mPlaying) return;
        mLeftTime -= Time.unscaledDeltaTime;
        float fill = mLeftTime / mTotalTime;

        //LogManager.Instance.LogError(fill);
        if (mIsCD)
        {
            mImage.fillAmount = fill;
        }
        else
        {
            mImage.fillAmount = 1f - fill;
        }
        
    }
}
