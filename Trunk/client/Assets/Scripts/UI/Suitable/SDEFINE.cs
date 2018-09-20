using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Suitable
{
    [XLua.Hotfix]
    [XLua.LuaCallCSharp]
    public class SDEFINE
    {
        public static int PX_LT_OFFSET = 44;
        public static int PX_LC_OFFSET = 44;
        public static int PX_LB_OFFSET = 44;
        public static int PX_CT_OFFSET = 0;
        public static int PX_CB_OFFSET = 0;
        public static int PX_RT_OFFSET = -44;
        public static int PX_RC_OFFSET = -44;
        public static int PX_RB_OFFSET = -44;


        public static float mStandRate = 1.78f;
        public static int mStandWidth = 1334;
        public static int mStandHeight = 750;


        public static void Suit(RectTransform rect, bool scale, int offset)
        {
            if (scale)
            {
                float mScreenWidth = Mathf.Max(Screen.width, Screen.height);
                float mScreenHeight = Mathf.Min(Screen.width, Screen.height);
                float wRate = (Suitable.SDEFINE.mStandWidth + 0f) / (mScreenWidth + 0f);
                float hRate = (Suitable.SDEFINE.mStandHeight + 0f) / (mScreenHeight + 0f);
                float scaleRate = wRate / hRate;

                //float rateWH = mScreenWidth / mScreenHeight;

                if (PluginsManager.Instance.IsIPhoneX())
                {
                    if (rect != null)
                    {
                        rect.localScale = new Vector3(0.82f, 0.82f, 1f);
                    }
                }
                if (GlobalData.isAndroid)
                {
                    if (rect != null)
                    {
                        rect.localScale = new Vector3(scaleRate, scaleRate, 1f);
                    }
                }
            }
            if (offset != 0)
            {
                rect.anchoredPosition = new Vector2(offset, 0);
            }
        }
    }
}

