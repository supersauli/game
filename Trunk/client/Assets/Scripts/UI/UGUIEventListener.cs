using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

using UnityEngine.EventSystems;
//----------------------------------------------
//            UGUI: kit
// Copyright © sf.sz liugr
//----------------------------------------------
[XLua.Hotfix]
[XLua.LuaCallCSharp]
public class UGUIEventListener : MonoBehaviour ,IPointerClickHandler,IPointerDownHandler , IPointerEnterHandler,IPointerExitHandler,IPointerUpHandler,ISelectHandler, IUpdateSelectedHandler
{
    //public delegate void VoidDelegate (GameObject go);
    //public delegate void VectorDelegate(GameObject go,Vector2 vec);
    //public delegate void BoolDelegate(GameObject go, bool b);

    public GameObjectDelegate onClick;//点击事件回调
	public GameObjectDelegate onEnter;//点击和悬浮
	public GameObjectDelegate onExit;//离开事件

    public GameObjectBoolDelegate onPress;//press事件
    public GameObjectBoolDelegate onPressDown;//按下
    public GameObjectBoolDelegate onPressUp;  // 抬起

    //public VectorDelegate onPressVector;//press事件
    //public VoidDelegate onDown;
    //public VoidDelegate onUp;

    public GameObjectDelegate onSelect;
	public GameObjectDelegate onUpdateSelect;

    public bool OpenLongPress = false;

    public float interval = 0.5f;
    public float longPressDelay = 1.0f;
    private bool isTouchDown = false;
    private float touchBegin = 0;
    private float lastInvokeTime = 0;

    public UGUIPlaySound sound
    {
        get;
        set;
    }
    public UGUIButtonScale bs
    {
        get;
        set;
    }
    public void SetIsTouchDown(bool b)
    {
        isTouchDown = b;
    }
    public static void SetSelectedGameObject(GameObject go)
    {
        EventSystem.current.SetSelectedGameObject(go);
    }

    static public UGUIEventListener Get(GameObject go)
	{
        UGUIEventListener listener = go.GetComponent<UGUIEventListener>();
        if (listener == null) listener = go.AddComponent<UGUIEventListener>();
		return listener;
	}

    public void OnPointerClick(PointerEventData eventData)
	{

        if (this.sound != null)
        {
            this.sound.OnClick();
        }
        if (onClick != null)
        {
            onClick(gameObject);
        }
    }
	public void OnPointerDown (PointerEventData eventData){
		//if(onDown != null) onDown(gameObject);

        if (this.sound != null)
        {
            this.sound.OnPress();
        }
        
        if (this.bs != null)
        {
            this.bs.SendMessage("OnPress", SendMessageOptions.DontRequireReceiver);
        }
        //if (onPressVector != null) { onPressVector(eventData.pressPosition); }
        if (OpenLongPress)
        {
            touchBegin = Time.time;
            isTouchDown = true;
            
        }
        else
        {
            if (onPress != null) { onPress(gameObject, true); }
            if (onPressDown != null) { onPressDown(gameObject, true); }
        }
    }
	public void OnPointerEnter (PointerEventData eventData){
        
        if (this.sound != null)
        {
            this.sound.OnEnter();
        }
       
        if (onEnter != null) { onEnter(gameObject); LogManager.Instance.Log("on enter"); }
        /*
        if (this.bs != null)
        {
            this.bs.SendMessage("OnEnter", SendMessageOptions.DontRequireReceiver);
        }
         * */
    }
	public void OnPointerExit (PointerEventData eventData){
        
        if (this.sound != null)
        {
            this.sound.OnExit();
        }
        
        if (onExit != null) { onExit(gameObject); LogManager.Instance.Log("on onExit"); }
        /*
        if (this.bs != null)
        {
            this.bs.SendMessage("OnExit", SendMessageOptions.DontRequireReceiver);
        }
         ** */

        if (OpenLongPress)
        {
            isTouchDown = false;
        }
    }
	public void OnPointerUp (PointerEventData eventData){
        //if(onUp != null) onUp(gameObject);

        if (this.bs != null)
        {
            this.bs.SendMessage("OnUp", SendMessageOptions.DontRequireReceiver);
        }

        if (OpenLongPress)
        {
            isTouchDown = false;
        }
        else
        {
            if (onPress != null) onPress(gameObject, false);
            if (onPressUp != null) onPressUp(gameObject, false);
        }
    }
	public void OnSelect (BaseEventData eventData){
		if(onSelect != null) onSelect(gameObject);
	}
	public void OnUpdateSelected (BaseEventData eventData){
		if(onUpdateSelect != null) onUpdateSelect(gameObject);
	}

    public void Update()
    {
        if (!OpenLongPress)
        {
            return;
        }

        if (isTouchDown)
        {
            if ((Time.time - touchBegin) > longPressDelay)
            {
                if ((Time.time - lastInvokeTime) > interval)
                {
                    if (onPress != null) onPress(gameObject, false);
                    lastInvokeTime = Time.time;
                }
            }
        }
    }
}

[XLua.LuaCallCSharp]
public enum ListenerType
{
    Normal,
    Extend,
}