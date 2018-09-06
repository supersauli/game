using UnityEngine;
using System.Collections.Generic;
//using TinyBinaryXml;

public class UIPanelData : MonoBehaviour
{
    public static bool UIDatarLoaded()
    {
        if (uiDatarLoaded)
        {
            return true;
        }

        return false;
    }
    public static Dictionary<string, PanelData> mUIPanelDic = new Dictionary<string, PanelData>();
    private static bool uiDatarLoaded = false;
    public static void Init()
    {
        if (uiDatarLoaded) return;

        //IResManager.Instance.LoadAsset(GameTools.StringBuilder(ResourcesDefine.mConfigPath, "uipanel"), LoadUIPanelComplete, null, IResExtend.Bytes,false);
    }
    private static void LoadUIPanelComplete(Object o, object obj)
    {
        //TextAsset text = o as TextAsset;
        //if (text == null)
        //{
        //    return;
        //}
        //uiDatarLoaded = true;

        //TbXmlNode doc = TbXml.Load(text.bytes).docNode;
        //List<TbXmlNode> list = doc.GetNodes("UIPanel/UI");

        //foreach (TbXmlNode node in list)
        //{
        //    string uiName = node.GetStringValue("Name");
      
        //    PanelData item = new PanelData();
        //    item.layer = node.GetIntValue("Layer");
        //    item.order = node.GetIntValue("SortingOrder");
        //    item.index = node.GetIntValue("DepthIndex");
        //    item.useLua = node.GetIntValue("UseLua") == 1;
        //    item.unique = node.GetIntValue("Unique") == 1;
        //    item.ShowClear = node.GetIntValue("ShowClear");
        //    item.HideRestore = node.GetIntValue("HideRestore");
        //    item.popType = node.GetIntValue("PopType");
        //    item.Fixed = node.GetIntValue("Fixed");
        //    item.closeWhenSwitchScene = node.GetIntValue("CloseWhenSwitchScene", 1) == 1;
        //    item.suitpx = node.GetIntValue("suitpx", 0) == 1 ? true : false;
        //    item.suitipad = node.GetIntValue("suitipad", 0) == 1 ? true : false;
        //    //item.suitwh18 = node.GetIntValue("suitwh18", 0) == 1 ? true : false;
        //    item.ScreeningDestruction = node.GetIntValue("ScreeningDestruction");
        //    item.limitLevel = node.GetIntValue("limitLevel");
        //    item.cgClose = node.GetIntValue("CGClose", 1) == 1;
        //    item.DisConnectClose = node.GetIntValue("DisConnectClose", 1) == 1;
        //    mUIPanelDic[uiName] = item;
        //}
    }
    public static PanelData GetUIPanel(string uiName)
    {
        PanelData mvdata = null;

        if (mUIPanelDic.TryGetValue(uiName, out mvdata))
        {
            return mvdata;
        }

        return null;
    }

}


public class PanelData
{
    public int layer;
    public int index;
    public int order;
    public bool useLua;
    public bool unique;
    public int popType;
    public bool closeWhenSwitchScene;
    public int ShowClear;
    public int HideRestore;
    public int Fixed;
    public bool suitpx = false;
    public bool suitipad = false;
    public int ScreeningDestruction;
    public bool cgClose = false;
    public int limitLevel;
    public bool DisConnectClose = true;
}
