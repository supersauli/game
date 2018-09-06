using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using SysUtils;
using UnityEngine.Video;

[DisallowMultipleComponent]
public class IView : MonoBehaviour, IDataView, IRecord, ITable
{
    private GUIPopEnum ViewPopEnum = GUIPopEnum.Enum_None;

    private GUILayerDepth mViewDepth = GUILayerDepth.GLD_POPUP;
    private bool mUnique = false;
    private bool mInShow = false;
    private int mViewIndex = -1;
    private int mViewOrder = -1;

    private Canvas mSelfCanvas;
    private int mSortOrder;

    //private LuaTable mScript;
    private PanelData mUIConf;
    private bool mUseLua = false;


    private static Dictionary<string, object> mUIData = new Dictionary<string, object>();
    public static object GetUIData(string uiName)
    {
        if (!mUIData.ContainsKey(uiName))
        {
            return null;
        }
        return mUIData[uiName];
    }

    public static void SetUIData(string uiName, object data)
    {
        if (!mUIData.ContainsKey(uiName))
        {
            mUIData.Add(uiName, data);
        }
        else
        {
            mUIData[uiName] = data;
        }
    }
    private float ViewPopDuation = 1.0f;
    public GUIPopEnum GetViewPopEnum()
    {
        return ViewPopEnum;
    }

    public GUILayerDepth ViewDepth
    {
        set { mViewDepth = value; }
        get { return mViewDepth; }
    }

    public int ViewOrder
    {
        set { mViewOrder = value; }
        get { return mViewOrder; }
    }

    public bool Unique
    {
        set { mUnique = value; }
        get { return mUnique; }
    }

    public bool InShow
    {
        set { mInShow = value; }
        get { return mInShow; }
    }

    public int ViewIndex
    {
        set { mViewIndex = value; }
        get { return mViewIndex; }
    }

    public int ShowClear
    {
        set; get;
    }

    public int HideRestore
    {
        set;get;
    }

    public bool ClosePanelWhenCG
    {
        set;get;
    }

    public int Fixed
    {
        set;get;
    }

    public Canvas SelfCanvas
    {
        set { mSelfCanvas = value; }
        get { return mSelfCanvas; }
    }

    public int SortOrder
    {
        set
        {
            mSortOrder = value;
            if(mSelfCanvas == null)
            {
                return;
            }
            mSelfCanvas.sortingOrder = value;

            if (mLuaSortOrder != null)
            {
                mLuaSortOrder();
            }
        }
        get
        {
            return mSortOrder;
        }
    }

    public void SetViewPopEnum(GUIPopEnum en, float duration = 0.3f)
    {
        ViewPopEnum = en;
        ViewPopDuation = duration;
    }
    public float GetViewPopDuration()
    {
        return ViewPopDuation;
    }
    public GameObject ViewObject
    {
        get;
        set;
    }

    public Transform ViewTransform
    {
        get;
        set;
    }

    public RectTransform ViewRectTransform
    {
        get;
        set;
    }

    public object ViewData
    {
        get;
        set;
    }

    public string ViewName
    {
        get;
        set;
    }

    public void SetPanelConfig(PanelData data)
    {
        mUIConf = data;
        
        if (mUIConf != null)
        {
            ViewPopEnum = (GUIPopEnum)data.popType;
            Unique = data.unique;
            ViewDepth = (GUILayerDepth)data.layer;
            ViewIndex = data.index;
            ViewOrder = data.order;
            ShowClear = data.ShowClear;
            HideRestore = data.HideRestore;
            ClosePanelWhenCG = data.cgClose;
            Fixed = data.Fixed;
        }

        if (mUIConf != null && mUIConf.useLua)
        {
            mUseLua = true;
        }
    }

    private Action mLuaStart;
    private Action<int> mLuaStartInt;
    private Action mLuaUpdate;
    private Action mLuaDestroy;
    private Action mLuaShow;
    private Action mLuaHide;
    private Action mLuaResume;
    private Action mLuaSortOrder;
    private Action mLuaPosShow;
    private Action mLuaPosHide;

    private Action<string, int> mOnCreateView;
    private Action<string> mOnDeleteView;
    private Action<string, int> mOnViewProperty;
    private Action<string, string> mOnViewAdd;
    private Action<string, string> mOnViewRemove;
    private Action<string, string, int> mOnViewObjectProperty;
    private Action<string, string, string> mOnViewObjectPropertyChange;

    private Action<int> mOnRecordTable;
    private Action<string, int, int> mOnRecordAddRow;
    private Action<string, int> mOnRecordRemoveRow;
    private Action<string, int> mOnRecordBeforeRemoveRow;
    protected Action<string, int> mOnRecordGrid;
    protected Action<string, int, int> mOnRecordSingleGrid;
    private Action<string> mOnRecordClear;

    public delegate void OnTableAddRow(string tableName, VarList args, int iRows, int Cols, int startIndex);
    public delegate void OnTableSingleChange(string tableName, VarList args, int iRow, int Col, int Rows, int Cols, int startIndex);
    public delegate void OnTableChange(string tableName, VarList args, int iRow, int Rows, int Cols, int startIndex);
    public delegate void OnTableDeleteRow(string tableName, VarList args, int iRow, int Rows, int Cols, int startIndex);
    public delegate void OnTableClear(string tableName, VarList args, int startIndex);

    private OnTableAddRow mOnTableAddRow;
    private OnTableSingleChange mOnTableSingleChange;
    private OnTableChange mOnTableChange;
    private OnTableDeleteRow mOnTableDeleteRow;
    private OnTableClear mOnTableClear;

    public virtual void OnPreView()
    {
        if (mUseLua)
        {
            //string luaName = GameTools.StringBuilder(this.name, "Ctrl");
            ////string uilua = LuaManager.Instance.GetUILua(luaName);
            ////mScript = LuaManager.Instance.Lua.NewTable();
            ////LuaTable meta = LuaManager.Instance.Lua.NewTable();
            //meta.Set("__index", LuaManager.Instance.Lua.Global);
            ////mScript.SetMetaTable(meta);
            //meta.Dispose();
            ////mScript.Set("self", this);
            ////LuaManager.Instance.Lua.DoString(uilua, luaName, mScript);
            ////mScript.Get("start", out mLuaStart);
            //mScript.Get("startint", out mLuaStartInt);
            //mScript.Get("update", out mLuaUpdate);
            //mScript.Get("ondestroy", out mLuaDestroy);
            //mScript.Get("onshow", out mLuaShow);
            //mScript.Get("onhide", out mLuaHide);
            //mScript.Get("onresume", out mLuaResume);
            //mScript.Get("sortorder", out mLuaSortOrder);

            //mScript.Get("viewcreate", out mOnCreateView);
            //mScript.Get("viewdelete", out mOnDeleteView);
            //mScript.Get("viewadd", out mOnViewAdd);
            //mScript.Get("viewremove", out mOnViewRemove);
            //mScript.Get("viewprop", out mOnViewProperty);
            //mScript.Get("viewobjectprop", out mOnViewObjectProperty);
            //mScript.Get("viewobjectpropchange", out mOnViewObjectPropertyChange);


            //mScript.Get("recordtable", out mOnRecordTable);
            //mScript.Get("recordaddrow", out mOnRecordAddRow);
            //mScript.Get("recordremoverow", out mOnRecordRemoveRow);
            //mScript.Get("recordbeforeremoverow", out mOnRecordBeforeRemoveRow);
            //mScript.Get("recordgrid", out mOnRecordGrid);
            //mScript.Get("recordsinglegrid", out mOnRecordSingleGrid);
            //mScript.Get("recordclear", out mOnRecordClear);

            //mScript.Get("tableaddrow", out mOnTableAddRow);
            //mScript.Get("tablesinglechange", out mOnTableSingleChange);
            //mScript.Get("tablechange", out mOnTableChange);
            //mScript.Get("tabledeleterow", out mOnTableDeleteRow);
            //mScript.Get("tableclear", out mOnTableClear);

            //mScript.Get("posshow", out mLuaPosShow);
            //mScript.Get("poshide", out mLuaPosHide);

            //LuaManager.Instance.AddUITable(this.name, mScript);
        }
    }

    public virtual void OnStart(params object[] args)
    {
        //LogManager.Instance.Log(ViewObject, " OnStart");

        if (mLuaStart != null)
        {
            mLuaStart();
        }

        if (mLuaStartInt != null)
        {
            if (args.Length < 2)
            {
                return;
            }
            if (args[1].GetType() != typeof(int))
            {
                return;
            }
            int arg = (int)args[1];
            mLuaStartInt(arg);
        }
    }

    public T GetChild<T>(string path) where T : Component
    {
        if (string.IsNullOrEmpty(path))
        {
            return default(T);
        }

        Transform child = ViewTransform.Find(path);
        if (child == null)
        {
            return default(T);
        }

        return child.GetComponent<T>();
    }

    #region <>获取各种组件<> start

    public Transform GetChildTransform(string path)
    {
        return GetChild<Transform>(path);
    }

    public RectTransform GetChildRectTransform(string path)
    {
        return GetChild<RectTransform>(path);
    }

    public Image GetChildImage(string path)
    {
        return GetChild<Image>(path);
    }

    public Text GetChildText(string path)
    {
        return GetChild<Text>(path);
    }

    public Button GetChildButton(string path)
    {
        return GetChild<Button>(path);
    }

    public InputField GetChildInputField(string path)
    {
        return GetChild<InputField>(path);
    }

    public Slider GetChildSlider(string path)
    {
        return GetChild<Slider>(path);
    }

    public GameObject GetChildGameObject(string path)
    {
        if (string.IsNullOrEmpty(path))
        {
            return null;
        }

        Transform child = ViewTransform.Find(path);
        if (child == null)
        {
            return null;
        }

        return child.gameObject;
    }

    #endregion <>获取各种组件<> end


    public virtual void OnShow()
    {
        mInShow = true;

        //LogManager.Instance.Log(ViewObject, " OnShow");

        //PlotManager.Instance.CheckPlotTrigger("4", this.name);

        //UGUIPlayBGM bgm = mSelfCanvas.GetComponent<UGUIPlayBGM>();
        //if (bgm != null)
        //{
        //    bgm.PlayBGM();
        //}

        //if (mLuaShow != null)
        //{
        //    mLuaShow();
        //}
    }

    public virtual void OnPosShow()
    {
        if (mLuaPosShow != null)
        {
            mLuaPosShow();
        }
    }

    public virtual void OnPosHide()
    {
        if (mLuaPosHide != null)
        {
            mLuaPosHide();
        }
    }

    public virtual void OnHide()
    {
        mInShow = false;
        //UGUIPlayBGM bgm = mSelfCanvas.GetComponent<UGUIPlayBGM>();
        //if (bgm != null)
        //{
        //    bgm.StopBGM();
        //}
        ////LogManager.Instance.Log(ViewObject, " OnHide_" + this.name);
        ////PlotManager.Instance.CheckPlotTrigger("3", this.name);

        //if (mLuaHide != null)
        //{
        //    mLuaHide();
        //}
    }

    public virtual void OnResume()
    {
        if (mLuaResume != null)
        {
            mLuaResume();
        }
    }

    public virtual void RegisterEvent()
    {

    }

    public virtual void UnRegisterEvent()
    {
        
    }

    public virtual void OnUpdate()
    {
        if (mLuaUpdate != null)
        {
            mLuaUpdate();
        }
    }

    public virtual void OnDestroy()
    {
        //LogManager.Instance.Log("OnDestroy");
        //UGUIPlayBGM bgm = mSelfCanvas.GetComponent<UGUIPlayBGM>();
        //if (bgm != null)
        //{
        //    bgm.StopBGM();
        //}
        //if (mLuaDestroy != null)
        //{
        //    mLuaDestroy();
        //}

        //if (LuaManager.Instance != null)
        //{
        //    LuaManager.Instance.RemoveUITable(ViewName);
        //}

        mLuaStart = null;
        mLuaUpdate = null;
        mLuaDestroy = null;
        mLuaShow = null;
        mLuaHide = null;

        mOnCreateView = null;
        mOnDeleteView = null;
        mOnViewProperty = null;
        mOnViewAdd = null;
        mOnViewRemove = null;
        mOnViewObjectProperty = null;
        mOnViewObjectPropertyChange = null;

        mOnRecordTable = null;
        mOnRecordAddRow = null;
        mOnRecordRemoveRow = null;
        mOnRecordBeforeRemoveRow = null;
        mOnRecordGrid = null;
        mOnRecordSingleGrid = null;
        mOnRecordClear = null;

        mOnTableAddRow = null;
        mOnTableSingleChange = null;
        mOnTableChange = null;
        mOnTableDeleteRow = null;
        mOnTableClear = null;

        //if (mScript != null)
        //{
        //    mScript.Dispose();
        //}
    }

    /// <summary>
    /// 创建视图
    /// </summary>
    /// <param name="view_id"></param>
    /// <param name="capacity"></param>
    public void on_create_view(string view_id, int capacity)
    {
        if (mOnCreateView != null)
        {
            mOnCreateView(view_id, capacity);
        }
    }

    /// <summary>
    /// 删除视图
    /// </summary>
    /// <param name="view_id"></param>
    public void on_delete_view(string view_id)
    {
        if (mOnDeleteView != null)
        {
            mOnDeleteView(view_id);
        }
    }

    /// <summary>
    /// 视图变动
    /// </summary>
    /// <param name="view_id"></param>
    /// <param name="count"></param>
    public void on_view_property(string view_id, int count)
    {
        if (mOnViewProperty != null)
        {
            mOnViewProperty(view_id, count);
        }
    }

    /// <summary>
    /// 视图增加对象
    /// </summary>
    /// <param name="view_id"></param>
    /// <param name="obj_index">索引</param>
    public void on_view_add(string view_id, string obj_index)
    {
        if (mOnViewAdd != null)
        {
            mOnViewAdd(view_id, obj_index);
        }
    }

    /// <summary>
    /// 视图删除对象
    /// </summary>
    /// <param name="view_id"></param>
    /// <param name="obj_index">索引</param>
    public void on_view_remove(string view_id, string obj_index)
    {
        if (mOnViewRemove != null)
        {
            mOnViewRemove(view_id, obj_index);
        }
    }

    /// <summary>
    /// 视图内对象变化
    /// </summary>
    /// <param name="view_id"></param>
    /// <param name="obj_index">索引</param>
    /// <param name="count"></param>
    public void on_view_object_property(string view_id, string obj_index, int count)
    {
        if (mOnViewObjectProperty != null)
        {
            mOnViewObjectProperty(view_id, obj_index, count);
        }
    }

    /// <summary>
    /// 视图对象属性变化
    /// </summary>
    /// <param name="view_id"></param>
    /// <param name="obj_index"></param>
    /// <param name="name"></param>
    public void on_view_object_property_change(string view_id, string obj_index, string name)
    {
        if (mOnViewObjectPropertyChange != null)
        {
            mOnViewObjectPropertyChange(view_id, obj_index, name);
        }
    }

    public void on_record_table(int nRecordCount)
    {
        if (mOnRecordTable != null)
        {
            mOnRecordTable(nRecordCount);
        }
    }

    /// <summary>
    /// 表格新增一行
    /// </summary>
    /// <param name="strRecordName"></param>
    /// <param name="nRow"></param>
    /// <param name="nRows"></param>
    public void on_record_add_row(string strRecordName, int nRow, int nRows)
    {
        if (mOnRecordAddRow != null)
        {
            mOnRecordAddRow(strRecordName, nRow, nRows);
        }
    }

    /// <summary>
    /// 表格删除一行
    /// </summary>
    /// <param name="strRecordName"></param>
    /// <param name="nRow"></param>
    public void on_record_remove_row(string strRecordName, int nRow)
    {
        if (mOnRecordRemoveRow != null)
        {
            mOnRecordRemoveRow(strRecordName, nRow);
        }
    }

    public void on_record_remove_row_before(string strRecordName, int nRow)
    {
        if(mOnRecordBeforeRemoveRow != null)
        {
            mOnRecordBeforeRemoveRow(strRecordName, nRow);
        }
    }

    /// <summary>
    /// 表格变动
    /// </summary>
    /// <param name="strRecordName"></param>
    /// <param name="nCount"></param>
    public void on_record_grid(string strRecordName, int nCount)
    {
        if (mOnRecordGrid != null)
        {
            mOnRecordGrid(strRecordName, nCount);
        }
    }

    /// <summary>
    /// 表格变动一格
    /// </summary>
    /// <param name="strRecordName"></param>
    /// <param name="nRow"></param>
    /// <param name="nCol"></param>
    public void on_record_single_grid(string strRecordName, int nRow, int nCol)
    {
        if (mOnRecordSingleGrid != null)
        {
            mOnRecordSingleGrid(strRecordName, nRow, nCol);
        }
    }

    /// <summary>
    /// 表格清空
    /// </summary>
    /// <param name="strRecordName"></param>
    public void on_record_clear(string strRecordName)
    {
        if (mOnRecordClear != null)
        {
            mOnRecordClear(strRecordName);
        }
    }

    /// <summary>
    /// 虚表增加
    /// </summary>
    /// <param name="tableName"></param>
    /// <param name="args"></param>
    /// <param name="iRows"></param>
    /// <param name="Cols"></param>
    /// <param name="startIndex"></param>
    public void on_table_add(string tableName, VarList args, int iRows, int Cols, int startIndex)
    {
        if (mOnTableAddRow != null)
        {
            mOnTableAddRow(tableName, args, iRows, Cols, startIndex);
        }
    }

    /// <summary>
    /// 虚表单个改变
    /// </summary>
    /// <param name="tableName"></param>
    /// <param name="args"></param>
    /// <param name="iRow"></param>
    /// <param name="Col"></param>
    /// <param name="Rows"></param>
    /// <param name="Cols"></param>
    /// <param name="startIndex"></param>
    public void on_table_change_single(string tableName, VarList args, int iRow, int Col, int Rows, int Cols, int startIndex)
    {
        if (mOnTableSingleChange != null)
        {
            mOnTableSingleChange(tableName, args, iRow, Col, Rows, Cols, startIndex);
        }
    }

    /// <summary>
    /// 虚表多个改变
    /// </summary>
    /// <param name="tableName"></param>
    /// <param name="args"></param>
    /// <param name="iRow"></param>
    /// <param name="Rows"></param>
    /// <param name="Cols"></param>
    /// <param name="startIndex"></param>
    public void on_table_change(string tableName, VarList args, int iRow, int Rows, int Cols, int startIndex)
    {
        if (mOnTableChange != null)
        {
            mOnTableChange(tableName, args, iRow, Rows, Cols, startIndex);
        }
    }

    /// <summary>
    /// 虚表删除行
    /// </summary>
    /// <param name="tableName"></param>
    /// <param name="args"></param>
    /// <param name="iRow"></param>
    /// <param name="Rows"></param>
    /// <param name="Cols"></param>
    /// <param name="startIndex"></param>
    public void on_table_delete(string tableName, VarList args, int iRow, int Rows, int Cols, int startIndex)
    {
        if (mOnTableDeleteRow != null)
        {
            mOnTableDeleteRow(tableName, args, iRow, Rows, Cols, startIndex);
        }
    }

    /// <summary>
    /// 虚表清空
    /// </summary>
    /// <param name="tableName"></param>
    /// <param name="args"></param>
    /// <param name="startIndex"></param>
    public void on_table_clear(string tableName, VarList args, int startIndex)
    {
        if (mOnTableClear != null)
        {
            mOnTableClear(tableName, args, startIndex);
        }
    }
}