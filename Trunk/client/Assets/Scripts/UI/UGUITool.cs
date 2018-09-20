using UnityEngine;
using System.Collections;
//----------------------------------------------
//            UGUI: kit
// Copyright © sf.sz liugr
//----------------------------------------------
[XLua.Hotfix]
[XLua.LuaCallCSharp]
public static class UGUITool  {

    static AudioListener mListener;
    static bool mLoaded = false;
    static float mGlobalVolume = 1f;
    static private float soundVolume
    {
        get
        {
            if (!mLoaded)
            {
                mLoaded = true;
                mGlobalVolume = PlayerPrefs.GetFloat("Sound", 1f);
            }
            return mGlobalVolume;
        }
        set
        {
            if (mGlobalVolume != value)
            {
                mLoaded = true;
                mGlobalVolume = value;
                PlayerPrefs.SetFloat("Sound", value);
            }
        }
    }
    static public AudioSource PlaySound(AudioClip clip) { return PlaySound(clip, 1f, 1f); }

    /// <summary>
    /// Play the specified audio clip with the specified volume.
    /// </summary>

    static public AudioSource PlaySound(AudioClip clip, float volume) { return PlaySound(clip, volume, 1f); }

    static float mLastTimestamp = 0f;
    static AudioClip mLastClip;

    /// <summary>
    /// Play the specified audio clip with the specified volume and pitch.
    /// </summary>
    //public static bool MUTE = false;
    static public AudioSource PlaySound(AudioClip clip, float volume, float pitch)
    {
        //Debug.LogError(111);
        //if (GameSoundManager.Instance != null)
        //{
        //    if (GameSoundManager.Instance.MuteAudio) return null;
        //}

        ////Debug.LogError(222);
        //float time = Time.time;
        //if (mLastClip == clip && mLastTimestamp + 0.1f > time) return null;
        ////Debug.LogError(333);
        //mLastClip = clip;
        //mLastTimestamp = time;
        ////volume *= soundVolume;
        //if(PlayerPrefsManager.Instance!=null)
        //{
        //    volume *= PlayerPrefsManager.Instance.AudioVolume;
        //}

        //if (!GameSceneManager.Instance.IsFightScene)//不是战斗场景
        //{
        //    //判断玩家状态
        //    if (UnitObjectManager.Instance.mRoleObject != null)
        //    {
        //        if (UnitObjectManager.Instance.mRoleObject.DataObject.QueryPropInt(ClientConstDefine.LogicState) != 0)//战斗状态
        //        {
        //            volume *= GameSoundManager.Instance.UISoundDel;
        //        }
        //    }

        //}
        //else//战斗场景
        //{
        //    volume *= GameSoundManager.Instance.UISoundDel;
        //}

        if (clip != null && volume > 0.01f)
        {
          //  GameSoundManager.Instance.AudioS.PlayOneShot(clip, volume);
            /*if (mListener == null || !UGUITool.GetActive(mListener))
            {
                AudioListener[] listeners = GameObject.FindObjectsOfType(typeof(AudioListener)) as AudioListener[];

                if (listeners != null)
                {
                    for (int i = 0; i < listeners.Length; ++i)
                    {
                        if (UGUITool.GetActive(listeners[i]))
                        {
                            mListener = listeners[i];
                            break;
                        }
                    }
                }

                if (mListener == null)
                {
                    Camera cam = Camera.main;
                    if (cam == null) cam = GameObject.FindObjectOfType(typeof(Camera)) as Camera;
                    if (cam != null) mListener = cam.gameObject.AddComponent<AudioListener>();
                }
            }

            if (mListener != null && mListener.enabled && UGUITool.GetActive(mListener.gameObject))
            {
#if UNITY_4_3 || UNITY_4_5 || UNITY_4_6
				AudioSource source = mListener.audio;
#else
                AudioSource source = mListener.GetComponent<AudioSource>();
#endif
                if (source == null) source = mListener.gameObject.AddComponent<AudioSource>();
#if !UNITY_FLASH
                source.priority = 50;
                source.pitch = pitch;
#endif
                source.PlayOneShot(clip, volume);
                
                //Debug.LogError("play");
                return source;
            }*/
        }
        return null;
    }

    [System.Diagnostics.DebuggerHidden]
    [System.Diagnostics.DebuggerStepThrough]
    static public bool GetActive(Behaviour mb)
    {
        return mb && mb.enabled && mb.gameObject.activeInHierarchy;
    }

    [System.Diagnostics.DebuggerHidden]
    [System.Diagnostics.DebuggerStepThrough]
    static public bool GetActive(GameObject go)
    {
        return go && go.activeInHierarchy;
    }
}
