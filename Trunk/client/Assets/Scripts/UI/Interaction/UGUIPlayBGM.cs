using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[AddComponentMenu("UGUI/Interaction/UGUIPlayBGM")]
public class UGUIPlayBGM : MonoBehaviour {
    public AudioSource mSound = null;

    public void PlayBGM()
    {
        //if (mSound != null)
        //{
        //    if (PlayerPrefsManager.Instance != null)
        //    {
        //        mSound.volume = PlayerPrefsManager.Instance.MusicVolume;
        //    }
            
        //    mSound.Play();
        //}
    }

    public void StopBGM()
    {
        //if (mSound != null)
        //{
        //    if (PlayerPrefsManager.Instance != null)
        //    {
        //        mSound.volume = PlayerPrefsManager.Instance.MusicVolume;
        //    }

        //    mSound.Stop();
        //}
    }
}
