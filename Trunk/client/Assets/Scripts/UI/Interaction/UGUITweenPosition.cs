using UnityEngine;
using System.Collections;
using DG.Tweening;
using System.Collections.Generic;
using UnityEngine.UI;
//----------------------------------------------
//            UGUI: kit
// Copyright © sf.sz liugr
//----------------------------------------------

[XLua.Hotfix]
[XLua.LuaCallCSharp]
public class UGUITweenPosition : MonoBehaviour {
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
    public bool StartAwake = true;
    private bool f = true;
    RectTransform rect = null;
    private Tweener tweener;

    private bool setMoving;
    private Vector3 targetTo;

    public bool isMoving
    {
        get
        {
            if (tweener == null) return false;
            return tweener.IsPlaying();
        }
    }

    void Reset()
    {
        from = GetComponent<RectTransform>().localPosition;
        to = GetComponent<RectTransform>().localPosition;
    }
	void Start () {
        rect = GetComponent<RectTransform>();
        if (StartAwake) Move(to);
    }
    public void Move(Vector3 _to) {
        setMoving = true;
        targetTo = _to;
        SetMove();
    }
    private void SetMove() {
        tweener = rect.DOLocalMove(targetTo, duration, true).OnComplete(OnComplete);
        tweener.SetEase(easeType);
    }

    public void Update()
    {
        if (setMoving)
        {
            if (!isMoving)
            {
                SetMove();
            }
        }
    }

    private void OnComplete()
    {
        if (looptype == LoopType.Loop)
        {
            f = true;
            rect.localPosition = from;
            Move(to);
        }
        else if (looptype == LoopType.PingPong)
        {
            if (f)
            {
                Move(from);
                f = false;
            }
            else
            {
                Move(to);
                f = true;
            }
        }
        else if (looptype == LoopType.Once)
        {
            f = false;
        }
        setMoving = false;
    }
}
