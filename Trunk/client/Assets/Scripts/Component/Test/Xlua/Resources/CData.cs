using UnityEngine;
using UnityEditor;

public class CData
{
    private int _value;

    public  void PrintValue()
    {
        Debug.Log("Value" + _value);
    }
    public void SetValue(int value)
    {
         _value = value;
        Debug.Log("SetValue" + value);
        Show();
    }

    public static void Show()
    {
        Debug.Log("Show");
    }
};