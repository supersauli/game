using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
//----------------------------------------------
//            UGUI: kit 技能按键的模拟双摇杆 更加简洁高效的实现方式
// Copyright © swg.sz liugr
//----------------------------------------------
[XLua.Hotfix]
[XLua.LuaCallCSharp]
public class UGUIDragComponent : MonoBehaviour , IPointerDownHandler, IPointerUpHandler, IDragHandler, IPointerEnterHandler,IMoveHandler
{
    public delegate void OnDragUp();
    public OnDragUp onDragUp = null;
    public Vector2 LocalPosition { get; private set; }
    public bool Pressed { get; private set; }

    public void OnPointerEnter(PointerEventData eventData)
    {
        LogManager.Instance.LogError("OnPointerEnter");
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        Pressed = true;
        LogManager.Instance.LogError("OnPointerDown");

    }
    public void OnPointerUp(PointerEventData eventData)
    {
        Pressed = false;
        if (onDragUp != null)
        {
            onDragUp();
        }
    }
    public void OnDrag(PointerEventData eventData)
    {
        LocalPosition = eventData.position - eventData.pressPosition ;
        //Debug.LogError(LocalPosition);
    }

    public void OnMove(AxisEventData eventData)
    {

    }
}
