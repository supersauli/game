using UnityEngine;
using System.Collections;
using DG.Tweening;
//----------------------------------------------
//            UGUI: kit
// Copyright © sf.sz liugr
//----------------------------------------------
[XLua.Hotfix]
[XLua.LuaCallCSharp]
public class UGUITweenRotation : MonoBehaviour
{
    public enum LoopType
    {
        Once,
        Loop
    }

    public LoopType looptype = LoopType.Once;
    public Ease easeType = Ease.Linear;
    public Vector3 from;
    public Vector3 to;
    public float duration = 1f;
    // 是否在onenable的时候开始执行
    public bool enableActive = false;

    //private bool f = true;
    RectTransform rect = null;

    void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    void Reset()
    {
        from = GetComponent<RectTransform>().localEulerAngles;
        to = GetComponent<RectTransform>().localEulerAngles;
        //Debug.LogError(from);
    }
    void Start()
    {
        if (!enableActive)
        {
            Rotate(to);
        }
    }

    private void OnEnable()
    {
        if (enableActive)
        {
            Rotate(to);
        }
    }

    void Rotate(Vector3 _to)
    {
        Tweener tweener = rect.DOLocalRotate(_to, duration, RotateMode.LocalAxisAdd).OnComplete(OnComplete);
        tweener.SetEase(easeType);
        
    }
    private void OnComplete()
    {
        if (looptype == LoopType.Loop)
        {
            //f = true;
            rect.localEulerAngles = from;
            rect.DOKill();
            
            Rotate(to);
        }

        else if (looptype == LoopType.Once)
        {
            //f = false;
        }
    }
}
