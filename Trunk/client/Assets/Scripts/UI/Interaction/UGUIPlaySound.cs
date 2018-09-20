using UnityEngine;
using System.Collections;
using System;
//----------------------------------------------
//            UGUI: kit
// Copyright © sf.sz liugr
//----------------------------------------------
[AddComponentMenu("UGUI/Interaction/UGUIPlaySound")]
public class UGUIPlaySound : MonoBehaviour
{
    private static UGUIPlaySound _Instance = null;
    public static UGUIPlaySound Instance
    {
        get { return _Instance; }
    }

    public enum Trigger
    {
        OnClick,
        OnPress,
        OnDragOut,
        onEnter,
        onExit,
        OnEnable,
    }
    public ListenerType ListenerType = ListenerType.Normal;
    [NonSerialized]
    public AudioClip audioClip;
    public string audioName;
    public Trigger trigger = Trigger.OnClick;

    [Range(0f, 1f)]
    public float volume = 1f;
    [Range(0f, 2f)]
    public float pitch = 1f;

	public float delayTime = 0;
    public bool IsEnablePlay = true;
    public bool firstPlay = true;

    //bool mIsOver = false;

    bool canPlay
    {
        get
        {
            if (!enabled) return false;
            return true;
        }
    }
    void Awake() {
        _Instance = this;
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
        lis.sound = this;
    }

    public void OnEnable()
    {
        if (!IsEnablePlay)
        {
            return;
        }

        if (!firstPlay)
        {
            firstPlay = true;
            return;
        }

        if (canPlay && trigger == Trigger.OnEnable)
        {
			if (delayTime > 0) {
				Invoke ("DelayPlay", delayTime);
			} else {
				DelayPlay ();
			}
        }
    }

    public void SetClip(string path)
    {
        IResManager.Instance.LoadAsset(path, LoadVideoComplete, null, IResExtend.Ogg);
    }

    private void LoadVideoComplete(UnityEngine.Object o, object obj)
    {
        if (o == null)
        {
            return;
        }

        audioClip = o as AudioClip;
        Play();
    }

    public void OnClick()
    {
        if (canPlay && trigger == Trigger.OnClick) {
            if (audioClip == null)
            {
                DelayPlay();
            }
            else
            {
                UGUITool.PlaySound(audioClip, volume, pitch);
            }
            
        }
            
    }
    public void OnPress()
    {
        if (canPlay && trigger == Trigger.OnPress)
        {
            if (audioClip == null)
            {
                DelayPlay();
            }
            else
            {
                UGUITool.PlaySound(audioClip, volume, pitch);
            }
        }

    }
    public void OnDragOut()
    {
        if (canPlay && trigger == Trigger.OnDragOut)
        {
            if (audioClip == null)
            {
                DelayPlay();
            }
            else
            {
                UGUITool.PlaySound(audioClip, volume, pitch);
            }
        }

    }
    public void OnEnter()
    {
        if (canPlay && trigger == Trigger.onEnter)
        {
            if (audioClip == null)
            {
                DelayPlay();
            }
            else
            {
                UGUITool.PlaySound(audioClip, volume, pitch);
            }
        }

    }
    public void OnExit()
    {
        if (canPlay && trigger == Trigger.onExit)
        {
            if (audioClip == null)
            {
                DelayPlay();
            }
            else
            {
                UGUITool.PlaySound(audioClip, volume, pitch);
            }
        }
    }

    public void Play() {
        if (audioClip != null) {
			UGUITool.PlaySound (audioClip, volume, pitch);
        }
    }

	private void DelayPlay()
	{
		//UGUITool.PlaySound(audioClip, volume, pitch);
        if (!string.IsNullOrEmpty(audioName))
        {
            SetClip(GameTools.StringBuilder(ResourcesDefine.mUINewSoundPath, audioName));
        }
        
	}
}

