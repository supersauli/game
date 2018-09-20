using UnityEngine;
using System.Collections;
using DG.Tweening;
//----------------------------------------------
//            UGUI: kit
// Copyright © sf.sz liugr
//----------------------------------------------
[XLua.Hotfix]
[XLua.LuaCallCSharp]
public class UGUITweenScale : MonoBehaviour
{
    public enum LoopType
    {
        Once,
        Loop,
        PingPong,
    }

    public LoopType looptype = LoopType.Once;
    public Ease easeType = Ease.Linear;
    public Vector3 from;
    public Vector3 to;
    public float duration = 1f;
    public bool play = true;
    private bool f = true;
    public bool enable{
        set {
            f = value;
        }
        get {
            return f;
        }
    }

    RectTransform rect = null;
    Tweener tweener = null;
    public void Reset()
    {
        Pause();
        GetComponent<RectTransform>().localScale = from;
        Scale(to);
    }
    void Awake()
    {
        rect = GetComponent<RectTransform>();
        if (play)
        {
            Scale(to);
        }
        
    }
    private void OnDestroy()
    {
        rect = null;
        tweener = null;
    }
    public void Scale(Vector3 _to)
    {
        tweener = rect.DOScale(_to, duration).OnComplete(OnComplete);
        tweener.SetEase(easeType);
    }
    public void Resume()
    {
        Scale(to);
    }

    public void Pause()
    {
        if (tweener != null)
        {
            tweener.Kill();
        }
    }
    private void OnComplete()
    {
        if (looptype == LoopType.Loop)
        {
            f = true;
            rect.localScale = from;
            Scale(to);
        }
        else if (looptype == LoopType.PingPong)
        {
            if (f)
            {
                Scale(from);
                f = false;
            }
            else
            {
                Scale(to);
                f = true;
            }
        }
        else if (looptype == LoopType.Once)
        {
            f = false;
        }
    }
}
