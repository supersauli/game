using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.UI;
//----------------------------------------------
//            UGUI: kit
// Copyright © sf.sz liugr
//----------------------------------------------
[XLua.Hotfix]
[XLua.LuaCallCSharp]
public class UGUITweenColor : MonoBehaviour
{
    public enum LoopType
    {
        Once,
        Loop,
        PingPong,
    }

    public LoopType looptype = LoopType.Once;
    public Ease easeType = Ease.Linear;
    public Color32 from;
    public Color32 to;
    public float duration = 1f;

    private bool f = true;
    Image image = null;
    public bool autoPlay = true;
    private bool stop = false;
    private bool reset = true;

    Tweener tweener = null;

    public void Reset()
    {
        Pause();
        GetComponent<Image>().color = from;
        Color(to);
    }
    void Awake()
    {
        //Image image = null;image.do
        if (image == null)
        {
            image = GetComponent<Image>();
        }

        if (autoPlay)
        {
            Color(to);
        }
    }
    void Color(Color32 _to)
    {
        stop = false;
        image.color = from;
        tweener = image.DOColor(_to, duration).OnComplete(OnComplete);
        tweener.SetEase(easeType);
    }

    private void OnDisable()
    {
        image.color = from;
    }
    public void Revert()
    {
        stop = false;
        tweener = image.DOColor(from, duration).OnComplete(OnComplete);
        tweener.SetEase(easeType);
    }

    public void Pause()
    {
        if (tweener != null)
        {
            tweener.Kill();
        }
    }
    public void DoColor()
    {
        if (image == null)
        {
            image = GetComponent<Image>();
        }
        Color(to);
    }
    public void Stop(bool _reset = true)
    {
        reset = _reset;
        if (reset)
        {
            image.color = from;
            stop = true;
        }
    }
    private void OnComplete()
    {
        if (stop)
        {
            if (reset)
            {
                image.color = from;
            }
            return;
        } 
        if (looptype == LoopType.Loop)
        {
            f = true;
            image.color = from;
            Color(to);
        }
        else if (looptype == LoopType.PingPong)
        {
            if (f)
            {
                Color(from);
                f = false;
            }
            else
            {
                Color(to);
                f = true;
            }
        }
        else if (looptype == LoopType.Once)
        {
            f = false;
        }
    }

    private void OnDestroy()
    {
        tweener = null;
    }
}
