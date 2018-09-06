using UnityEngine;
#if UNITY_IPHONE && !UNITY_EDITOR
using UnityEngine.iOS;
#endif

public class GlobalData : MonoBehaviour
{
#region <>游戏相关信息<>
    /// <summary>
    /// 语言版本
    /// </summary>
    public static string mLanguageVersion =
#if EN
        "EN"
#elif TW
        "TW"
#else
        "CN"
#endif
        ;

    /// <summary>
    /// 游戏版本
    /// </summary>
    public static readonly string mVersion = Application.version;

    /// <summary>
    /// 游戏名称
    /// </summary>
    public static readonly string mProductName = Application.productName;

    /// <summary>
    /// 游戏包名
    /// </summary>
    public static readonly string mPackageName = Application.identifier;

    /// <summary>
    /// 分区ID
    /// </summary>
    public static string mDistrictId = "0";

    /// <summary>
    /// 游戏ID
    /// </summary>
    public static string mGameID = string.Empty;

    /// <summary>
    /// 渠道ID
    /// </summary>
    public static string mChannelID = string.Empty;

    /// <summary>
    /// 日志开关
    /// </summary>
    public static bool mTrace = true;

    public static bool mGlobalXML = true;

    /// <summary>
    /// xLua Hotfix开关，默认关闭
    /// </summary>
    public static bool mHotfixEnable = false;

    /// <summary>
    /// 状态服分组号（用于区分状态服服务器列表显示）
    /// 1.内网 2.版署 3.腾讯 4.凯撒
    /// </summary>
    public static int StateServerGroup
    {
        get;
        set;
    }

    public static int HeartBeat
    {
        get;
        set;
    }

    public static int TimeoutBeat
    {
        get;
        set;
    }

    public static bool HotFixEnable
    {
        set
        {
            mHotfixEnable = value;
        }
        get
        {
            return mHotfixEnable;
        }
    }

    /// <summary>
    /// Unity远程调试开关
    /// </summary>
    public static bool HdgRemoteDebug
    {
        set
        {
            // 为true开启远程调试服务
            if (value)
            {
//                 RemoteDebugServerFactory.Instance.StartServer();
//                 Hdg.RemoteDebugServer.AddDontDestroyOnLoadObject(GameObject.Find("Launch"));
//                 Hdg.RemoteDebugServer.AddDontDestroyOnLoadObject(GameObject.Find("UICamera"));
//                 Hdg.RemoteDebugServer.AddDontDestroyOnLoadObject(GameObject.Find("UIRoot"));
            }
        }
    }

    /// <summary>
    /// FPS状态
    /// </summary>
    private static bool mStatus = false;

    /// <summary>
    /// 调试模式
    /// </summary>
#if UNITY_IPHONE && !UNITY_EDITOR
    public static bool mDebug = false;
#elif (UNITY_EDITOR || UNITY_STANDALONE_WIN) && !UNITY_PC
    public static bool mDebug = true;
#elif (UNITY_EDITOR || UNITY_STANDALONE_WIN) && UNITY_PC
    public static bool mDebug = false;
#elif UNITY_ANDROID && !UNITY_EDITOR
    public static bool mDebug = false;
#endif

    /// <summary>
    /// 资源服地址
    /// </summary>
    public static string mResourceServer = string.Empty;

    public static string mLuaServer = string.Empty;

    /// <summary>
    /// 错误日志采集地址
    /// </summary>
    public static string mErrorUploadPath = string.Empty;

    /// <summary>
    /// 崩溃采集服务器地址
    /// </summary>
    public static string mCrashReportServer = string.Empty;

#region <>角色信息<>

    /// <summary>
    /// 玩家账号
    /// </summary>
    public static string UserAccount
    {
        get;
        set;
    }

    /// <summary>
    /// 计费返回账号
    /// </summary>
    public static string UserChargeAccount
    {
        get;
        set;
    }

    /// <summary>
    /// 角色名
    /// </summary>
    public static string RoleName
    {
        get;
        set;
    }

    /// <summary>
    /// 开放ID
    /// </summary>
    public static string OpenID
    {
        get;
        set;
    }

    /// <summary>
    /// 登录串
    /// </summary>
    public static string LoginStr
    {
        get;
        set;
    }

    /// <summary>
    /// 首次登录
    /// </summary>
    public static bool FirstLogin
    {
        get;
        set;
    }

#endregion

#region <>服务器状态列表<>

    public static string ServerId
    {
        get; set;
    }

    /// <summary>
    /// 
    /// </summary>
    public static string ServerStatusPath
    {
        get; set;
    }

    /// <summary>
    /// 
    /// </summary>
    public static string ServerStatusKey
    {
        get; set;
    }

    /// <summary>
    /// 
    /// </summary>
    //public static ServerStatusManager.ServerList ServerList
    //{
    //    get; set;
    //}

#endregion

#region <>强制更新<>

    /// <summary>
    /// 是否更新强制
    /// </summary>
    public static bool mForceUpdate { get; set; }

    /// <summary>
    /// 目标版本号，用于强制更新。
    /// </summary>
    public static string mTargetVersion { get; set; }

    /// <summary>
    /// 强更地址
    /// </summary>
    public static string mForceUpdateUrl { get; set; }

#endregion

#region <>GVoice语音<>

    public static string GVoiceAppId
    {
        get;
        set;
    }

    public static string GVoiceAppKey
    {
        get;
        set;
    }

    public static string GVoiceServer
    {
        get;
        set;
    }

#endregion

#region <>MTP安全参数<>

    public static int TP2GameId { get; set; }

    public static string TP2AppKey { get; set; }

    //public static int TP2AccountType
    //{
    //    get
    //    {
    //        return (int)Tp2Entry.ENTRY_ID_OTHERS;
    //    }
    //}

#endregion

#region <>Bugly参数<>

    public static string AndroidBuglyAppId { get; set; }

    public static string iPhoneBuglyAppId { get; set; }

#endregion

#region <>画质相关参数<>
    /// <summary>
    /// 画质配置地址
    /// </summary>
    public static string mQualityConfigPath = string.Empty;

#endregion

#endregion

#region <>系统信息<>

    /// <summary>
    /// 设备平台
    /// </summary>
    public enum PlatformType
    {
        PlatformType_Unknown = 0,
        PlatformType_Editor = 1,
        PlatformType_iOS = 2,
        PlatformType_Android = 3,
        PlatformType_WinStandalone = 4,
        PlatformType_Max,
    }

    /// <summary>
    /// 设备平台
    /// </summary>
    public static PlatformType DevicePlatform
    {
        get
        {
#if UNITY_EDITOR
            return PlatformType.PlatformType_Editor;
#elif UNITY_IPHONE
            return PlatformType.PlatformType_iOS;
#elif UNITY_ANDROID
            return PlatformType.PlatformType_Android;
#elif UNITY_STANDALONE_WIN
            return PlatformType.PlatformType_WinStandalone;
#endif
        }
    }

    public static float WHRate
    {
        get;
        set;
    }

    public static bool useLua
    {
        get
        {
            return true;
        }
    }

    public static bool isTrace
    {
        get
        {
            return false;
        }
    }

    //public static bool isStatus
    //{
    //    get
    //    {
    //        return mStatus;
    //    }
    //    set
    //    {
    //        mStatus = value;
    //        Status.Instance.ShowStatus(value);
    //    }
    //}

    public static bool isiPhone
    {
        get
        {
            return DevicePlatform == PlatformType.PlatformType_iOS;
        }
    }

    public static bool isiPhoneX
    {
        get
        {
#if UNITY_IPHONE && !UNITY_EDITOR
            return Device.generation == DeviceGeneration.iPhoneX;
#else
            return false;
#endif
        }
    }

    public static bool isAndroid
    {
        get
        {
            return DevicePlatform == PlatformType.PlatformType_Android;
        }
    }

    /// <summary>
    /// 是否是移动设备
    /// </summary>
    public static bool isMobile
    {
        get
        {
            return isiPhone || isAndroid;
        }
    }

    public static bool isEditor
    {
        get
        {
            return DevicePlatform == PlatformType.PlatformType_Editor;
        }
    }

    public static bool isStandalone
    {
        get
        {
            return DevicePlatform == PlatformType.PlatformType_WinStandalone;
        }
    }

    /// <summary>
    /// 设备型号
    /// </summary>
    public static string DeviceModel
    {
        get { return SystemInfo.deviceModel; }
    }

    /// <summary>
    /// 设备UID
    /// </summary>
    public static string DeviceUID
    {
        get { return SystemInfo.deviceUniqueIdentifier; }
    }

    /// <summary>
    /// CPU型号
    /// </summary>
    public static string DeviceProcessorType
    {
        get { return SystemInfo.processorType; }
    }

    /// <summary>
    /// CPU核心数
    /// </summary>
    public static int DeviceProcessorCount
    {
        get { return SystemInfo.processorCount; }
    }

    /// <summary>
    /// CPU主频（为保证设备主频准确，故除以1000，不除1024）
    /// </summary>
    public static float DeviceProcessorFrequency
    {
        get { return SystemInfo.processorFrequency / 1000.0f; }
    }

    /// <summary>
    /// 系统内存（理由同上）
    /// </summary>
    public static float DeviceMemory
    {
        get { return SystemInfo.systemMemorySize / 1000.0f; }
    }

    /// <summary>
    /// 设备显卡名称
    /// </summary>
    public static string DeviceVendorName
    {
        get { return SystemInfo.graphicsDeviceVendor; }
    }

    /// <summary>
    /// 设备操作系统（带版本号）
    /// </summary>
    public static string DeviceOperatingSystem
    {
        get { return SystemInfo.operatingSystem; }
    }

    /// <summary>
    /// 设备充电状态
    /// </summary>
    public static BatteryStatus DeviceBatteryStatus
    {
        get { return SystemInfo.batteryStatus; }
    }
    public static bool DeviceBattery
    {
        get
        {
            if (SystemInfo.batteryStatus == BatteryStatus.Charging)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
    /// <summary>
    /// 设备电量
    /// </summary>
    public static float DeviceBatteryLevel
    {
        get { return SystemInfo.batteryLevel; }
    }

    /// <summary>
    /// 设备默认屏幕宽度
    /// </summary>
    public static int DeviceDefaultScreenWidth
    {
        get;
        set;
    }

    /// <summary>
    /// 设备默认屏幕高度
    /// </summary>
    public static int DeviceDefaultScreenHeight
    {
        get;
        set;
    }

    /// <summary>
    /// 设备屏幕宽度（某些ipad会返回相反的宽高，故做一次对比）
    /// </summary>
    public static int DeviceScreenWidth
    {
        get { return Screen.width > Screen.height ? Screen.width : Screen.height; }
    }

    /// <summary>
    /// 设备屏幕高度（同上理）
    /// </summary>
    public static int DeviceScreenHeight
    {
        get { return Screen.height > Screen.width ? Screen.width : Screen.height; }
    }

#endregion

#region <>网络信息<>
    public static string NetUnreachable
    {
        get; set;
    }
    public static string NetConnectFailed
    {
        get; set;
    }

    public static string UpdateContent
    {
        get; set;
    }

    public static string UITips
    {
        get; set;
    }

#endregion

#region 按钮状态
    private static Material gray;

    public static Material GetGrayMat
    {
        get
        {
            if (gray == null)
            {
                gray = new Material(Shader.Find("UISprites/DefaultGray"));
            }

            return gray;
        }
    }
#endregion

    // 进入的副本ID
    private static int mEnterChapterId = 0;

    public static int EnterChapterId
    {
        set
        {
            mEnterChapterId = value;
        }
        get
        {
            return mEnterChapterId;
        }
    }


    public static bool Test
    {
        get;set;
    }
}