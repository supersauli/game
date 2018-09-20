using UnityEngine;
using System.Collections;
using DG.Tweening;
//----------------------------------------------
//            UGUI: kit
// Copyright © sf.sz liugr
//----------------------------------------------
[XLua.Hotfix]
[XLua.LuaCallCSharp]
public class UGUITweenTop : MonoBehaviour
{
    public enum LoopType
    {
        Once,
        Loop,
        PingPong,
    }

    public LoopType looptype = LoopType.Once;
    public Ease easeType = Ease.Linear;
    public float from;
    public float to;
    public bool PlayAwake = true;

    private float value;
    private VoidDelegate OnCompleteCallBack;
    private VoidDelegate OnUpdateCallBack;
    public float Delay;

    public float duration = 1f;
    private Tweener mTween;

    private bool f = true;
    RectTransform rect = null;
    void Reset()
    {
        if(rect == null) rect = GetComponent<RectTransform>();
        from = rect.offsetMax.y;
        to = rect.offsetMax.y;
    }
    void Start()
    {
        rect = GetComponent<RectTransform>();
        if(PlayAwake)TweenTop(from, to);
    }

    public bool isPlaying() {
        if (mTween != null) {
            return mTween.IsPlaying();
        }
        return false;
    }
    public void TweenTop(float _from,float _to,VoidDelegate overcallback = null, VoidDelegate updatecallback = null)
    {
        value = _from;

        mTween = DOTween.To(() => value, x => value = x, _to, duration).OnComplete(OnComplete);
        mTween.OnUpdate(OnValuesUpdate);
        mTween.SetDelay(Delay);
        mTween.SetEase(easeType);
        if (overcallback != null) {
            OnCompleteCallBack = overcallback;
        }
        if (updatecallback != null)
        {
            OnUpdateCallBack = updatecallback;
        }
    }
    
    public void Play( VoidDelegate callback = null, VoidDelegate updatecallback = null)
    {
        TweenTop(from, to, callback, updatecallback);
    }
    public void PlayBack( VoidDelegate callback = null, VoidDelegate updatecallback = null)
    {
        TweenTop(to, from, callback, updatecallback);
    }


    private void OnValuesUpdate()
    {
        if (rect != null) {
            var ofs = rect.offsetMax;
            ofs.y = -value;
            rect.offsetMax = ofs;
        }
        if (OnUpdateCallBack != null)
        {
            OnUpdateCallBack.Invoke();
        }
        
    }

    private void OnComplete()
    {
        if (OnCompleteCallBack != null) {
            OnCompleteCallBack.Invoke();
        }
        if (looptype == LoopType.Loop)
        {
            f = true;
            value = from;
            TweenTop(from, to);
        }
        else if (looptype == LoopType.PingPong)
        {
            if (f)
            {
                TweenTop(from, to);
                f = false;
            }
            else
            {
                TweenTop(from, to);
                f = true;
            }
        }
        else if (looptype == LoopType.Once)
        {
            f = false;
        }
    }
}
