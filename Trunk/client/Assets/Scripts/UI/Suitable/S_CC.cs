using UnityEngine;

[XLua.Hotfix]
[XLua.LuaCallCSharp]
public class S_CC : MonoBehaviour
{
    RectTransform rect = null;
    public bool mScale = false;
    //private int mScreenWidth = 0;
    //private int mScreenHeight = 0;
    void Start()
    {
        rect = GetComponent<RectTransform>();
        Suitable.SDEFINE.Suit(rect, mScale, 0);
    }

}
