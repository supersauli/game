#if USE_UNI_LUA
using LuaAPI = UniLua.Lua;
using RealStatePtr = UniLua.ILuaState;
using LuaCSFunction = UniLua.CSharpFunctionDelegate;
#else
using LuaAPI = XLua.LuaDLL.Lua;
using RealStatePtr = System.IntPtr;
using LuaCSFunction = XLua.LuaDLL.lua_CSFunction;
#endif

using System;
using System.Collections.Generic;
using System.Reflection;


namespace XLua.CSObjectWrap
{
    public class XLua_Gen_Initer_Register__
	{
        
        
        static void wrapInit0(LuaEnv luaenv, ObjectTranslator translator)
        {
        
            translator.DelayWrapLoader(typeof(CData), CDataWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(object), SystemObjectWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(Tutorial.BaseClass), TutorialBaseClassWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(Tutorial.TestEnum), TutorialTestEnumWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(Tutorial.DrivenClass), TutorialDrivenClassWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(Tutorial.DrivenClass.TestEnumInner), TutorialDrivenClassTestEnumInnerWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(Tutorial.ICalc), TutorialICalcWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(Tutorial.DrivenClassExtensions), TutorialDrivenClassExtensionsWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(GUIManager), GUIManagerWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(LuaManager), LuaManagerWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UGUISpriteAnimation), UGUISpriteAnimationWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UGUIAtlas), UGUIAtlasWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UVData), UVDataWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UGUIAysnSprite), UGUIAysnSpriteWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UGUIDragComponent), UGUIDragComponentWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UGUIImageCD), UGUIImageCDWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UGUIImageLoader), UGUIImageLoaderWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UGUIImageProg), UGUIImageProgWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UGUIImageSlider), UGUIImageSliderWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.UI.UGUIInputField), UnityEngineUIUGUIInputFieldWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UGUINoDrawingRayCast), UGUINoDrawingRayCastWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UGUISpriteName), UGUISpriteNameWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UGUITweenCanvasGroup), UGUITweenCanvasGroupWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UGUITweenColor), UGUITweenColorWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UGUITweenFill), UGUITweenFillWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UGUITweenPosition), UGUITweenPositionWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UGUITweenRotation), UGUITweenRotationWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UGUITweenScale), UGUITweenScaleWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UGUITweenTop), UGUITweenTopWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UGUIVideoPlayer), UGUIVideoPlayerWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(S_CB), S_CBWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(S_CC), S_CCWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(S_CT), S_CTWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(S_LB), S_LBWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(S_LC), S_LCWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(S_LT), S_LTWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(S_RB), S_RBWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(S_RC), S_RCWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(S_RT), S_RTWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(Suitable.SDEFINE), SuitableSDEFINEWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(TextGradient), TextGradientWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UGUITextWriter), UGUITextWriterWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UGUIEventListener), UGUIEventListenerWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(ListenerType), ListenerTypeWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UGUIEventListenerEx), UGUIEventListenerExWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UGUITool), UGUIToolWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(Allocator), AllocatorWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(LocalizationLabel), LocalizationLabelWrap.__Register);
        
        
        
        }
        
        static void Init(LuaEnv luaenv, ObjectTranslator translator)
        {
            
            wrapInit0(luaenv, translator);
            
            
            translator.AddInterfaceBridgeCreator(typeof(LuaFunc), LuaFuncBridge.__Create);
            
            translator.AddInterfaceBridgeCreator(typeof(LuaFunc2), LuaFunc2Bridge.__Create);
            
            translator.AddInterfaceBridgeCreator(typeof(LuaFunc3), LuaFunc3Bridge.__Create);
            
            translator.AddInterfaceBridgeCreator(typeof(CSCallLua.ItfD), CSCallLuaItfDBridge.__Create);
            
        }
        
	    static XLua_Gen_Initer_Register__()
        {
		    XLua.LuaEnv.AddIniter(Init);
		}
		
		
	}
	
}
namespace XLua
{
	public partial class ObjectTranslator
	{
		static XLua.CSObjectWrap.XLua_Gen_Initer_Register__ s_gen_reg_dumb_obj = new XLua.CSObjectWrap.XLua_Gen_Initer_Register__();
		static XLua.CSObjectWrap.XLua_Gen_Initer_Register__ gen_reg_dumb_obj {get{return s_gen_reg_dumb_obj;}}
	}
	
	internal partial class InternalGlobals
    {
	    
	    static InternalGlobals()
		{
		    extensionMethodMap = new Dictionary<Type, IEnumerable<MethodInfo>>()
			{
			    
			};
			
			genTryArrayGetPtr = StaticLuaCallbacks.__tryArrayGet;
            genTryArraySetPtr = StaticLuaCallbacks.__tryArraySet;
		}
	}
}
