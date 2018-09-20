#region 程序集 UnityEngine.UI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// D:\Naruto\Program\Client\Trunk\Library\UnityAssemblies\UnityEngine.UI.dll
#endregion

using System;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace UnityEngine.UI
{
    [XLua.Hotfix]
    [XLua.LuaCallCSharp]
    public class UGUIInputField : InputField
    {
        //private Text mPlaceTex = null;
        //private Text mContentTex = null;

        public override void OnDeselect(BaseEventData eventData)
        {
            base.OnDeselect(eventData);
            if (textComponent.text == string.Empty)
            {
                placeholder.gameObject.SetActive(true);
            }
        }
        public override void OnSelect(BaseEventData eventData)
        {
            base.OnSelect(eventData);
            placeholder.gameObject.SetActive(false);
        }
    }
}