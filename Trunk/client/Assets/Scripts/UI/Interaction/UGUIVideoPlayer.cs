using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

[XLua.Hotfix]
[XLua.LuaCallCSharp]

public class UGUIVideoPlayer : MonoBehaviour
{
    private VideoPlayer mVideo = null;
    private AudioSource mAudioSource = null;
    private VoidDelegate mComplete = null;
    private VideoPlayer.EventHandler mloopPointReachedCallback = null;
    private Camera mUICamera = null;
    private double mPlayTime;

    private void Awake()
    {
        mVideo = GetComponent<VideoPlayer>();
        mAudioSource = GetComponent<AudioSource>();
        mUICamera = GameObject.FindGameObjectWithTag("UICamera").GetComponent<Camera>();
    }

    public void SetClip(string path, VoidDelegate completeCallback = null)
    {
        if (mVideo == null)
        {
            mVideo = GetComponent<VideoPlayer>();
        }
        if (mUICamera == null)
        {
            GameObject.FindGameObjectWithTag("UICamera").GetComponent<Camera>();
        }
        mComplete = completeCallback;

        mUICamera.cullingMask = 2097152;
        mVideo.targetCamera = mUICamera;

        IResManager.Instance.LoadVideo(path, LoadVideoComplete, false);
    }

    public void SetUIClip(string path, VoidDelegate completeCallback = null)
    {
        if (mVideo == null)
        {
            mVideo = GetComponent<VideoPlayer>();
        }

        IResManager.Instance.LoadVideo(path, LoadVideoComplete, true);
    }

    public void SetComplete()
    {
        mUICamera.cullingMask = 32;
    }

    private void VideoPlayerEventHandler(VideoPlayer source)
    {
        if (mloopPointReachedCallback != null)
        {
            mloopPointReachedCallback.Invoke(source);
        }
        SetComplete();
    }

    public void SetloopPointReached(VideoPlayer.EventHandler loopPointReachedCallback, bool add = true)
    {
        if (loopPointReachedCallback != null)
        {
            if (add)
            {
                mloopPointReachedCallback = loopPointReachedCallback;
                mVideo.loopPointReached += VideoPlayerEventHandler;
            }
            else
            {
                mVideo.loopPointReached -= VideoPlayerEventHandler;
                mloopPointReachedCallback = null;
            }
        }
    }

    private void LoadVideoComplete(Object o, object arg)
    {
        if (o == null)
        {
            return;
        }

        mVideo.clip = o as VideoClip;
        mVideo.playOnAwake = (bool)arg;
        if (mAudioSource != null)
        {
            mVideo.audioOutputMode = VideoAudioOutputMode.AudioSource;
            mVideo.SetTargetAudioSource(mVideo.audioTrackCount, mAudioSource);
        }

        mVideo.Prepare();
        mVideo.prepareCompleted += OnVideoPrepareCompleted;
    }

    void OnVideoPrepareCompleted(VideoPlayer source)
    {
        if (source == null)
        {
            return;
        }

        source.Play();
        if (mComplete != null)
        {
            mComplete.Invoke();
        }
    }

    private void OnApplicationPause(bool pause)
    {
        if (mVideo == null)
        {
            return;
        }

        // 暂停时记录视频播放时间
        if (pause)
        {
            mPlayTime = mVideo.time;
        }
        // 恢复时继续上一次播放时间
        else
        {
            mVideo.time = mPlayTime;
            mVideo.Play();
        }
    }

    public void Stop()
    {
        if (mVideo == null)
        {
            return;
        }
        //mVideo.time = 0;
        //mVideo.Stop();
       
       //mVideo.Play();
        mVideo.Stop();
    }
}
