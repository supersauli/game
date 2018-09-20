using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

[XLua.Hotfix]
[XLua.LuaCallCSharp]
[RequireComponent(typeof(Text))]
public class UGUITextWriter : MonoBehaviour
{
    private Text mText;
    public string TextValue;
    public float DuringTime;

    private void Start()
    {
        mText = GetComponent<Text>();
        mText.DOKill();
        StartWriter();
    }
    
    private void OnEnable()
    {
        if (mText == null)
        {
            mText = GetComponent<Text>();
        }
        
        mText.text = string.Empty;
        mText.DOKill();
        StartWriter();   
    }

    private void OnDisable()
    {
        mText.text = string.Empty;
        mText.DOKill();
    }

    private void StartWriter()
    {
        if (string.IsNullOrEmpty(TextValue))
        {
            return;
        }

        int length = TextValue.Length;
        mText.DOText(TextValue, length * DuringTime);
    }

    private void OnDestroy()
    {
        mText.DOKill();
        mText = null;
    }
}