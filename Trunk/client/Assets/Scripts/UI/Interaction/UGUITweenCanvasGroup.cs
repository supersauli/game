using UnityEngine;
using System.Collections;
using DG.Tweening;
//----------------------------------------------
//            UGUI: kit
// Copyright © sf.sz liugr
//----------------------------------------------
[XLua.Hotfix]
[XLua.LuaCallCSharp]
[RequireComponent(typeof(CanvasGroup))]
public class UGUITweenCanvasGroup : MonoBehaviour
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
    public float Delay;

    private float value;
    private VoidDelegate OnCompleteCallBack;
    private VoidDelegate OnUpdateCallBack;

    public float duration = 1f;
    private Tweener mTween;

    private bool f = true;
    private CanvasGroup group;

    RectTransform rect = null;
    void Reset()
    {
        if(rect == null) rect = GetComponent<RectTransform>();
        from = rect.offsetMax.y;
        to = rect.offsetMax.y;
    }
    void Start()
    {
        group = GetComponent<CanvasGroup>();
        rect = GetComponent<RectTransform>();
        if(PlayAwake)StartTween(from, to);
    }

    public bool isPlaying() {
        if (mTween != null) {
            return mTween.IsPlaying();
        }
        return false;
    }
    public void StartTween(float _from,float _to,VoidDelegate overcallback = null, VoidDelegate updatecallback = null)
    {
        group.alpha = _from;
        value = _from;
        mTween = DOTween.To(() => value, x => value = x, _to, duration).OnComplete(OnComplete);
        mTween.SetDelay(Delay);

        mTween.OnUpdate(OnValuesUpdate);
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
        StartTween(from, to, callback, updatecallback);
    }
    public void PlayBack( VoidDelegate callback = null, VoidDelegate updatecallback = null)
    {
        StartTween(to, from, callback, updatecallback);
    }


    private void OnValuesUpdate()
    {
        if (rect != null) {
            group.alpha = value;
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
            StartTween(from, to);
        }
        else if (looptype == LoopType.PingPong)
        {
            if (f)
            {
                StartTween(from, to);
                f = false;
            }
            else
            {
                StartTween(from, to);
                f = true;
            }
        }
        else if (looptype == LoopType.Once)
        {
            f = false;
        }
    }
}
