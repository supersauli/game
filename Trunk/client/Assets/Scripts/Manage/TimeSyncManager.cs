using UnityEngine;
using System.Collections;
using System;

using SysUtils;

/// <summary>
/// 服务器时间同步类
/// </summary>
public class TimeSyncManager : MonoBehaviour
{
    private static TimeSyncManager _instance;

    private void Awake()
    {
        _instance = this;
    }

    public static TimeSyncManager Instance
    {
        get
        {
            return _instance;
        }
    }

    /// <summary>
    /// 与服务器误差（Tick计算），已经包含与UTC时间的误差（时区误差）
    /// </summary>
    long compareTimeTicks = 0;
    long serverEnterTicks = 0;                   //进入服务器的时候，服务器的64位时间戳
    long clientTicks = 0;                        //进入服务器的时候，客户端的64位时间戳

    /// <summary>
    /// 1970,1,1 DateTime
    /// </summary>
    public DateTime DateTime_1970 = new DateTime(1970, 1, 1);

    /// <summary>
    /// 游戏发布地区的所在时区(例如：西八区是-8.0f)
    /// </summary>
    public float Local_TimeZone = 8;

    /// <summary>
    /// 是否同步时间
    /// </summary>
    public bool SynTimer = false;
    /// <summary>
    /// 同步间隔
    /// </summary>
    public float SynDelay = 5f;

    private float preTime = 0;

    private float mSendTime = 0f;

    public bool SendSysnHeart { get; set; }
    public void Init()
    {
        //CustomSystem.RegistCustomCallback(CustomHeader.SERVER_CUSTOMMSG_SERVER_TIME, OnSyncServerTime);
        SendSysnHeart = true;
        SendSyncTime();
    }

    public void UnInit()
    {
        SynTimer = false;
        //CustomSystem.UnRegistCustomCallback(CustomHeader.SERVER_CUSTOMMSG_SERVER_TIME, OnSyncServerTime);
    }

    private void OnSyncServerTime(VarList args)
    {
        CorrectLocalTime(args.GetInt64(2), args.GetInt64(3));

        float sTime = Time.realtimeSinceStartup;

        int t = Mathf.FloorToInt(1000f * (sTime - mSendTime));
        GameTools.SetPing(t);
        if (!SendSysnHeart)
        {
            GameTools.SetPing(32);

        }

        //LogManager.Instance.LogError("OnSyncServerTime:", sTime);
    }

    /// <summary>
    /// 转换秒为具体的时间(1:1:12:21 天:时:分:秒)
    /// </summary>
    /// <param name="t">秒</param>
    /// <returns>天:时:分:秒</returns>
    public string changeSecsToTime(int t)
    {
        string r = string.Empty;
        int day, hour, minute, second;

        day = Convert.ToInt16(t / 86400);
        hour = Convert.ToInt16((t % 86400) / 3600);
        minute = Convert.ToInt16((t % 86400 % 3600) / 60);
        second = Convert.ToInt16(t % 86400 % 3600 % 60);
        r = GameTools.StringBuilder(string.Format("{0:00}", day), ":", string.Format("{0:00}", hour), ":", string.Format("{0:00}", minute), ":", string.Format("{0:00}", second));

        return r;
    }

    /// <summary>
    /// 转换秒为具体的时间(1:12:21 时:分:秒)
    /// </summary>
    /// <param name="t">秒</param>
    /// <returns>时:分:秒</returns>
    public string ConvertSecsToHours(long t)
    {
        string r = string.Empty;
        long hour, minute, second;
        //DateTime.Now.ToShortTimeString

        hour = Convert.ToInt64(t / 3600);
        minute = Convert.ToInt64((t % 3600) / 60);
        second = Convert.ToInt64(t % 3600 % 60);
        r = GameTools.StringBuilder(string.Format("{0:00}", hour), ":", string.Format("{0:00}", minute), ":", string.Format("{0:00}", second));

        return r;
    }

    /// <summary>
    /// 转换时间戳为具体时间（时:分:秒）
    /// </summary>
    /// <param name="t">1970年开始的UTC秒数</param>
    /// <returns></returns>
    public string ConverLocSecsToTime(long t)
    {
        DateTime time = new DateTime(1970, 1, 1).AddSeconds(t);
        time = time.AddHours(Local_TimeZone);
        return GameTools.StringBuilder(time.Hour.ToString("00"), ":", time.Minute.ToString("00"), ":", time.Second.ToString("00"));
    }

    /// <summary>
    /// 转换秒为具体的时间
    /// </summary>
    /// <returns></returns>
    public string ConvertSecsToTime(int t)
    {
        DateTime time = new DateTime().AddSeconds(t);
        return GameTools.StringBuilder(time.Minute.ToString("00"), ":", time.Second.ToString("00"));
    }

    /// <summary>
    /// 转换秒为具体时间（小时：分钟   01:01）
    /// </summary>
    /// <param name="t"></param>
    /// <returns></returns>
    public string ConvertSecsToMinute(long t)
    {
        DateTime time = new DateTime().AddSeconds(t);
        return GameTools.StringBuilder(time.Hour.ToString("00"), ":", time.Minute.ToString("00"));
    }

    /// <summary>
    /// 转换秒为具体时间（小时：分钟   01:01）
    /// </summary>
    /// <param name="t">秒</param>
    /// <returns></returns>
    public string ConvertSecsToSecond(long t)
    {
        long hour = t / 3600;
        DateTime time = new DateTime().AddSeconds(t);

        return GameTools.StringBuilder(hour.ToString("00"), ":", time.Minute.ToString("00"), ":", time.Second.ToString("00"));
    }

    /// <summary>
    /// 倒计时以秒计算
    /// </summary>
    /// <param name="t"></param>
    /// <returns></returns>
    public string ConvertCountDownToMinute(int t, string daySuffix, string hourSuffix, string minuteSuffix)
    {
        int day, hour, minute;
        day = Convert.ToInt16(t / 86400);
        hour = Convert.ToInt16((t % 86400) / 3600);
        minute = Convert.ToInt16((t % 86400 % 3600) / 60);
        return GameTools.StringBuilder(day.ToString("00"), daySuffix, hour.ToString("00"), hourSuffix, minute.ToString("00"), minuteSuffix);
    }

    /// <summary>
    /// 倒计时以秒计算
    /// </summary>
    /// <param name="t"></param>
    /// <returns></returns>
    public string ConvertCountDownToSecond(int t, string daySuffix, string hourSuffix, string minuteSuffix, string secSuffix)
    {
        int day, hour, minute, second;
        day = Convert.ToInt16(t / 86400);
        hour = Convert.ToInt16((t % 86400) / 3600);
        minute = Convert.ToInt16((t % 86400 % 3600) / 60);
        second = Convert.ToInt16(t % 86400 % 3600 % 60);
        return GameTools.StringBuilder(day.ToString("00"), daySuffix, hour.ToString("00"), hourSuffix, minute.ToString("00"), minuteSuffix, second.ToString("00"), secSuffix);
    }

    /// <summary>
    /// 倒计时转换为日期
    /// </summary>
    /// <param name="time"></param>
    /// <param name="day"></param>
    /// <param name="hour"></param>
    /// <param name="minute"></param>
    /// <param name="second"></param>
    public void ConvertSecondToDate(long time, ref int day, ref int hour, ref int minute, ref int second)
    {
        day = Convert.ToInt16(time / 86400);
        hour = Convert.ToInt16((time % 86400) / 3600);
        minute = Convert.ToInt16((time % 86400 % 3600) / 60);
        second = Convert.ToInt16(time % 86400 % 3600 % 60);
    }

    /// <summary>
    /// 转换秒为具体时间（年-月-日 2015-04-09）1970UTC秒数转本地时区时间
    /// </summary>
    /// <param name="t">1970年开始的UTC秒数</param>
    /// <returns></returns>
    public string ConvertSecsToDay(long t)
    {
        DateTime time = new DateTime(1970, 1, 1).AddSeconds(t);
        time = time.AddHours(Local_TimeZone);
        return GameTools.StringBuilder(time.Year, "-", time.Month.ToString("00"), "-", time.Day.ToString("00"));
    }

    /// <summary>
    /// 转换秒为具体时间（年-月-日-时 2015-04-09-09）1970UTC秒数转本地时区时间
    /// </summary>
    /// <param name="t">1970年开始的UTC秒数</param>
    /// <returns></returns>
    public string ConvertSecsToHour(long t)
    {
        DateTime time = new DateTime(1970, 1, 1).AddSeconds(t);
        time = time.AddHours(Local_TimeZone);
        return GameTools.StringBuilder(time.Year, "-", time.Month.ToString("00"), "-", time.Day.ToString("00"), time.Hour.ToString("  00"), TextData.GetText("Time_Hour"));
    }

    /// <summary>
    /// 转换秒为具体时间（年-月-日 时:分 2015-04-09 09:30:57）1970UTC秒数转本地时区时间
    /// </summary>
    /// <param name="t">1970年开始的UTC秒数</param>
    /// <returns></returns>
    public string ConvertSecsToString(long t)
    {
        DateTime time = new DateTime(1970, 1, 1).AddSeconds(t);
        time = time.AddHours(Local_TimeZone);
        return GameTools.StringBuilder(time.Year, "-", time.Month.ToString("00"), "-", time.Day.ToString("00"), time.Hour.ToString("  00"), ":", time.Minute.ToString("00"), ":", time.Second.ToString("00"));
    }

    /// <summary>
    /// 转换秒为具体的时间--增加时区信息 自1970年开始的UTC秒数，带时区校正
    /// </summary>
    /// <returns></returns>
    public DateTime ConvertSecsToDateTime(long timestamp)
    {
        DateTime start = new DateTime(1970, 1, 1).AddSeconds(timestamp);
        return start.AddHours(Local_TimeZone);
    }

    /// <summary>
    /// 转换秒为具体的时间(12:21 分:秒)
    /// </summary>
    /// <param name="t">秒</param>
    /// <returns>时:分:秒</returns>
    public string ConvertSecsToMinute(int t)
    {
        string r = string.Empty;
        int minute, second;
        minute = Convert.ToInt16((t % 3600) / 60);
        second = Convert.ToInt16(t % 3600 % 60);
        r = GameTools.StringBuilder(string.Format("{0:00}", minute), ":", string.Format("{0:00}", second));
        return r;
    }
    /// <summary>
    /// 得到utc时间，自0000年开始的秒数（为了兼容老接口，这里是自1970年开始的秒数）
    /// </summary>
    /// <returns></returns>
    public long GetUTCTime()
    {
        TimeSpan sp = new TimeSpan(GetServerDataTime().Ticks - DateTime_1970.Ticks);
        return (long)sp.TotalSeconds;
    }

    /// <summary>
    /// 获取服务器时间（通过服务器时间计算）
    /// </summary>
    /// <returns></returns>
    public long GetNowUTCMTime()
    {
        TimeSpan sp = new TimeSpan(GetServerDataTime().Ticks - DateTime_1970.Ticks);
        return (long)sp.TotalMilliseconds;
    }

    /// <summary>
    /// 获取服务器时间（服务器UTC时间）
    /// (不允许修改)
    /// </summary>
    /// <returns></returns>
    public DateTime GetServerDataTime()
    {
        DateTime dt = new DateTime().AddTicks(DateTime.Now.Ticks + compareTimeTicks);
        return dt;
    }

    /// <summary>
    /// 获取当前是周几
    /// </summary>
    /// <returns></returns>
    public int GetNowWeekDay()
    {
        DateTime now = GetLocalDataTime();

        int y = now.Year;
        int m = now.Month;
        int d = now.Day;
        if (m == 1 || m == 2)
        {
            m += 12;
            y--;
        }
        int weekIndex = (d + 2 * m + 3 * (m + 1) / 5 + y + y / 4 - y / 100 + y / 400) % 7;
        if(weekIndex < 0)
        {
            weekIndex = -weekIndex;
        }
        return weekIndex + 1;
    }

    /// <summary>
    /// 获取本地时间（校验过本地时区）
    /// </summary>
    /// <returns></returns>
    public DateTime GetLocalDataTime()
    {
        DateTime dt = GetServerDataTime();
        dt = dt.AddHours(Local_TimeZone);
        return dt;
    }

    /// <summary>
    /// 获取当天0点之后的总秒数
    /// </summary>
    /// <returns></returns>
    public int GetLocalTotleSecond()
    {
        DateTime dtime = GetLocalDataTime();
        return dtime.Hour * 3600 + dtime.Minute * 60 + dtime.Second;
    }

    /// <summary>
    /// 获取当天0点之后的总秒数
    /// </summary>
    /// <returns></returns>
    public int GetServerTotleSecond()
    {
        DateTime dtime = GetServerDataTime();
        dtime = dtime.AddHours(Local_TimeZone);
        return dtime.Hour * 3600 + dtime.Minute * 60 + dtime.Second;
    }
    /// <summary>
    /// 获取当天0点之后的总秒数
    /// </summary>
    /// <returns></returns>
    public int GetServerTotleMilliSecond()
    {
        DateTime dtime = GetServerDataTime();
        dtime = dtime.AddHours(Local_TimeZone);
        return  (dtime.Hour * 3600 + dtime.Minute * 60 + dtime.Second) * 1000 + dtime.Millisecond;
    }
    /// <summary>
    /// 获取自1970开始的总秒数(服务器UTC时间)
    /// </summary>
    /// <returns></returns>
    public long GetTotleSeconds()
    {
        TimeSpan sp = GetServerDataTime().Subtract(new DateTime(1970, 1, 1));
        return (long)sp.TotalSeconds;
    }

    /// <summary>
    /// 校正本地时间
    /// </summary>
    /// <param name="timer">服务器以秒计算的时间,自1970年开始的秒数，UTC</param>
    public void CorrectLocalTime(long timer, long pServerTicks)
    {
        DateTime serverTime = new DateTime(1970, 1, 1).AddSeconds(timer);
        DateTime nowTime = DateTime.Now;
        TimeSpan compareTime = serverTime.Subtract(nowTime);
        compareTimeTicks = compareTime.Ticks;

        serverEnterTicks = pServerTicks;
        clientTicks = nowTime.Ticks / 10000;

        SynTimer = true;

        ServerNowTicks();
    }

    /// <summary>
    /// 获得服务器当前的ticks
    /// </summary>
    /// <param name="timer"></param>
    /// <returns></returns>
    public long ServerNowTicks()
    {
        DateTime nowTime = DateTime.Now;
        return (serverEnterTicks + nowTime.Ticks / 10000 - clientTicks);
    }

    public long ServerNowSecond()
    {
        DateTime nowTime = DateTime.Now;
        return (serverEnterTicks + nowTime.Ticks / 10000 - clientTicks) / 1000;
    }

    private VarList mVarList = null;
    /// <summary>
    /// 发送同步时间消息
    /// </summary>
    public void SendSyncTime()
    {
        if (mVarList == null)
        {
            mVarList = VarList.GetVarList();
        }
        else
        {
            mVarList.Clear();
        }
        
        //mVarList.AddInt(CustomHeader.CLIENT_CUSTOMMSG_GET_SERVERTIME);
        //GameCommand.SendCustom(CustomHeader.CLIENT_CUSTOMMSG_GET_SERVERTIME, mVarList);

        //mSendTime = Time.realtimeSinceStartup;
        //LogManager.Instance.LogError("SendSyncTime:", mSendTime);
    }

    public void ResetSyncTime()
    {
        mSendTime = Time.realtimeSinceStartup;
        preTime = Time.realtimeSinceStartup;
        GameTools.SetPing(32);
    }

    public void OnUpdate()
    {
        //if (!GameSceneManager.Instance.SceneReady())
        //{
        //    return;
        //}

        //if (SendSysnHeart)
        //{
        //    if (LoadingManager.Instance != null && !LoadingManager.Instance.IsLoadingLevel)
        //    {
        //        if (Time.realtimeSinceStartup - preTime >= SynDelay)
        //        {
        //            preTime = Time.realtimeSinceStartup;
        //            SendSyncTime();
        //        }
        //    }
        //}
        
        
    }
}
