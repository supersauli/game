using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[XLua.Hotfix]
[XLua.LuaCallCSharp]
[AddComponentMenu("LocalizationLabel")]
public class LocalizationLabel : MonoBehaviour
{

    public enum FromType
    {
        UIString,
        CharacterString,
        QuestString,
    }
    public FromType type = FromType.UIString;
    public string Key;
    public void Start()
    {
        Text label = GetComponent<Text>();
        if (label != null)
        {
            string txt = string.Empty;
            switch (type)
            {
                case FromType.UIString:
                    txt = TextData.GetTextByLua(Key);
                    break;
                case FromType.CharacterString:
                    txt = TextData.GetCharacterText(Key);
                    break;
                case FromType.QuestString:
                    txt = TextData.GetQuestByLua(Key);
                    break;
                default:
                    break;
            }
            label.text = txt;
        }
    }

}
