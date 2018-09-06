using UnityEngine;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine.UI;


public class GameTools
{
    private static System.Text.StringBuilder sb = new System.Text.StringBuilder();
    /// <summary>
    /// 字符串拼接
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    public static string StringBuilder(params object[] args)
    {
        sb.Remove(0, sb.Length);

        for (int i = 0; i < args.Length; ++i)
        {
            sb.Append(args[i]);
        }
        
        return sb.ToString();
    }

    public static void SetLayer(Transform t,int layer)
    {
        t.gameObject.layer = layer;
        int childCount = t.childCount;
        if (childCount > 0)
        {
            for (int i = 0; i < childCount; i++)
            {
                SetLayer(t.GetChild(i), layer);
            }
        }
    }

    /// <summary>
    /// 播放视频
    /// </summary>
    /// <param name="movieName"></param>
    public static void PlayMovie(string movieName)
    {
#if UNITY_IPHONE || UNITY_ANDROID
        if (string.IsNullOrEmpty(movieName))
        {
            return;
        }
     
        string path = StringBuilder(movieName, ".mp4");

        Handheld.PlayFullScreenMovie(path, Color.black, FullScreenMovieControlMode.Hidden, FullScreenMovieScalingMode.Fill);
#endif
    }

    #region <>类型转换类方法<>
    /*
    private static string [] IntString =
    {
        "0", "1", "2", "3", "4", "5", "6", "7", "8", "9",
        "10", "11", "12", "13", "14", "15", "16", "17", "18", "19",
        "20", "21", "22", "23", "24", "25", "26", "27", "28", "29",
        "30", "31", "32", "33", "34", "35", "36", "37", "38", "39",
        "40", "41", "42", "43", "44", "45", "46", "47", "48", "49",
        "50", "51", "52", "53", "54", "55", "56", "57", "58", "59",
        "60", "61", "62", "63", "64", "65", "66", "67", "68", "69",
        "70", "71", "72", "73", "74", "75", "76", "77", "78", "79",
        "80", "81", "82", "83", "84", "85", "86", "87", "88", "89",
        "90", "91", "92", "93", "94", "95", "96", "97", "98", "99",
    };
    */

    private static Dictionary<int, string> mIntString = null;

    public static void Init()
    {
        mIntString = new Dictionary<int, string>();
        for (int i = -500; i <= 500; ++i)
        {
            mIntString.Add(i, i.ToString());
        }
    }
    public static string Int2String(int value)
    {
        if (value >= -500 && value <= 500)
        {
            return mIntString[value];
        }
        else if (value < -500 || value > 500)
        {
            return value.ToString();
        }

        return value.ToString();
    }

    /*
    public static string Int2String(int value)
    {
        if (value >= 0 && value < 100)
        {
            return IntString[value];
        }
        else if(value > -100 && value < 0){
            return GameTools.StringBuilder("-", IntString[Math.Abs(value)]);
        }

        return string.Format("{0}", value);
    }
    */
    public static int IntParse(string str)
    {
        int value = 0;
        int.TryParse(str, out value);
        return value;
    }

    public static float FloatParse(string str)
    {
        float value = 0.0f;
        float.TryParse(str, out value);
        return value;
    }

    public static double DoubleParse(string str)
    {
        double value = 0.0f;
        double.TryParse(str, out value);
        return value;
    }

    public static long LongParse(string str)
    {
        long value = 0;
        long.TryParse(str, out value);
        return value;
    }
    public static string StringParse(object value)
    {
        return string.Format("", value);
    }


    public static float Long2Float(long f,int length = 0)
    {
        string str = f.ToString();
        if (length == 0)
        { 
            return FloatParse(str);
        }
        else
        {
            int len = str.Length;
            string catStr = str.Substring(len - length, length);
            return FloatParse(catStr);
        }
    } 

    public static string Vector3StringF2(Vector3 v)
    {
        string x = v.x.ToString("F2");
        string y = v.y.ToString("F2");
        string z = v.z.ToString("F2");

        return StringBuilder(x, ",", y, ",", z);
    }

    //弧度转角度
    public static float Rad2Deg(float rad)
    {
        return rad / Mathf.Deg2Rad;
    }

    //角度转弧度
    public static float Deg2Rad(float angle)
    {
        return angle * Mathf.Deg2Rad;
    }

    #endregion

    #region <>时间类方法<>
    public static string GetCurrentTime()
    {
        return DateTime.Now.ToShortTimeString().ToString();
    }

    public static double GetTimestamp(int second)
    {
        if (second < 1)
        {
            return 0;
        }

        return (int)GetTotalSeconds() / second;
    }

    public static double GetTotalSeconds()
    {
        TimeSpan ts = DateTime.Now - DateTime.Parse("1970-1-1");
        return ts.TotalSeconds;
    }

    public static double GetTotalSeconds(long time)
    {
        TimeSpan ts = new DateTime(time) - DateTime.Parse("1970-1-1");
        return ts.TotalSeconds;
    }

    public static double GetTotalMilliseconds()
    {
        TimeSpan ts = DateTime.Now - DateTime.Parse("1970-1-1");
        return ts.TotalMilliseconds;
    }

    public static double GetTotalMilliseconds(long time)
    {
        TimeSpan ts = new DateTime(time) - DateTime.Parse("1970-1-1");
        return ts.TotalMilliseconds;
    }

    public static string GetTimeFormatHMS(int time)
    {
        int day;
        int hour;
        int min;
        int second;
        string str = "";
        day = time / 86400;
        hour = (time - day* 86400) / 3600;
        min = (time - day * 86400 - hour * 3600) / 60;
        second = time % 60;

        if (day > 0)
        {
            str = StringBuilder(day, TextData.GetText("Time_Day"));
        }
        if (hour > 0)
        {
            //str = StringBuilder(hour, TextData.GetText("Time_Hour"), min, TextData.GetText("Time_Minute"), second, TextData.GetText("Time_Second"));
            str = StringBuilder(str, hour, TextData.GetText("Time_Hour"));

        }
        if (min > 0)
        {
            str = StringBuilder(str, min, TextData.GetText("Time_Minute"));
        }
        if (second > 0)
        {
            str = StringBuilder(str, second, TextData.GetText("Time_Second"));
        }
        //str = StringBuilder( hour, TextData.GetText("Time_Hour"), min, TextData.GetText("Time_Minute"), second, TextData.GetText("Time_Second"));

        return str;
    }

    public static string GetTimeFormal(int time)
    {
        int min;
        int second;
        string str = "";
        min = time / 60;
        second = time % 60;

        if (min < 10)
        {
            str = StringBuilder("0", min, ":");
        }
        else
        {
            str = StringBuilder( min, ":");
        }

        if (second < 10)
        {
            str = StringBuilder("0" , second);
        }
        else
        {
            str = Int2String(second);
        }

        return str;
    }

    /// <summary>
    /// 获取时间格式
    /// 2017-06-05 03:55:55
    /// </summary>
    /// <returns></returns>
    public static string GetTime()
    {
        return DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
    }
    public static string GetHouseTime() {
        return DateTime.Now.ToString("HH:mm:ss");
    }

    public static Vector3 CalculateCubicBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        Vector3 p = uu * p0;
        p += 2 * u* t * p1;
        p += tt* p2;
        return p;
    } 
    /// <summary>
    /// 获取时间格式
    /// 03:55:55
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    public static string GetTime(long time)
    {
        DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
        DateTime dt = dtStart.AddMilliseconds(time);
        return dt.ToString("HH:mm:ss");
    }


    /// <summary>
    /// 获取日期格式
    /// 2017-06-05
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    public static string GetDate(long time)
    {
        DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
        DateTime dt = dtStart.AddMilliseconds(time);
        return dt.ToString("yyyy/MM/dd HH:mm:ss");
    }
    /// <summary>
    /// 获取日期格式
    /// 2017-06-05
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    public static string GetDateText(long time)
    {
        DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
        DateTime dt = dtStart.AddMilliseconds(time);
        string content = string.Format(TextData.GetText("AntiAddiction20"), dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute);
        return content;
    }

#endregion

#region <>范围判断类方法<>

    /// <summary>
    /// 是否在圆形内
    /// </summary>
    /// <param name="circleCenter"></param>
    /// <param name="radius"></param>
    /// <param name="point"></param>
    /// <returns></returns>
    public static bool InCircle(Vector2 circleCenter, float radius, Vector2 point)
    {
        if (radius < 0.0f)
        {
            return false;
        }
        return Vector2.Distance(circleCenter, point) <= radius;
    }
    /// <summary>
    /// 环形范围
    /// </summary>
    /// <param name="enter"></param>
    /// <param name="radius1"></param>
    /// <param name="radius2"></param>
    /// <param name="point"></param>
    /// <returns></returns>
    public static bool InRing(Vector2 enter, float radius1,float radius2, Vector2 point)
    {
        if (radius1 < 0.0f|| radius2<0.0f)
        {
            return false;
        }
        float dis = Vector2.Distance(enter, point);
        if (dis >= radius1 && dis <= radius2)
        {
            return true;
        }
        return false;
    }

    public static bool InBall(Vector3 center, float radius, Vector3 point)
    {
        if (radius < 0.0f)
        {
            return false;
        }
        return Vector3.Distance(center, point) <= radius;
    }


    private static float GetCross(Vector2 p1, Vector2 p2, Vector2 p)
    {
        return (p2.x - p1.x) * (p.y - p1.y) - (p.x - p1.x) * (p2.y - p1.y);
    }
    /// <summary>
    /// 是否在矩形中
    /// </summary>
    /// <param name="p">被判断点</param>
    /// <param name="p1"></param>
    /// <param name="p2"></param>
    /// <param name="p3"></param>
    /// <param name="p4"></param>
    /// <returns></returns>

    public static bool InRect(Vector2 p,Vector2 p1,Vector2 p2,Vector2 p3,Vector2 p4)
    {
        return GetCross(p1, p2, p) * GetCross(p3, p4, p) >= 0 && GetCross(p2, p3, p) * GetCross(p4, p1, p) >= 0;
    }

    /*
    public static bool InRect(Vector2 rectCenter, float width, float height, Vector2 point)
    {
        if (width < 0.0f || height < 0.0f)
        {
            return false;
        }

        Rect rect = new Rect(rectCenter, new Vector2(width, height));
        rect.width = width;
        rect.height = height;
        return rect.Contains(point);
    }
    */
    /// <summary>
    /// 
    /// </summary>
    /// <param name="pos">判断的时候初始位置</param>
    /// <param name="forward">主角的前方判断的方向</param>
    /// <param name="radius"></param>
    /// <param name="degree"></param>
    /// <param name="otherT"></param>
    /// <param name="fDis">前方距离</param>
    /// <returns></returns>
    public static bool InSector(Vector3 pos,Vector3 forward, float radius, float degree, Transform otherT)
    {
        if (radius < 0.0f || degree < 0.0f)
        {
            return false;
        }
        degree = degree * 0.5f;
        Vector3 vec = otherT.position - pos;
        float dis = Vector3.Distance(pos, otherT.position);
        if (dis > radius)
        {
            return false;
        }

        
        float angel = Vector3.Angle(forward, vec);

        if (angel > degree)
        {
            return false;
        }
        return true;
    }
    public static bool PointOnVectorLeft(Vector3 srcVector,Vector3 point)
    {
        point.y = srcVector.y;
        float angle = Vector3.Dot(srcVector, point);
        if (angle < 0.0f)
        {
            return true;//左
        }
        else
        {
            return false;
        }
    }
    
    /// <summary>
    /// 向量旋转
    /// </summary>
    /// <returns></returns>
    public static Vector3 RotateRound(Vector3 vec, Vector3 axis, float angle)
    {
        return Quaternion.AngleAxis(angle, axis) * vec;

    }
    /*
    public static bool PointOnLeftSideOfVector(this Vector3 vector3, Vector3 originPoint, Vector3 point)
    {
        Vector2 originVec2 = originPoint.IgnoreYAxis();
        Vector2 pointVec2 = (point.IgnoreYAxis() - originVec2).normalized;
        Vector2 vector2 = vector3.IgnoreYAxis();
        float verticalX = originVec2.x;
        float verticalY = (-verticalX * vector2.x) / vector2.y;
        Vector2 norVertical = (new Vector2(verticalX, verticalY)).normalized;
        float dotValue = Vector2.Dot(norVertical, pointVec2);
        return dotValue < 0f;
    }
    */
    /// <summary>
    /// 获取
    /// </summary>
    /// <param name="offset"></param>
    /// <returns></returns>
    public static Vector3 GetRandomVector3(float offset)
    {
        Vector3 temp = Vector3.zero;
        temp.x = UnityEngine.Random.Range(-offset, offset);
        temp.y = UnityEngine.Random.Range(-offset, offset);
        temp.z = UnityEngine.Random.Range(-offset, offset);
        
        return temp;
    }

    public static Vector3 GetRandomVector3Locky(float offset)
    {
        Vector3 temp = Vector3.zero;
        temp.x = UnityEngine.Random.Range(-offset, offset);
        temp.y = 0f;
        temp.z = UnityEngine.Random.Range(-offset, offset);

        return temp;
    }

    private static List<Vector3> paths = new List<Vector3>();
    public static List<Vector3> GetRandomPathBetween2Points(Vector3 start, Vector3 end, int step = 1, float offset = 0.0f)
    {
        paths.Clear();
        if (start == end)
        {
            return paths;
        }
        
        if (step <= 1)
        {
            paths.Add(start);
            paths.Add(end);
            return paths;
        }

        Vector3 offset_vec = Vector3.zero;
        Vector3 temp = (end - start) / step;
        paths.Add(start);
        for (int i = 1; i < step; ++i)
        {
            paths.Add(start + temp * i + GetRandomVector3(offset));
        }
        paths.Add(end);

        return paths;
    }
    public static List<Vector3> GetRandomLockYPathBetween2Points(Vector3 start, Vector3 end, int step = 1, float offset = 0.0f)
    {
        paths.Clear();
        if (start == end)
        {
            return paths;
        }

        if (step <= 1)
        {
            paths.Add(start);
            paths.Add(end);
            return paths;
        }

        Vector3 offset_vec = Vector3.zero;
        Vector3 temp = (end - start) / step;
        paths.Add(start);
        for (int i = 1; i < step; ++i)
        {
            paths.Add(start + temp * i + GetRandomVector3Locky(offset));
        }
        paths.Add(end);

        return paths;
    }

    static Vector2 vecTemp2D1 = Vector2.zero;
    static Vector2 vecTemp2D2 = Vector2.zero;

    public static float Distance2D(Vector3 p1, Vector3 p2)
    {
        vecTemp2D1.x = p1.x;
        vecTemp2D1.y = p1.z;
        vecTemp2D2.x = p2.x;
        vecTemp2D2.y = p2.z;

        return Vector2.Distance(vecTemp2D1, vecTemp2D2);
    }
    
#endregion

#region <>特效相关<>

    /// <summary>
    /// 重置Nc特效
    /// </summary>
    public static void ResetNcEffect(GameObject effect, bool bResetParent)
    {
        //if (effect == null)
        //    return;

        //if (bResetParent && effect.transform.parent != null)
        //    effect.transform.parent = null;
        //{
        //    NcVisible nc = effect.GetComponent<NcVisible>();
        //    if (!effect.activeSelf && nc != null&& nc.mPoolShow)
        //    {
        //        effect.SetActive(true);
        //    }
        //}
        
        
        //{
        //    NcDuplicator item = effect.GetComponentInChildren<NcDuplicator>();
        //    if (item != null)
        //    {
        //        LogManager.Instance.Log(effect.name, " NcDuplicator cannot be replayed.");
        //        return;
        //    }
        //}
        {
            /*
            NcSpriteAnimation[] list = effect.GetComponentsInChildren<NcSpriteAnimation>(true);
            for (int i = 0; i < list.Length; i++)
            {
                if (list[i] != null)
                {
                    list[i].ResetAnimation();
                }    
            }
            */
            //NcSpriteAnimation nc = effect.GetComponent<NcSpriteAnimation>();
            
            //if (nc != null)
            //{
            //    nc.ResetAnimation();
            //}

        }

        {
            /*
            NcCurveAnimation[] list = effect.GetComponentsInChildren<NcCurveAnimation>(true);
            for (int i = 0; i < list.Length; i++)
            {
                if (list[i] != null)
                {
                    list[i].ResetAnimation();
                }
                    
            }
            */
            //NcCurveAnimation nc = effect.GetComponent<NcCurveAnimation>();
            
            //if (nc != null)
            //{
            //    nc.ResetAnimation();
            //}
        }

        {
            //NcDelayActive[] list = effect.GetComponentsInChildren<NcDelayActive>(true);
            //for (int i = 0; i < list.Length; i++)
            //{
                //                 if (list[i] != null)
                //                     list[i].ResetAnimation();
            //}
        }

        {
            /*
            NcUvAnimation[] list = effect.GetComponentsInChildren<NcUvAnimation>(true);
            for (int i = 0; i < list.Length; i++)
            {
                if (list[i] != null)
                    list[i].ResetAnimation();
            }
            */
            //NcUvAnimation nc = effect.GetComponent<NcUvAnimation>();
            //if (nc != null)
            //    nc.ResetAnimation();
        }

        {
            /*
            NcUvAnimation[] list = effect.GetComponentsInChildren<NcUvAnimation>(true);
            for (int i = 0; i < list.Length; i++)
            {
                if (list[i] != null)
                    list[i].ResetAnimation();
            }
            */
            //NcDelay nc = effect.GetComponent<NcDelay>();
            //if (nc != null)
            //    nc.ResetDelay();
        }
        {
            //NcAttachPrefab nc = effect.GetComponent<NcAttachPrefab>();//【注】这个脚本因为对象池的问题,暂时先弃用了
            //if (nc != null)
            //{
                //nc.ResetAnimation();
            //}
            //NcAttachPrefab[] list = effect.GetComponentsInChildren<NcAttachPrefab>(true);
            //for (int i = 0; i < list.Length; i++)
            //{
            //if (list[i] != null)
            //{
            //list[i].enabled = true;
            //list[i].ResetAnimation();
            //}

            //}
        }
//         {
//             Fx_TexturCoordsEffect[] list = oEffect.GetComponentsInChildren<Fx_TexturCoordsEffect>(true);
//             for (int i = 0; i < list.Length; i++)
//             {
//                 if (list[i] != null)
//                     list[i].ResetAnimation();
//             }
//         }
        {
            /*
            ParticleSystem[] list = effect.GetComponentsInChildren<ParticleSystem>(true);
            for (int i = 0; i < list.Length; i++)
            {
                ParticleSystem pSystem = list[i];
                if (pSystem != null)
                {
                    pSystem.Stop();
                    pSystem.Clear();
                    pSystem.time = 0;
                    pSystem.Play();
                }
            }
            */
        }
        {
            /*
            Animation[] list = effect.GetComponentsInChildren<Animation>(true);
            for (int i = 0; i < list.Length; i++)
            {
                Animation animation = list[i];
                if (animation == null)
                    continue;
                foreach (AnimationState anim in animation)
                {
                    anim.time = 0;
                }
                animation.Play();
            }
            */
        }
    }

    public static void ResumeEffect(Transform t)
    {
        //if(t.parent==null||t.parent.GetComponent<NcDelay>()==null)//处理和nc插件的回收冲突
        //{
        //    t.gameObject.SetActive(true);
        //}
        
        //int childCount = t.childCount;
        //if (childCount > 0)
        //{
        //    for (int i = 0; i < childCount; i++)
        //    {
        //        ResumeEffect(t.GetChild(i));
        //    }
        //}
        //ParticleSystem ps = t.GetComponent<ParticleSystem>();
        //if (ps != null)
        //{
        //    ps.Clear();
        //    ps.Play();
        //}

        //Animation anim = t.GetComponent<Animation>();
        //if (anim != null)
        //{
        //    foreach (AnimationState anims in anim)
        //    {
        //        anims.time = 0;
        //    }
        //    anim.Play();
        //}
        //ResetNcEffect(t.gameObject, false);
        
    }

#endregion

#region <>位置操作<>

    public static Vector3 GetRoundPositionXZ(Vector3 vec, float r)
    {
        Vector3 temp = Vector3.zero;
        float x = UnityEngine.Random.Range(-r, r);
        float z = UnityEngine.Random.Range(-r, r);

        temp.x = vec.x + x;
        temp.y = vec.y;
        temp.z = vec.z + z;

        return temp;
    }

    public static Quaternion GetRotationY(Quaternion q)
    {
        Quaternion temp = q;
        temp.x = temp.z = 0.0f;
        return temp;
    }

    public static Vector3 GetPosXZ(Vector3 vec)
    {
        Vector3 v = Vector3.zero;
        v.x = vec.x;
        v.y = 0.0f;
        v.z = vec.z;
        return v;
    }

    /// <summary>
    /// 获取navmesh地形指定点高度
    /// </summary>
    /// <param name="pos"></param>
    /// <returns></returns>
    public static float GetHeight(Vector3 pos)
    {
        pos.y = 100;
        RaycastHit hit;
        Physics.Raycast(pos, Vector3.down * 200, out hit);
        return hit.point.y;
    }

    /// <summary>
    /// 获取navmesh地形指定点高度
    /// </summary>
    /// <param name="pos"></param>
    /// <returns></returns>
    public static float GetHeight(float x, float z)
    {
        Vector3 pos = new Vector3(x, 100, z);
        RaycastHit hit;
        Physics.Raycast(pos, Vector3.down * 200, out hit);
        return hit.point.y;
    }

#endregion

#region<>坐标转化<>
    public static Vector2 World3D2Screen2D(GameObject go)
    {
        if (go == null)
        {
            LogManager.Instance.LogError("World3D2Screen2D() GameObject go is null");
            return Vector2.zero;
        }

        RectTransform rect = GUIManager.Instance.ExtendRectTransform;
        Canvas canvas = GUIManager.Instance.ExtendCanvas;
        Vector2 screenPos = Vector2.zero;
        if (Camera.main == null)
        {
            return screenPos;
        }

        Vector3 Vec3Pos = Camera.main.WorldToScreenPoint(go.transform.position);
        Vector2 screenVec2Pos = new Vector2(Vec3Pos.x, Vec3Pos.y);

        RectTransformUtility.ScreenPointToLocalPointInRectangle(rect, screenVec2Pos, canvas.worldCamera, out screenPos);
        return screenPos;
    }

    public static Vector3 GetMouseWorldPosition()
    {
        return  Camera.allCameras[1].ScreenToWorldPoint(Input.mousePosition);
    }
#endregion


#region <>陀螺仪相关<>

    /// <summary>
    /// 获取陀螺仪旋转
    /// </summary>
    /// <returns></returns>
    public static Quaternion GetGyroscopeRotation()
    {
        Quaternion fixRotation = Quaternion.identity;
        if (Screen.orientation == ScreenOrientation.Landscape ||
            Screen.orientation == ScreenOrientation.LandscapeLeft)
        {
            fixRotation = Quaternion.Euler(0, 0, -90);
        }
        else if (Screen.orientation == ScreenOrientation.LandscapeRight)
        {
            fixRotation = Quaternion.Euler(0, 0, 90);
        }

        Quaternion gyroscopeRotation = Input.gyro.attitude;
        gyroscopeRotation.z = -gyroscopeRotation.z;
        gyroscopeRotation.w = -gyroscopeRotation.w;

        return gyroscopeRotation * fixRotation;
    }

#endregion

#region <>手机震动<>

    /// <summary>
    /// 手机震动，时长固定0.5s（Unity默认值，无法修改）。
    /// </summary>
    public static void Vibrate()
    {
#if UNITY_IPHONE || UNITY_ANDROID
        Handheld.Vibrate();
#endif
    }

#endregion

#region <>与或操作<>

    /// <summary>
    /// 手机震动，时长固定0.5s（Unity默认值，无法修改）。
    /// </summary>
    public static int LuaAnd(int a, int b)
    {
        return a & b;
    }

    public static int LuaOr(int a, int b)
    {
        return a | b;
    }

#endregion

#region <>手机摇一摇<>

    /// <summary>
    /// 摇晃阈值
    /// </summary>
    private static float mCheckValue = 0.8f;

    /// <summary>
    /// 临时偏移
    /// </summary>
    private static Vector3 mDetalAcceleration;
    private static Vector3 mOldAcceleration;
    private static Vector3 mNewAcceleration;

    /// <summary>
    /// 摇晃后回调
    /// </summary>
    public delegate void ShakeDeviceDelegate();
    public static ShakeDeviceDelegate mShakeDeviceDelegate;

    public static void RegisterShakeDevice(ShakeDeviceDelegate callback)
    {
        mShakeDeviceDelegate = callback;
    }

    public static void UpdateShake()
    {
        if (mShakeDeviceDelegate == null)
        {
            return;
        }

        mNewAcceleration = Input.acceleration;
        mDetalAcceleration = mNewAcceleration - mOldAcceleration;
        mOldAcceleration = mNewAcceleration;

        if (mDetalAcceleration.x > mCheckValue ||
            mDetalAcceleration.y > mCheckValue ||
            mDetalAcceleration.z > mCheckValue)
        {
            mShakeDeviceDelegate();
        }
    }

    public static void UnregisterShakeDevice()
    {
        mShakeDeviceDelegate = null;
    }

#endregion

#region <>获取屏幕宽高<>

    public static int GetScreenHight() {
        return Screen.height;
    }
    public static int GetScreenWidth()
    {
        return Screen.width;
    }
    public static float GetScreenRate() {
        return ((Screen.height + 0f) / 750f) / ((Screen.width + 0f) / 1334f);
    }

    //根据不同的机型适配,计算出不同的屏幕需要位移的距离
    public static float GetScreenExpandLength (int recthight,bool adapt,float upvalue)
    {
        float scaleRate = 1f;
        if (adapt) {
            //if (PluginsManager.Instance.IsIPhoneX())
            //{
            //    scaleRate = 0.82f;
            //}
            //else
            {
               scaleRate = GetScreenRate();
            }
        }
        recthight = (int)(recthight * scaleRate);
        var panel = GameObject.Find("CanvasUI").GetComponent<RectTransform>();
        var hight = panel.sizeDelta.y;

        return upvalue * hight - (hight - recthight)/ 2 ;
    }
#endregion

    public static void GameGC()
    {
        Resources.UnloadUnusedAssets();
        System.GC.Collect();
    }

    private static int mPing = 3;
    private static int mPingTime = 50;
    public static void SetPing(int p)
    {
        mPingTime = Mathf.Min(p,2000);
        if (p < 80)
        {
            mPing = 3;
        }
        else if (p >= 80 && p < 160)
        {
            mPing = 2;
        }
        else
        {
            mPing = 1;
        }
        mLastPings.Add(mPingTime);
        if (mLastPings.Count > 10)
        {
            mLastPings.RemoveAt(0);
        }
    }

    private static List<int> mLastPings = new List<int>();//保存最近10次获取的ping值
    public static string GetPingTime()
    {
        return mPingTime.ToString();
    }
    public static int GetMeanValuePingTimeMS()
    {
        int ping = 0;
        int count = mLastPings.Count;
        for (int i = 0; i < mLastPings.Count; ++i)
        {
            ping += mLastPings[i];
        }

        return ping / count;
    }
    public static int GetPingTimeMS()
    {
        return mPingTime;
    }

    public static int GetPing()
    {
        return mPing;
    }

    public static void CaptureScreen()
    {
        string filename = StringBuilder(GetTotalSeconds(), ".png");
        string path =  Path.Combine(Application.persistentDataPath, filename);
        if (File.Exists(path))
        {
            File.Delete(path);
        }
        
        if (GlobalData.isiPhone)
        {
            ScreenCapture.CaptureScreenshot(filename);
        }
        else
        {
            ScreenCapture.CaptureScreenshot(path);
        }
    }

    /// <summary>
    /// 输入的字符是否超过最大数量
    /// </summary>
    /// <param name="text">文本</param>
    /// <param name="count">限制数量</param>
    /// <returns></returns>
    public static bool CheckOverLimitCharCount(string text, int count)
    {
        bool bRet = false;

        int chars = 0;

        for (int i = 0; i < text.Length; ++i)
        {
            char a = text[i];

            if (a <= 127)
            {
                chars += 1;
            }
            else if(a > 127 && a <= 2047)
            {
                chars += 2;
            }
            else
            {
                chars += 3;
            }
        }

        if (count >= chars)
        {
            bRet = false;
        }
        else
        {
            bRet = true;
        }

        return bRet;
    }

    public static bool MemoryWarning()
    {
        int totalSize = SystemInfo.systemMemorySize;
        int freeSize = totalSize;

#if UNITY_EDITOR
        freeSize = totalSize;
#elif UNITY_ANDROID && !UNITY_EDITOR

#elif UNITY_IPHONE

#endif
        if (totalSize <= 1024)
        {
            if (freeSize <= 50)
            {
                return true;
            }
        }
        else if (totalSize > 1024 && totalSize < 2048)
        {
            if (freeSize <= 100 || freeSize / totalSize <= 0.07f)
            {
                return true;
            }
        }
        else
        {
            return false;
        }
        return false;
    }


#region<>lua相关new的中转<>
    public static UnityEngine.Video.VideoClip GetVideoClip(UnityEngine.Object o)
    {
        return o as UnityEngine.Video.VideoClip;
    }

    public static void VideoPlayerAddListen(UnityEngine.Video.VideoPlayer player, UnityEngine.Video.VideoPlayer.EventHandler cb)
    {
        player.loopPointReached += cb;
    }

    /// <summary>
    /// 获取视频地址
    /// </summary>
    /// <param name="movieName"></param>
    /// <returns></returns>
    public static string PlayerCGByXCode(string movieName)
    {
        return GameTools.StringBuilder(Application.streamingAssetsPath, "/", movieName, ".mp4");
    }

    //public static void ScrollonValueChangedAddListen(UnityEngine.UI.ScrollRect scroll, VoidDelegate cb)
    //{
    //    scroll.onValueChanged.AddListener(delegate {  cb(); });
    //}

    //public static void DropDownOnValueChangedAddListen(UnityEngine.UI.Dropdown dropdown, IntDelegate cb)
    //{
    //    dropdown.onValueChanged.AddListener(delegate (int index) { cb(index); });
    //}

    //public static void RegisterActiveLight(CelShadingLight light)
    //{
    //    CelShadingLight.RegisterActiveLight(light);
    //}

#endregion

    #region <>对象操作<>

    public static void SafeDestroy(GameObject go)
    {
        if (go == null)
        {
            return;
        }
        
        GameObject.Destroy(go);
        go = null;
    }

    // 显示隐藏UI组件。
    public static void SafeActive(GameObject go, bool active)
    {
        if (go == null)
        {
            return;
        }

        go.transform.localScale = active ? Vector3.one : Vector3.zero;
    }

    // 显示隐藏UI组件。
    public static void SafeActiveByPos(GameObject go, bool active)
    {
        if (go == null)
        {
            return;
        }

        go.transform.localPosition = active ? Vector3.zero : Vector3.right * 9999;
    }

    /// <summary>
    /// 设置Renderer渲染顺序
    /// </summary>
    /// <param name="go"></param>
    /// <param name="sortIndex"></param>
    public static void SetRendererOrder(GameObject go, int sortIndex)
    {
        Renderer[] renderers = go.GetComponentsInChildren<Renderer>();

        foreach (Renderer renderer in renderers)
        {
            renderer.sortingOrder = sortIndex;
        }
    }

    /// <summary>
    /// 设置渲染顺序，不打乱go物件内部原来的渲染顺序
    /// </summary>
    /// <param name="go"></param>
    /// <param name="sortIndex"></param>
    public static void SetRendererOrderByParent(GameObject go, int sortIndex)
    {
        Renderer[] renderers = go.GetComponentsInChildren<Renderer>();

        foreach (Renderer renderer in renderers)
        {
            renderer.sortingOrder = renderer.sortingOrder % 100 + sortIndex;
        }
    }

    public static void AddGameObjectComponent(GameObject go, int sortIndex)
    {
        Canvas canvas = go.GetComponent<Canvas>();
        if(canvas == null)
        {
            canvas = go.AddComponent<Canvas>(); 
        }

        canvas.overrideSorting = true;
        canvas.sortingOrder = sortIndex + 10;

        GraphicRaycaster raycaster = go.GetComponent<GraphicRaycaster>();
        if(raycaster == null)
        {
            go.AddComponent<GraphicRaycaster>();
        }
       
        //go.AddComponent<EventTriggerListener>();
    }

    //public static void AddFlyToRole(GameObject go)
    //{
    //    if( UnitObjectManager.Instance.mRoleObject == null)
    //    {
    //        return;
    //    }
    //    FlyToRole flyRole = go.AddComponent<FlyToRole>();
    //    flyRole.target = UnitObjectManager.Instance.mRoleObject.Transform;
    //    //flyRole.Type = FlyToRole.FlyType.P15V15;
    //    flyRole.bDestroyObject = true;
    //}
#endregion

#region<>游戏品质设置<>
    //public static void SetQuality()
    //{
    //    string quality = PlayerPrefsManager.Instance.GameOptions_Quality;       
    //    switch (quality)
    //    {
    //        case GameDefine.GameQuality.Low:
    //            QualitySettings.SetQualityLevel(0);
    //            break;
    //        case GameDefine.GameQuality.Mid:
    //            QualitySettings.SetQualityLevel(1);
    //            break;
    //        case GameDefine.GameQuality.High:
    //            QualitySettings.SetQualityLevel(2);
    //            break;
    //    }
    //}

#endregion

#region <>身份证验证<>

    /// <summary>
    /// 身份证号码规则验证
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public static bool CheckID(string id)
    {
        if (string.IsNullOrEmpty(id))
        {
            return false;
        }

        string regex = "^[1-9]\\d{7}((0\\d)|(1[0-2]))(([0|1|2]\\d)|3[0-1])\\d{3}$|^[1-9]\\d{5}[1-9]\\d{3}((0\\d)|(1[0-2]))(([0|1|2]\\d)|3[0-1])\\d{3}([0-9]|X)$";
        System.Text.RegularExpressions.Regex mRegex = new System.Text.RegularExpressions.Regex(regex);

        return mRegex.IsMatch(id);
    }

    /// <summary>
    /// 身份证号码规则验证 并判断是否满足18岁
    /// </summary>
    /// <param name="id">0 - 没输入 1 - 输入的不合法 2 - 出生日期不合法 3 - 合法成年 4 - 未成年</param>
    /// <returns></returns>
    public static int CheckIdCardNo(string id)
    {
        if (string.IsNullOrEmpty(id))
        {
            return 0;
        }

        string regex = "^[1-9]\\d{7}((0\\d)|(1[0-2]))(([0|1|2]\\d)|3[0-1])\\d{3}$|^[1-9]\\d{5}[1-9]\\d{3}((0\\d)|(1[0-2]))(([0|1|2]\\d)|3[0-1])\\d{3}([0-9]|X)$";
        System.Text.RegularExpressions.Regex mRegex = new System.Text.RegularExpressions.Regex(regex);

        if (!mRegex.IsMatch(id))
        {
            return 1;
        }

        string timeStr = string.Empty;
        int year = 0;
        int month = 0;
        int day = 0;

        if (id.Length == 15)//肯定成年了
        {
            //timeStr = id.Substring(6, 11);
            //year = int.Parse(StringBuilder(19,timeStr.Substring(0,1)));
            //month = int.Parse(timeStr.Substring(2, 3));
            //day = int.Parse(timeStr.Substring(4, 5));
            return 3;
        }
        else if (id.Length == 18)
        {
            timeStr = id.Substring(6, 8);
            year = int.Parse(timeStr.Substring(0, 4));
            month = int.Parse(timeStr.Substring(4, 2));
            day = int.Parse(timeStr.Substring(6, 2));
        }

        //获取服务器时间，以服务器时间为准
        DateTime serverTime = TimeSyncManager.Instance.GetServerDataTime();

        if(year > serverTime.Year || month > 12 || month <= 0 || day > 31 || day <= 0)
        {
            return 2;
        }

        if(serverTime.Year - year == 18)
        {
            if(serverTime.Month == month)
            {
                if(serverTime.Day >= day)
                {
                    return 3;
                }
            }
            else if(serverTime.Month > month)
            {
                return 3;
            }
        }
        else if(serverTime.Year - year > 18)
        {
            return 3;
        }

        return 4;
    }

    /// <summary>
    /// 名字规则验证
    /// </summary>
    /// <param name="id">待验证的字符串</param>
    /// <returns>是中文返回true,否则false</returns>
    public static bool CheckChinaName(string name)
    {
        string regex = "^[\u4e00-\u9fa5]{2,10}$";

        System.Text.RegularExpressions.Regex mRegex = new System.Text.RegularExpressions.Regex(regex);

        return mRegex.IsMatch(name);
    }

    /// <summary>
    /// 判断unity对象是否为NULL
    /// </summary>
    /// <param name="o"></param>
    /// <returns></returns>
   public static bool IsNull(UnityEngine.Object o)
    {
        if (o != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// 停止NC动画帧
    /// </summary>
    /// <param name="ncObject">包含ncspriteanimation的对象</param>
    /// <param name="stopIndex">停止到的帧</param>
    //public static void StopNcSpriteAnimation(GameObject ncObject, int stopIndex)
    //{
    //    NcSpriteAnimation npa = ncObject.GetComponent<NcSpriteAnimation>();
    //    npa.m_PlayMode = NcSpriteAnimation.PLAYMODE.SELECT;
    //    npa.SetSelectFrame(stopIndex);
    //}

#endregion
}