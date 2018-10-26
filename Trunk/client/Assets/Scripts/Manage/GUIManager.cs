using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.Video;
public enum GUIPopEnum
{
    Enum_None,
    Enum_Scale,
    Enum_L2R,
    Enum_R2L,
    Enum_T2B,
    Enum_B2T
}


public enum GUILayerDepth
{
    GLD_FIXED,
    GLD_POPUP,
    GLD_TOP
}

[XLua.Hotfix]
[XLua.LuaCallCSharp]
public class GUIManager : MonoBehaviour
{
    private Transform mParent;
    //private RectTransform mUICanvasRect = null;
    private Canvas mUICanvas = null;
    //private Transform mPreviewMask;
    //private Image mTargetImage;
    private List<IView> mLastPopupShows = new List<IView>();
    private List<IView> mLastExceptTopShows = new List<IView>();
    private List<IView> mLastAllShows = new List<IView>();
    private List<IView> mShowingView = new List<IView>();

    private List<string> mDelayHideView = new List<string>();
    private List<string> mDelayShowView = new List<string>();
    private Dictionary<string, IView> mViewNameDic = new Dictionary<string, IView>();

    private List<IView> mSortView = new List<IView>();
    private List<IView> mPopView = new List<IView>();
    private List<IView> mTopView = new List<IView>();
    private string mTopUIName = "";
    private int defaultSortIndex = 4000;

    public Transform Parent
    {
        get
        {
            return mParent;
        }
        set
        {
            mParent = value.Find("CanvasUI");
            //mUICanvasRect = mParent.GetComponent<RectTransform>();
            mUICanvas = mParent.GetComponent<Canvas>();
            //ExtendCanvas = value.Find("CanvasExtend").GetComponent<Canvas>();
            //ExtendRectTransform = ExtendCanvas.GetComponent<RectTransform>();
            //mPreviewMask = value.Find("CanvasExtend/mask");
            //mTargetImage = value.Find("CanvasExtend/target").GetComponent<Image>();
        }
    }


    public void SetInputAnchorPosition(RectTransform rect)
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 pos;
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(rect, Input.mousePosition, mUICanvas.worldCamera, out pos))
            {
                rect.anchoredPosition = pos;
            }
        }
    }

    public Canvas ExtendCanvas
    {
        get;
        set;
    }

    public RectTransform ExtendRectTransform
    {
        get;
        set;
    }

    public string topUIName
    {
        get
        {
            return mTopUIName;
        }
        private set
        {
            mTopUIName = value;
        }
    }

    public void ShowTargetUI(bool show, Vector2 p)
    {

    }

    private static GUIManager _Instance = null;
    public static GUIManager Instance
    {
        get { return _Instance; }
    }

    void Awake()
    {
        _Instance = this;
       Parent = GameObject.Find("UIRoot").transform;
    }

    private Vector3 orignPos = Vector3.zero;
    private Vector3 hidePos = new Vector3(9999, 9999, 0);
    public void CreateView(string uiName, bool flag = true)
    {
        LogManager.Instance.Log("CreateView:", uiName);
        if (string.IsNullOrEmpty(uiName))
        {
            return;
        }
        IView ui = GetView(uiName);
        if (ui != null)
        {
            if (!ui.InShow)
            {
                ShowView(uiName);
            }

            return;
        }
        //PanelData data = UIPanelData.GetUIPanel(uiName);
        //if (UnitObjectManager.Instance.mRoleObject != null)
        //{
        //    int currentLevel = UnitObjectManager.Instance.mRoleObject.DataObject.QueryPropInt(ClientConstDefine.Level);
        //    if (currentLevel < data.limitLevel)
        //    {
        //        string text = string.Format(TextData.GetCSharpText("UI_Shop_17"), data.limitLevel);
        //        ClientEventManager.Instance.DispatchEvent(ClientEventDefine.TipsPanel_ShowTip, text);
        //        return;
        //    }
        //}
        string path = "Prefab/UI/";
        //if (PluginsManager.Instance.IsIPhoneX())
        //{
        //    if (data.suitpx)
        //    {
        //        path = GameTools.StringBuilder(ResourcesDefine.mPXUIPath, uiName);
        //    }
        //    else
        //    {
        //        path = GameTools.StringBuilder(ResourcesDefine.mUIPath, uiName);
        //    }
        //}
        //else if (PluginsManager.Instance.IsIpad())
        //{
        //    if (data.suitipad)
        //    {
        //        path = GameTools.StringBuilder(ResourcesDefine.mIPADUIPath, uiName);
        //    }
        //    else
        //    {
        //        path = GameTools.StringBuilder(ResourcesDefine.mUIPath, uiName);
        //    }
        //}
        //else
        //{
        //    path = GameTools.StringBuilder(ResourcesDefine.mUIPath, uiName);
        //}

        path += uiName;

        ArrayList list = new ArrayList();
        list.Add(uiName);
        list.Add(flag);
        IResManager.Instance.LoadUIAsset(path, LoadUIComplete, list);
    }

    private void LoadUIComplete(Object o, object obj)
    {
        ArrayList list = (ArrayList)obj;
        string uiName = (string)list[0];
        bool flag = (bool)list[1];
        object[] args = new object[0];//list[1] as object[];
        bool hide = false;
        //if (args.Length > 0)
        {
            //hide = (bool)args[0];
        }
        if (o == null)
        {
            return;
        }
        GameObject newui = GameObject.Instantiate(o, Vector3.zero, Quaternion.identity) as GameObject;
        if (newui == null)
        {
            return;
        }



        newui.transform.SetParent(mParent, false);
        RectTransform rect = newui.GetComponent<RectTransform>();
        rect.localScale = Vector3.one;
        newui.name = uiName;
        IView view = null;

        Canvas canvas = newui.AddComponent<Canvas>();

        canvas.pixelPerfect = false;
        //var raycaster = newui.AddComponent<GraphicRaycaster>();
        newui.AddComponent<GraphicRaycaster>();

        PanelData data = UIPanelData.GetUIPanel(uiName);

        bool useLua = true;

        if (data != null)
        {
            useLua = data.useLua;
        }
        else
        {
            LogManager.Instance.LogError("uipanel config error");
        }
        if (useLua)
        {
            view = newui.AddComponent<IView>();
        }
        else
        {
            view = newui.AddComponent(System.Type.GetType(uiName)) as IView;
        }

        view.ViewName = uiName;
        view.SetPanelConfig(data);
        view.SelfCanvas = canvas;
        view.ViewObject = newui;
        view.ViewTransform = newui.transform;
        view.ViewRectTransform = rect;
        view.OnPreView();
        view.OnStart(args);
        view.RegisterEvent();
        canvas.overrideSorting = true;
        view.SortOrder = defaultSortIndex;// + data.layer * 1000 + data.index * 10;//view.ViewOrder;

        if (!mViewNameDic.ContainsKey(uiName))
        {
            mViewNameDic[uiName] = view;
        }

        if (!hide)
        {
            ShowView(uiName);
        }
        else
        {
            view.ViewRectTransform.localPosition = hidePos;
        }

        if (flag)
        {
            ShowPanelAnimation(view);
        }
        /*switch (view.GetViewPopEnum())
        {
            case GUIPopEnum.Enum_Scale:
                rect.localScale = new Vector3(0.1f, 0.1f, 1f);
                view.GetComponent<RectTransform>().DOScale(Vector3.one, view.GetViewPopDuration()).SetEase(Ease.Linear);
                break;
            case GUIPopEnum.Enum_L2R:
                rect.localPosition = new Vector3(-Screen.width, 0f, 0f);
                rect.DOLocalMoveX(0f, view.GetViewPopDuration()).SetEase(Ease.Linear);
                break;
            case GUIPopEnum.Enum_R2L:
                rect.localPosition = new Vector3(Screen.width, 0f, 0f);
                rect.DOLocalMoveX(0f, view.GetViewPopDuration()).SetEase(Ease.Linear);
                break;
            case GUIPopEnum.Enum_T2B:
                rect.localPosition = new Vector3(0f, -Screen.height, 0f);
                rect.DOLocalMoveY(0f, view.GetViewPopDuration()).SetEase(Ease.Linear);
                break;
            case GUIPopEnum.Enum_B2T:
                rect.localPosition = new Vector3(0f, -50, 0f);
                rect.DOLocalMoveY(0f, view.GetViewPopDuration()).SetEase(Ease.Linear);

                break;
            default:
                break;
        }*/
        
        o = null;
    }

    //界面出来的动画
    public void ShowPanelAnimation(IView view) {
        switch (view.GetViewPopEnum())
        {
            case GUIPopEnum.Enum_Scale:
                //view.ViewRectTransform.localScale = new Vector3(0.1f, 0.1f, 1f);
                //view.GetComponent<RectTransform>().DOScale(Vector3.one, view.GetViewPopDuration()).SetEase(Ease.InOutBack);
                break;
            case GUIPopEnum.Enum_L2R:
                view.ViewRectTransform.localPosition = new Vector3(-Screen.width, 0f, 0f);
                view.ViewRectTransform.DOLocalMoveX(0f, view.GetViewPopDuration()).SetEase(Ease.InOutBack);
                break;
            case GUIPopEnum.Enum_R2L:
                view.ViewRectTransform.localPosition = new Vector3(Screen.width, 0f, 0f);
                view.ViewRectTransform.DOLocalMoveX(0f, view.GetViewPopDuration()).SetEase(Ease.InOutBack);
                break;
            case GUIPopEnum.Enum_T2B:
                view.ViewRectTransform.localPosition = new Vector3(0f, -Screen.height, 0f);
                view.ViewRectTransform.DOLocalMoveY(0f, view.GetViewPopDuration()).SetEase(Ease.InOutBack);
                break;
            case GUIPopEnum.Enum_B2T:
                view.ViewRectTransform.localPosition = new Vector3(0f, -50, 0f);
                view.ViewRectTransform.DOLocalMoveY(0f, view.GetViewPopDuration()).SetEase(Ease.InOutBack);

                break;
            default:
                break;
        }
    }
    /*
    public void CreateView<T>(params object[] args) where T : IView
    {
        bool hide = false;
        string uiName = typeof(T).ToString();

        if (string.IsNullOrEmpty(uiName))
        {
            return;
        }

        if (args.Length > 0)
        {
            hide = (bool)args[0];
        }

        IView ui = GetView(uiName);
        if (ui != null)
        {
            if (!hide)
            {
                ShowView(ui);
            }
            
            return;
        }

        string path = GameTools.StringBuilder(ResourcesDefine.mUIPath, uiName);
        
        IResManager.Instance.LoadAsset(path, (Object o, object obj) =>
        {
            if (o == null)
            {
                return;
            }

            GameObject newui = GameObject.Instantiate(o, Vector3.zero, Quaternion.identity) as GameObject;
            if (newui == null)
            {
                return;
            }

            newui.transform.SetParent(mParent, false);
            RectTransform rect = newui.GetComponent<RectTransform>();
            rect.localScale = Vector3.one;
            newui.name = uiName;
            IView view = null;
            if (GlobalData.useLua)
            {
                view = newui.AddComponent<IView>();
            }
            else
            {
                view = newui.AddComponent(System.Type.GetType(uiName)) as IView;
            }
            
            view.ViewObject = newui;
            view.ViewTransform = newui.transform;
            view.ViewRectTransform = rect;
            view.OnPreView();
            view.OnStart(args);
            view.RegisterEvent();
            if (!mViewNameDic.ContainsKey(uiName))
            {
                mViewNameDic[uiName] = view;
            }
            
            if (!hide)
            {
                ShowView(view);
            }
            else
            {
                view.ViewRectTransform.localPosition = hidePos;
            }

            switch (view.GetViewPopEnum())
            {
                case GUIPopEnum.Enum_Scale:
                    rect.localScale = new Vector3(0.1f, 0.1f, 1f);
                    view.GetComponent<RectTransform>().DOScale(Vector3.one, view.GetViewPopDuration()).SetEase(Ease.OutBack);
                    break;
                case GUIPopEnum.Enum_L2R:
                    rect.localPosition = new Vector3(-Screen.width, 0f, 0f);
                    rect.DOLocalMoveX(0f, view.GetViewPopDuration()).SetEase(Ease.OutBack);
                    break;
                case GUIPopEnum.Enum_R2L:
                    rect.localPosition = new Vector3(Screen.width, 0f, 0f);
                    rect.DOLocalMoveX(0f, view.GetViewPopDuration()).SetEase(Ease.OutBack);
                    break;
                case GUIPopEnum.Enum_T2B:
                    rect.localPosition = new Vector3(0f, -Screen.height, 0f);
                    rect.DOLocalMoveY(0f, view.GetViewPopDuration()).SetEase(Ease.OutBack);
                    break;
                case GUIPopEnum.Enum_B2T:
                    rect.localPosition = new Vector3(0f, Screen.height, 0f);
                    rect.DOLocalMoveY(0f, view.GetViewPopDuration()).SetEase(Ease.OutBack);

                    break;
                default:
                    break;
            }

            o = null;
            
        });
    }
    */

    public int GetViewSortOrder(string viewName)
    {
        IView view = GetView(viewName);

        if (view != null)
        {
            return view.SortOrder;
        }

        return -1;
    }

    public void AddToShowing(IView view)
    {
        if (!mShowingView.Contains(view))
        {
            mShowingView.Add(view);
        }
    }

    public void RemoveFormShowing(IView view)
    {
        if (mShowingView.Contains(view))
        {
            mShowingView.Remove(view);
        }
        for (int i = mShowingView.Count - 1; i >= 0; --i)
        {
            if (mShowingView[i] == null)
            {
                mShowingView.RemoveAt(i);
            }
        }
    }

    public void RemoveViewCache(IView view)
    {
        if (mViewNameDic.ContainsValue(view))
        {
            mViewNameDic.Remove(view.name);
        }

        if (mShowingView.Contains(view))
        {
            mShowingView.Remove(view);
        }

        if (mLastAllShows.Contains(view))
        {
            mLastAllShows.Remove(view);
        }

        if (mLastPopupShows.Contains(view))
        {
            mLastPopupShows.Remove(view);
        }
    }


    public bool HasView(string uiName)
    {
        return GetView(uiName) == null ? false : true;
    }

    public IView GetView(string uiName)
    {
        if (mViewNameDic.ContainsKey(uiName))
        {
            return mViewNameDic[uiName];
        }

        return null;
    }

    public bool InShow(string uiName)
    {
        IView view = GetView(uiName);

        if (view != null)
        {
            return view.InShow;
        }

        return false;
    }

    public void HideAllView()
    {
        return;
//         mLastAllShows.Clear();
//         IView[] views = mParent.GetComponentsInChildren<IView>();
//         for (int i = 0; i < views.Length; i++)
//         {
//             if (views[i].InShow)
//             {
//                 views[i].ViewRectTransform.anchoredPosition = hidePos;
//                 //views[i].OnHide();
//                 mLastAllShows.Add(views[i]);
//                 //RemoveFormShowing(views[i]);
//             }
//         }
    }

    public void ShowAllView()
    {
        foreach (IView view in mLastAllShows)
        {
            view.ViewRectTransform.anchoredPosition = orignPos;
            //view.OnShow();
            //AddToShowing(view);
        }

        mLastAllShows.Clear();

    }

    public void HideView(string uiName)
    {
        if (uiName == "LoadingPanel")
        {
            LogManager.Instance.Log("HideView LoadingPanel");
        }
        if (!HasView(uiName))
        {
            return;
        }

        IView view = GetView(uiName);
        if (view != null)
        {
            HideView(view);
        }
    }

    public void HideView(IView view)
    {
        view.OnHide();

        view.ViewRectTransform.anchoredPosition = hidePos;
        RemoveFormShowing(view);

        //if (view.Fixed != 1 && view.HideRestore == 1)
        //{
        //    ClientEventManager.Instance.DispatchEvent(ClientEventDefine.RestoreScreenPanel, null);
        //}

        //if (view.Unique)
        //{
        //    ShowAllPopup();
        //}

        //ClientEventManager.Instance.DispatchEvent(ClientEventDefine.Hide_Panel, view.ViewName);
    }

    public void ShowView(string uiName)
    {
        if (uiName == "LoadingPanel")
        {
            LogManager.Instance.Log("ShowView LoadingPanel");
        }
        //Debug.Log("<<<<<<<<<<<<<<ShowView1<<<<<<<<<<<<");
        IView view = GetView(uiName);

        if (InShow(uiName))
        {
            return;
        }

        if (view == null) return;

        view.OnShow();
        //Debug.Log("<<<<<<<<<<<<<<ShowView2<<<<<<<<<<<<");
        AddToShowing(view);
        ChangeViewDepth(view);
        view.ViewRectTransform.anchoredPosition = orignPos;

        if (view.Fixed != 1 && view.ShowClear == 1)
        {
            Invoke("DelayInvoke", 0.5f * Time.timeScale);//防止收到时间缩放的影响
        }


        //Debug.Log("<<<<<<<<<<<<<<ShowView3<<<<<<<<<<<<");
        if (view.Unique)
        {
            HideAllPopup();
        }

        //ClientEventManager.Instance.DispatchEvent(ClientEventDefine.Show_Panel, view.ViewName);
    }

    private void DelayInvoke()
    {
        if (this.IsInvoking("DelayInvoke"))
        {
            return;
        }

        //ClientEventManager.Instance.DispatchEvent(ClientEventDefine.ClearScreenPanel, null);
        //this.CancelInvoke("DelayInvoke");
    }
    public void OnUpdate()
    {
        for (int i = 0; i < mShowingView.Count; ++i)
        {
            if (mShowingView[i] == null || !mShowingView[i].InShow || !mShowingView[i].gameObject.activeSelf)
            {
                continue;
            }

            mShowingView[i].OnUpdate();
        }

        /*foreach (KeyValuePair<string, IView> kvp in mViewNameDic)
        {
            IView view = kvp.Value;
            if (view == null || !view.InShow)
            {
                continue;
            }

            view.OnUpdate();
        }*/
    }

    public void DestroyView(string uiName)
    {

        IView view = GetView(uiName);
        if (view == null)
        {
            return;
        }

        if (view.ViewObject == null)
        {
            return;
        }

        RemoveViewCache(view);

        if (view.Unique)
        {
            ShowAllPopup();
        }

        HideView(view);
        view.OnDestroy();
        GameObject.Destroy(view.ViewObject);
        view.ViewObject = null;
        view = null;
        //ClientEventManager.Instance.DispatchEvent(ClientEventDefine.DestroyPanel, uiName);
    }

    public void DestroyAllView()
    {
        for (int i = mShowingView.Count - 1; i >= 0; i--)
        {
            if (mShowingView.Count <= i)
            {
                continue;
            }

            IView ui = mShowingView[i];

            PanelData data = UIPanelData.GetUIPanel(ui.name);
            if (data == null)
            {
                continue;
            }
            if (ui.name == "LoadingPanel" || ui.name == "ItemPromptPanel")
                continue;
            DestroyView(ui.name);
        }
    }

    public void DestroyAllOpenView()
    {
        List<string> mAllList = new List<string>();
        foreach (var v in mViewNameDic)
        {
            mAllList.Add(v.Key);
        }

        foreach(string name in mAllList)
        {
            DestroyView(name);
        }
    }

    public void DestoryArenaView()
    {
        for (int i = mShowingView.Count - 1; i >= 0; i--)
        {
            if (mShowingView.Count <= i)
            {
                continue;
            }

            IView ui = mShowingView[i];

            PanelData data = UIPanelData.GetUIPanel(ui.name);
            if (data == null)
            {
                continue;
            }
            if (data.ScreeningDestruction == 1) {
                continue;
            }
            DestroyView(ui.name);
        }
    }

    //销毁杀星功能不需要的界面
    public void DestoryKillStarView()
    {
        for (int i = mShowingView.Count - 1; i >= 0; i--)
        {
            if (mShowingView.Count <= i)
            {
                continue;
            }

            IView ui = mShowingView[i];

            PanelData data = UIPanelData.GetUIPanel(ui.name);
            if (data == null)
            {
                continue;
            }
            if (data.layer == 1) {
                DestroyView(ui.name);
            }
        }
    }


    public void DestoryAllPopup()
    {
        for (int i = mShowingView.Count - 1; i >= 0; i--)
        {
            if (mShowingView.Count <= i)
            {
                continue;
            }

            IView ui = mShowingView[i];

            PanelData data = UIPanelData.GetUIPanel(ui.name);
            if (data == null)
            {
                continue;
            }
            if (mShowingView[i].ViewDepth == GUILayerDepth.GLD_POPUP)
            {
                DestroyView(ui.name);
            }
        }
    }


    public void DestroyViewWhenSwitchScene()
    {
        for (int i = mShowingView.Count - 1; i >= 0; i--)
        {
            if (mShowingView.Count <= i)
            {
                continue;
            }

            IView ui = mShowingView[i];
            PanelData data = UIPanelData.GetUIPanel(ui.name);
            if (data == null)
            {
                continue;
            }

            if (data.closeWhenSwitchScene)
            {
                DestroyView(ui.name);
            }
        }

    }

    public void DestroyViewWhenDisConnect()
    {
        for (int i = mShowingView.Count - 1; i >= 0; i--)
        {
            if (mShowingView.Count <= i)
            {
                continue;
            }

            IView ui = mShowingView[i];
            PanelData data = UIPanelData.GetUIPanel(ui.name);
            if (data == null)
            {
                continue;
            }

            if (data.DisConnectClose)
            {
                DestroyView(ui.name);
            }
        }
    }

    private void ChangeViewDepth(IView view)
    {
        GUILayerDepth depth = view.ViewDepth;

        mSortView.Clear();
        mPopView.Clear();
        mTopView.Clear();

        int fixedIndex = 0;
        int popIndex = 0;
        int topIndex = 0;


        for (int i = 0; i < mShowingView.Count; ++i)
        {
            IView iview = mShowingView[i];

            if (iview.ViewDepth == GUILayerDepth.GLD_FIXED)
            {
                fixedIndex++;
                mSortView.Add(iview);
            }
            else if (iview.ViewDepth == GUILayerDepth.GLD_POPUP)
            {
                popIndex++;
                mPopView.Add(iview);
            }
            else if (iview.ViewDepth == GUILayerDepth.GLD_TOP)
            {
                topIndex++;
                mTopView.Add(iview);
            }

        }

        mSortView.Sort(CompareIndex);
        mTopView.Sort(CompareIndex);

        for (int i = 0; i < mSortView.Count; ++i)
        {
            mSortView[i].SortOrder = defaultSortIndex + i * 1000;
        }

        for (int i = 0; i < popIndex; ++i)
        {
            int index = fixedIndex + i;
            mPopView[i].SortOrder = defaultSortIndex + index * 1000;
        }

        for (int i = 0; i < topIndex; ++i)
        {
            int index = fixedIndex + popIndex + i;
            mTopView[i].SortOrder = defaultSortIndex + index * 1000;
        }

        if (mPopView.Count > 0)
        {
            topUIName = mPopView[mPopView.Count - 1].ViewName;
        }
        else
        {
            if (mSortView.Count > 0)
            {
                topUIName = mSortView[mSortView.Count - 1].ViewName;
            }
        }
    }

    public void HideAllExpectTop()
    {
        mLastExceptTopShows.Clear();

        for (int i = mShowingView.Count - 1; i >= 0; --i)
        {
            if (mShowingView[i].ViewDepth <= GUILayerDepth.GLD_POPUP/* && mShowingView[i].InShow*/ && mShowingView[i].InShow)
            {

                SetViewVisible(mShowingView[i], false);
                mLastExceptTopShows.Add(mShowingView[i]);
                //RemoveFormShowing(mShowingView[i]);
            }
        }
    }

    public void ResumeEvent()
    {
        if (mViewNameDic == null)
        {
            return;
        }

        foreach (KeyValuePair<string, IView> kvp in mViewNameDic)
        {
            IView view = kvp.Value;
            if (view == null)
            {
                continue;
            }

            view.OnResume();
        }
    }

    public void ShowAllExpectTop()
    {
        foreach (IView view in mLastExceptTopShows)
        {
            if (view == null || view.ViewRectTransform == null)
            {
                continue;
            }

            SetViewVisible(view, true);
            //AddToShowing(view);
        }

        mLastExceptTopShows.Clear();
    }
    
    public void HideAllPopup()
    {
        mLastPopupShows.Clear();

        for (int i = mShowingView.Count - 1; i >= 0; --i)
        {
            if (mShowingView[i].ViewDepth == GUILayerDepth.GLD_POPUP)
            {
                SetViewVisible(mShowingView[i], false);
                mLastPopupShows.Add(mShowingView[i]);
                //RemoveFormShowing(mShowingView[i]);
            }

        }
    }

    public void HidePopupPanelExecptSpecify(string name)
    {
        mLastPopupShows.Clear();

        for (int i = mShowingView.Count - 1; i >= 0; --i)
        {
            if (mShowingView[i].name.Equals(name))
                continue;
            if (mShowingView[i].ViewDepth == GUILayerDepth.GLD_POPUP)
            {
                SetViewVisible(mShowingView[i], false);
                mLastPopupShows.Add(mShowingView[i]);
                //RemoveFormShowing(mShowingView[i]);
            }

        }
    }

    public void HideViewPosition(string name)
    {
        IView view = null;
        if (mViewNameDic.TryGetValue(name, out view))
        {
            if (!view.InShow)
            {
                return;
            }
            view.InShow = false;
            SetViewVisible(view, false);
            view.OnPosHide();
            if (view.ShowClear == 1)
            {
                //ClientEventManager.Instance.DispatchEvent(ClientEventDefine.RestoreScreenPanel, null);
            }
        }
    }

    public void ShowViewPosition(string name,bool flag = true)
    {
        IView view = null;
        if (mViewNameDic.TryGetValue(name, out view))
        {
            if (view.InShow)
            {
                return;
            }
            view.InShow = true;
            SetViewVisible(view, true);
            view.OnPosShow();
            if (flag) {
                ShowPanelAnimation(view);
            }
            AddToShowing(view);
            ChangeViewDepth(view);
            if (view.ShowClear == 1)
            {
                //ClientEventManager.Instance.DispatchEvent(ClientEventDefine.ClearScreenPanel, null);
            }
        }
    }

    public void ShowAllPopupPanel()
    {
        foreach (IView view in mLastPopupShows)
        {
            if (view == null || view.ViewRectTransform == null)
            {
                continue;
            }

            SetViewVisible(view, true);
            //AddToShowing(view);
        }

        mLastPopupShows.Clear();
    }

    public void AddDelayHideView(string uiName)
    {
        if (!mDelayHideView.Contains(uiName))
        {
            mDelayHideView.Add(uiName);
        }
    }

    public void HideDelayViews()
    {
        if (mDelayHideView.Count <= 0)
        {
            return;
        }

        foreach (string uiName in mDelayHideView)
        {
            IView view = GetView(uiName);

            if (view != null)
            {
                //if (view.InShow)
                {
                    HideView(view);
                }

            }

        }

        mDelayHideView.Clear();
    }

    public void AddDelayShowView(string uiName)
    {

        if (!mDelayShowView.Contains(uiName))
        {
            mDelayShowView.Add(uiName);
        }
    }

    private void SetViewVisible(IView view, bool showFlag)
    {
        if (showFlag)
        {
            if (view.Fixed != 1)
            {
                view.ViewRectTransform.anchoredPosition = orignPos;
            }
            else
            {
              //  ClientEventManager.Instance.DispatchEvent(ClientEventDefine.RestoreScreenPanel, null);
            }
            //ClientEventManager.Instance.DispatchEvent(ClientEventDefine.Show_Panel, view.ViewName);
        }
        else
        {
            if (view.Fixed != 1)
            {
                view.ViewRectTransform.anchoredPosition = hidePos;
            }
            else
            {
                //ClientEventManager.Instance.DispatchEvent(ClientEventDefine.ClearScreenPanel, null);
            }

            //ClientEventManager.Instance.DispatchEvent(ClientEventDefine.Hide_Panel, view.ViewName);
        }
    }

    public void ShowDelayViews()
    {
        if (mDelayShowView.Count <= 0)
        {
            return;
        }

        foreach (string uiName in mDelayShowView)
        {
            IView view = GetView(uiName);

            if (view != null)
            {
                SetViewVisible(view, true);
            }
            else
            {
                CreateView(uiName);
            }

        }

        mDelayShowView.Clear();
    }

    public void CloseCanDestroyPanel()
    {
        for (int i = mShowingView.Count - 1; i >= 0; --i)
        {
            if (i >= mShowingView.Count)
            {
                continue;
            }

            if (mShowingView[i] != null && mShowingView[i].ClosePanelWhenCG)
            {
                DestroyView(mShowingView[i].name);
            }

        }
    }

    public void ShowAllPopup()
    {
        foreach (IView view in mLastPopupShows)
        {
            SetViewVisible(view, true);
            //AddToShowing(view);
        }

        mLastPopupShows.Clear();
    }

    public void SetViewState(string uiName, bool val)
    {
        IView view = GetView(uiName);

        if (view != null)
        {
            SetViewVisible(view, true);
        }
    }

    private static int CompareIndex(IView a, IView b)
    {
        if (a.ViewIndex > b.ViewIndex)
        {
            return 1;
        }
        else if (a.ViewIndex < b.ViewIndex)
        {
            return -1;
        }
        else
        {
            return 0;
        }
    }

    /*
    public void TestOpenUI(object[] args)
    {
        LogManager.Instance.Log(" public void TestOpenUI(object[] args)");
    }
    */
}