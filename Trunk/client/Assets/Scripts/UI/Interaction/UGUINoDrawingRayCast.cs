using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

/// <summary>
/// UGUI 无可视图形的事件检测
/// </summary>
[XLua.Hotfix]
[XLua.LuaCallCSharp]
public class UGUINoDrawingRayCast : Graphic
{
    public override void SetMaterialDirty()
    {}

    public override void SetVerticesDirty()
    {}

    [Obsolete("This function is obsolete")]
    protected override void OnFillVBO(List<UIVertex> vbo)
    {
        vbo.Clear();
    }
}
