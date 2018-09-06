using UnityEngine;
using System.Collections.Generic;
using TinyBinaryXml;

public class TextData : IConfigData
{
    private static Dictionary<string, string> mTextDic = new Dictionary<string, string>();
    private static Dictionary<string, string> mCharacterDic = new Dictionary<string, string>();
    private static Dictionary<string, string> mPropDic = new Dictionary<string, string>();
    private static Dictionary<string, string> mSystemDic = new Dictionary<string, string>();
    private static Dictionary<string, string> mTipsDic = new Dictionary<string, string>();

    private static Dictionary<string, string> mQuestDic = new Dictionary<string, string>();

    public static Dictionary<long, TalkContentData> mTalkContentDataList = new Dictionary<long, TalkContentData>();

    public override void Init()
    {
        IResManager.Instance.LoadAsset(GameTools.StringBuilder(ResourcesDefine.mConfigPath, "uistring"), LoadUIStringComplete, null, IResExtend.Bytes, false);
    }

    public void LoadUIStringComplete(Object o, object obj)
    {
        TextAsset text = o as TextAsset;
        if (text == null)
        {
            return;
        }

        mTextDic.Clear();
        TbXmlNode doc = TbXml.Load(text.bytes).docNode;
        List<TbXmlNode> list = doc.GetNodes("uistrings/uistring");
        foreach (TbXmlNode node in list)
        {
            string id = node.GetStringValue("ID");
            string str = node.GetStringValue("Value");
            if (!mTextDic.ContainsKey(id))
            {
                mTextDic.Add(id, str);
            }
        }

        IResManager.Instance.LoadAsset(GameTools.StringBuilder(ResourcesDefine.mConfigPath, "queststring"), LoadQuestStringComplete, null, IResExtend.Bytes, false);
        IResManager.Instance.LoadAsset(GameTools.StringBuilder(ResourcesDefine.mConfigPath, "namestring"), LoadCharacterStringComplete, null, IResExtend.Bytes, false);
        IResManager.Instance.LoadAsset(GameTools.StringBuilder(ResourcesDefine.mConfigPath, "propstring"), LoadPropStringComplete, null, IResExtend.Bytes, false);
        IResManager.Instance.LoadAsset(GameTools.StringBuilder(ResourcesDefine.mConfigPath, "systemstring"), LoadSystemStringComplete, null, IResExtend.Bytes, false);
        IResManager.Instance.LoadAsset(GameTools.StringBuilder(ResourcesDefine.mConfigPath, "talk_content"), LoadTalkContentDataComplete, null, IResExtend.Bytes);
    }

    public void LoadPropStringComplete(Object o, object obj)
    {
        TextAsset text = o as TextAsset;
        if (text == null)
        {
            return;
        }

        mPropDic.Clear();
        TbXmlNode doc = TbXml.Load(text.bytes).docNode;
        List<TbXmlNode> list = doc.GetNodes("propstrings/propstring");
        foreach (TbXmlNode node in list)
        {
            string id = node.GetStringValue("ID");
            string str = node.GetStringValue("Value");
            if (mPropDic.ContainsKey(id))
            {
                LogManager.Instance.LogError("propstring.xml Repeat ID:", id);
                continue;
            }

            mPropDic.Add(id, str);
        }
    }

    public void LoadCharacterStringComplete(Object o, object obj)
    {
        TextAsset text = o as TextAsset;
        if (text == null)
        {
            return;
        }

        mCharacterDic.Clear();
        TbXmlNode doc = TbXml.Load(text.bytes).docNode;
        List<TbXmlNode> list = doc.GetNodes("namestrings/namestring");
        foreach (TbXmlNode node in list)
        {
            string id = node.GetStringValue("ID");
            string str = node.GetStringValue("Value");
            if (mCharacterDic.ContainsKey(id))
            {
                LogManager.Instance.LogError("namestring.xml Repeat ID:", id);
                continue;
            }

            mCharacterDic.Add(id, str);
        }
    }

    public void LoadQuestStringComplete(Object o, object obj)
    {
        TextAsset text = o as TextAsset;
        if (text == null)
        {
            return;
        }

        mQuestDic.Clear();
        TbXmlNode doc = TbXml.Load(text.bytes).docNode;
        List<TbXmlNode> list = doc.GetNodes("queststrings/queststring");
        foreach (TbXmlNode node in list)
        {
            string id = node.GetStringValue("ID");
            string str = node.GetStringValue("Value");
            if (mQuestDic.ContainsKey(id))
            {
                LogManager.Instance.LogError("queststring.xml Repeat ID:", id);
                continue;
            }

            mQuestDic.Add(id, str);
        }
    }

    public static void LoadSystemStringComplete(Object o, object obj)
    {
        TextAsset text = o as TextAsset;
        if (text == null)
        {
            return;
        }

        mTipsDic.Clear();
        mSystemDic.Clear();
        TbXmlNode doc = TbXml.Load(text.bytes).docNode;
        List<TbXmlNode> list = doc.GetNodes("systemstrings/systemstring");
        foreach (TbXmlNode node in list)
        {
            string id = node.GetStringValue("ID");
            string str = node.GetStringValue("Value");
            if (mSystemDic.ContainsKey(id))
            {
                LogManager.Instance.LogError("systemstring.xml Repeat ID:", id);
                continue;
            }

            if (!mTipsDic.ContainsKey(id) && id.StartsWith("UI_Tips_"))
            {
                mTipsDic.Add(id, str);
                continue;
            }

            mSystemDic.Add(id, str);
        }
    }

    public static string[] symbols = { "{0}", "{1}", "{2}", "{3}", "{4}", "{5}", "{6}" };
    public static string GetTextByLua(string id)
    {
        var text = GetText(id);
        text = text.Replace("%", "%%");
        text = text.Replace("[", "<");
        text = text.Replace("]", ">");
        text = text.Replace("\\n", "\n");

        foreach (var symbol in symbols)
        {
            text = text.Replace(symbol, "%s");
        }
        return text;
    }

    public static string GetTextByLuaNoPercent(string id)
    {
        var text = GetText(id);
        text = text.Replace("[", "<");
        text = text.Replace("]", ">");
        text = text.Replace("\\n", "\n");

        foreach (var symbol in symbols)
        {
            text = text.Replace(symbol, "%s");
        }
        return text;
    }

    public static string GetPropByLua(string id)
    {
        var text = GetPropText(id);
        text = text.Replace("%", "%%");
        text = text.Replace("[", "<");
        text = text.Replace("]", ">");
        text = text.Replace("\\n", "\n");
        foreach (var symbol in symbols)
        {
            text = text.Replace(symbol, "%s");
        }
        return text;
    }

    public static string GetLuaTextByNoSharp(string id)
    {
        var text = GetText(id);
        text = text.Replace("%", "%%");
        text = text.Replace("\\n", "\n");

        foreach (var symbol in symbols)
        {
            text = text.Replace(symbol, "%s");
        }
        return text;

    }

    public static string GetCharacterByLua(string id)
    {
        var text = GetCharacterText(id);
        text = text.Replace("%", "%%");
        text = text.Replace("[", "<");
        text = text.Replace("]", ">");
        text = text.Replace("\\n", "\n");
        foreach (var symbol in symbols)
        {
            text = text.Replace(symbol, "%s");
        }
        return text;
    }

    public static string GetSystemByLua(string id)
    {
        var text = GetSystemText(id);
        text = text.Replace("%", "%%");
        text = text.Replace("[", "<");
        text = text.Replace("]", ">");
        text = text.Replace("\\n", "\n");
        foreach (var symbol in symbols)
        {
            text = text.Replace(symbol, "%s");
        }
        return text;
    }

    public static string GetTipsText(string id)
    {
        string text = id;
        mTipsDic.TryGetValue(id, out text);
        if (string.IsNullOrEmpty(text))
        {
            text = id;
        }
        return text;
    }

    public static string GetTipsByLua(string id)
    {
        var text = GetTipsText(id);
        text = text.Replace("%", "%%");
        text = text.Replace("[", "<");
        text = text.Replace("]", ">");
        text = text.Replace("\\n", "\n");
        foreach (var symbol in symbols)
        {
            text = text.Replace(symbol, "%s");
        }
        return text;
    }

    public static string GetTipsRandom()
    {
        if (mTipsDic == null || mTipsDic.Count < 1)
        {
            return GlobalData.UITips;
        }

        int size = Random.Range(1, mTipsDic.Count);
        string id = GameTools.StringBuilder("UI_Tips_", size);
        string text = GetTipsByLua(id);
        if (string.IsNullOrEmpty(text))
        {
            text = GlobalData.UITips;
        }

        return text;
    }

    public static string[] questSymbols = { "{Name}" };
    public static string GetQuestByLua(string id)
    {
        var text = GetQuestText(id);
        text = text.Replace("%", "%%");
        text = text.Replace("[", "<");
        text = text.Replace("]", ">");
        text = text.Replace("\\n", "\n");
        foreach (var questSymbol in questSymbols)
        {
            switch (questSymbol)
            {
                case "{Name}":
                    {
                        //RoleObject mRole = UnitObjectManager.Instance.mRoleObject;
                        //if (mRole != null)
                        //{
                        //    string name = mRole.DataObject.QueryPropString("Name");
                        //    text = text.Replace(questSymbol, name);
                        //}
                    }
                    break;
                default:
                    break;
            }
        }
        foreach (var symbol in symbols)
        {
            text = text.Replace(symbol, "%s");
        }
        return text;
    }

    public static string GetText(string id)
    {
        string text = id;
        mTextDic.TryGetValue(id, out text);

        if (string.IsNullOrEmpty(text))
        {
            text = id;
        }
        return text;
    }

    public static string GetCSharpText(string id)
    {
        string text = id;
        mTextDic.TryGetValue(id, out text);

        if (string.IsNullOrEmpty(text))
        {
            text = id;
        }
        text = text.Replace("%", "%%");
        text = text.Replace("[", "<");
        text = text.Replace("]", ">");
        text = text.Replace("\\n", "\n");
        return text;
    }

    public static string GetPropTextByLua(string id)
    {
        var text = GetPropText(id);
        text = text.Replace("%", "%%");
        text = text.Replace("[", "<");
        text = text.Replace("]", ">");
        text = text.Replace("\\n", "\n");

        foreach (var symbol in symbols)
        {
            text = text.Replace(symbol, "%s");
        }
        return text;
    }
    public static string GetPropText(string id)
    {
        //        Debug.Log("GetPropText " + id);
        string text = id;
        mPropDic.TryGetValue(id, out text);

        if (string.IsNullOrEmpty(text))
        {
            text = id;
        }

        return text;
    }
    public static string GetCharacterText(string id)
    {
        string text = id;
        mCharacterDic.TryGetValue(id, out text);

        if (string.IsNullOrEmpty(text))
        {
            text = id;
        }

        return text;
    }

    public static string GetCharacterTextUseColor(string id)
    {
        string text = id;

        mCharacterDic.TryGetValue(id, out text);
        if (string.IsNullOrEmpty(text))
        {
            text = id;
        }
        else
        {
            text = text.Replace("[", "<");
            text = text.Replace("]", ">");
            text = text.Replace("\\n", "\n");
        }
        return text;
    }

    public static string GetSystemText(string id)
    {
        string text = id;
        mSystemDic.TryGetValue(id, out text);

        if (string.IsNullOrEmpty(text))
        {
            text = id;
        }

        return text;
    }

    public static string GetQuestText(string id)
    {
        string text = id;
        mQuestDic.TryGetValue(id, out text);

        if (string.IsNullOrEmpty(text))
        {
            text = id;
        }

        return text;
    }

    //// 利用C#的接口格式化字符串
    //public static string FormatText(string format, LuaTable table)
    //{
    //    format = format.Replace("[", "<");
    //    format = format.Replace("]", ">");
    //    string result = "";
    //    string[] param = new string[table.Length];
    //    for (int i = 1; i <= table.Length; i++)
    //    {
    //        param[i - 1] = table.Get<int, string>(i);

    //    }
    //    result = string.Format(format, param);
    //    return result;
    //}

    public static void LoadTalkContentDataComplete(Object o, object obj)
    {
        TextAsset text = o as TextAsset;
        if (text == null)
        {
            return;
        }

        mTalkContentDataList.Clear();
        TbXmlNode doc = TbXml.Load(text.bytes).docNode;
        List<TbXmlNode> talkcontentList = doc.GetNodes("talk_contents/talk_content");
        foreach (var subTalkContentDataNode in talkcontentList)
        {
            TalkContentData talkContentData = new TalkContentData();
            talkContentData.ID = subTalkContentDataNode.GetLongValue("ID");
            talkContentData.LastTime = subTalkContentDataNode.GetFloatValue("LastTime");
            var talkcontentitemList = subTalkContentDataNode.GetNodes("talk_content_item");
            foreach (var subTalkContentItemDataNode in talkcontentitemList)
            {
                TalkContentItemData talkContentItemData = new TalkContentItemData();
                talkContentItemData.ID = subTalkContentItemDataNode.GetLongValue("ID");
                talkContentItemData.Content = subTalkContentItemDataNode.GetStringValue("Content");
                talkContentData.TalkContentItemDataList[talkContentItemData.ID] = talkContentItemData;
            }
            mTalkContentDataList[talkContentData.ID] = talkContentData;
        }
    }

    public static TalkContentData GetTalkContentData(long id)
    {
        TalkContentData item = null;
        if (mTalkContentDataList.TryGetValue(id, out item))
        {
        }
        return item;
    }
}

public class TalkContentData
{
    public long ID;
    public float LastTime;
    public Dictionary<long, TalkContentItemData> TalkContentItemDataList = new Dictionary<long, TalkContentItemData>();
    public string GetText()
    {
        var index = Random.Range(1, TalkContentItemDataList.Count + 1);
        return TextData.GetText(TalkContentItemDataList[index].Content); ;
    }
    public string GetTextByLua()
    {
        var index = Random.Range(1, TalkContentItemDataList.Count + 1);
        return TextData.GetTextByLua(TalkContentItemDataList[index].Content); ;
    }
}
public class TalkContentItemData
{
    public long ID;
    public string Content;
}

