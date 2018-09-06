using System;
using System.IO;
using System.Net;
using System.Text;
using UnityEngine;

public class LogManager : MonoBehaviour 
{
    private static LogManager _Instance = null;
    public static LogManager Instance
    {
        get { return _Instance; }
    }
    
    private FileStream mCrashFileStream = null;
    private StreamWriter mCrashStreamWriter = null;
    //private string mFilePath = string.Empty;
    //private string mErrorLogPath = string.Empty;
    private string mCrashFilePath = string.Empty;

    private void Awake()
    {
        _Instance = this;

        if (GlobalData.isEditor || GlobalData.isStandalone)
        {
            mCrashFilePath = GameTools.StringBuilder(Application.dataPath, "/../CrashReport.log");
        }
        else if (GlobalData.isMobile)
        {
            mCrashFilePath = GameTools.StringBuilder(Application.persistentDataPath, "/CrashReport.log");
        }

        UploadCrashReport();
    }

    private void UploadCrashReport()
    {

        CrashReport[] reports = CrashReport.reports;
        if (reports == null || reports.Length < 1)
        {
            return;
        }

        WebClient client = new WebClient();
        if (File.Exists(mCrashFilePath))
        {
            // 上传文件后，删除该文件。
            client.UploadFile(GlobalData.mCrashReportServer, mCrashFilePath);
            File.Delete(mCrashFilePath);
        }
        if (mCrashFileStream == null)
        {
            mCrashFileStream = new FileStream(mCrashFilePath, FileMode.OpenOrCreate);
            mCrashStreamWriter = new StreamWriter(mCrashFileStream);
        }

        for (int i = 0; i < reports.Length; ++i)
        {
            DateTime time = reports[i].time;
            string text = reports[i].text;

            WriteCrash(GameTools.StringBuilder("[", time, "] ", text));
        }
        /*
        if (mStreamWriter != null)
        {
            mStreamWriter.Flush();
            mStreamWriter.Close();
            mFileStream.Close();
            mFileStream = null;
            mStreamWriter = null;
        }
        */
        // 写完崩溃日志后，上传服务器。
        client.UploadFile(GlobalData.mCrashReportServer, mCrashFilePath);
        CrashReport.RemoveAll();
        File.Delete(mCrashFilePath);
    }

    public void Log(params object[] args)
    {
        if (!GlobalData.isTrace)
        {
            return;
        }

        string time = GameTools.StringBuilder("[", GameTools.GetTime(), "]");
        string log = GameTools.StringBuilder(args);
        if (string.IsNullOrEmpty(log))
        {
            return;
        }

        log = GameTools.StringBuilder(time, "[Log]", log);
        Debug.Log(log);
    }

    public void LogWarning(params object[] args)
    {
        if (!GlobalData.isTrace)
        {
            return;
        }

        string time = GameTools.StringBuilder("[", GameTools.GetTime(), "]");
        string log = GameTools.StringBuilder(args);
        if (string.IsNullOrEmpty(log))
        {
            return;
        }

        log = GameTools.StringBuilder(time, "[Warning]", log);
        Debug.LogWarning(log);
    }

    public void LogError(params object[] args)
    {
        if (!GlobalData.isTrace)
        {
            return;
        }

        string time = GameTools.StringBuilder("[", GameTools.GetTime(), "]");
        string log = GameTools.StringBuilder(args);
        if (string.IsNullOrEmpty(log))
        {
            return;
        }

        log = GameTools.StringBuilder(time, "[Error]", log);
        Debug.LogError(log);
    }

    //战斗日志
    public void LogFight(params object[] args)
    {
        if (!GlobalData.isTrace)
        {
            return;
        }

        string time = GameTools.StringBuilder("[", GameTools.GetTime(), "]");
        string log = GameTools.StringBuilder(args);
        if (string.IsNullOrEmpty(log))
        {
            return;
        }

        log = GameTools.StringBuilder("<color=yellow>",time, "[Fight]", log, "</color>");
        Debug.Log(log);
    }

    public void LogException(Exception e)
    {
        if (!GlobalData.isTrace)
        {
            return;
        }

        Debug.LogException(e);
    }
    
    private void WriteCrash(string crash)
    {
        if (mCrashFileStream == null)
            return;

        mCrashStreamWriter.WriteLine(crash);
    }

    private void OnDestroy()
    {
        if (mCrashStreamWriter != null)
        {
            mCrashStreamWriter.Flush();
            mCrashStreamWriter.Close();
            mCrashFileStream.Close();
            mCrashFileStream = null;
            mCrashStreamWriter = null;
        }
    }
}