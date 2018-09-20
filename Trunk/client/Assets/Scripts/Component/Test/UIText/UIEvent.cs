using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
  
public class UIEvent : MonoBehaviour,IPointerClickHandler
{
    public int _id;
    public  void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("OnClick" + _id);
    }
    
    public void OnPointerClickEx(PointerEventData eventData)
    {
        Debug.Log("OnClickEx" + _id);
        // 偏移
        //GetComponent<RectTransform>().anchoredPosition = new Vector2(1.0f,1.0f);
    }

}
