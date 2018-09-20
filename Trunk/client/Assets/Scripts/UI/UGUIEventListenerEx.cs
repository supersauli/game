using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

using UnityEngine.EventSystems;
//----------------------------------------------
//            UGUI: kit
// Copyright © sf.sz wangzk
//----------------------------------------------
[XLua.Hotfix]
[XLua.LuaCallCSharp]
public class UGUIEventListenerEx : UGUIEventListener, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public GameObjectVectorDelegate onBeginDrag;//拖拽开始
    public GameObjectDelegate onEndDrag;//拖拽结束
    public GameObjectVectorDelegate onDrag;//拖拽ing

    static public new UGUIEventListenerEx Get(GameObject go)
    {
        UGUIEventListenerEx listener = go.GetComponent<UGUIEventListenerEx>();
        if (listener == null) listener = go.AddComponent<UGUIEventListenerEx>();
        return listener;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (onBeginDrag != null) onBeginDrag(gameObject, eventData.delta);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (this.sound != null)
        {
            this.sound.OnDragOut();
        }
        if (onEndDrag != null) { onEndDrag(gameObject); }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (onDrag != null) onDrag(gameObject, eventData.delta);
    }

}