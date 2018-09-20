using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;
//----------------------------------------------
//            UGUI: kit
// Copyright © sf.sz liugr
//----------------------------------------------
[AddComponentMenu("UGUI/Interaction/UGUIButtonScale")]
public class UGUIButtonScale : MonoBehaviour
{
    public ListenerType ListenerType = ListenerType.Normal;
    public Image tweenTarget;
    public Vector3 hover = new Vector3(1.1f, 1.1f, 1.1f);
    public Vector3 pressed = new Vector3(1.05f, 1.05f, 1.05f);
    public float duration = 0.15f;
    private Vector3 originalScale;

    //Vector3 mScale;
    bool mStarted = false;

    void Awake() {
        UGUIEventListener lis = gameObject.GetComponent<UGUIEventListener>();
        if (lis == null)
        {
            if (ListenerType == ListenerType.Extend)
            {
                lis = gameObject.AddComponent<UGUIEventListenerEx>();
            }
            else 
            {
                lis = gameObject.AddComponent<UGUIEventListener>();
            }
        }
        lis.bs = this;

        originalScale = transform.localScale;
    }
    void Start()
    {
        if (!mStarted)
        {
            mStarted = true;
            if (tweenTarget == null) tweenTarget = gameObject.GetComponent<Image>();
            //mScale = tweenTarget.GetComponent<RectTransform>().localScale;
        }
    }
    void OnPress()
    {
        if (enabled)
        {
            if (!mStarted) Start();
            
            tweenTarget.rectTransform.DOScale(pressed, duration).SetEase(Ease.Linear);
            
        }
    }
    void OnUp()
    {
        if (enabled)
        {
            if (!mStarted) Start();
            tweenTarget.rectTransform.DOScale(originalScale, duration).SetEase(Ease.Linear);
        }
    }
    void OnEnter()
    {
        if (enabled)
        {
            if (!mStarted) Start();
            tweenTarget.rectTransform.DOScale(hover, duration).SetEase(Ease.Linear);
        }
    }
    void OnExit()
    {
        if (enabled)
        {
            if (!mStarted) Start();
            tweenTarget.rectTransform.DOScale(originalScale, duration).SetEase(Ease.Linear);
        }
    }
}

