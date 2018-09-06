using UnityEngine;
public class ResourcesDefine : MonoBehaviour 
{
    public static string mUIPath = "Prefab/UI/";
    public static string mPXUIPath = "Prefab/PXUI/";
    public static string mIPADUIPath = "Prefab/IPADUI/";
    public static string mUIWH18Path = "Prefab/UIWH18/";
    public static string mAttachmentPath = "Prefab/Character/Attachment/";
    public static string mCharacterRolePath = "Prefab/Character/Role/";
    public static string mCharacterNpcPath = "Prefab/Character/Npc/";
    public static string mCharacterPartnerPath = "Prefab/Character/Partner/";
    public static string mCharacterWeaponPath = "Prefab/Character/Weapon/";
    public static string mCharacterBgPath = "Prefab/Character/Bg/";
    public static string mSkillEffectPath = "Prefab/Effect/";
    public static string mSkillSoundPath = "Prefab/Sound/";
    public static string mSkillCameraAnimationPath = "Prefab/CameraAnimation/";
    public static string mScenePath = "Scenes/";
    public static string mConfigPath = "Config/";
    public static string mAIPath = "Config/AI/";
    public static string mHUDPath = "Prefab/HUD/";
    public static string mTalkModelPath = "Talk/Model/";
    public static string mLoginSoundPath = "Prefab/Sound/Bgm/";
    public static string mTalkSoundPath = "Prefab/Sound/Talk/";
    public static string mTalkBGMPath = "Prefab/Sound/Bgm/";
    public static string mUISoundPath = "Prefab/Sound/UI/";
    public static string mPlotSoundPath = "Prefab/Sound/Plot/";
    public static string mPlotMusicPath = "Prefab/Sound/Bgm/";

    public static string mIdleEffectPath = "Prefab/Effect/Common/water_ring_2";//站在水上的特效
    
    public static string mMoveEffectPath = "Prefab/Effect/Common/move";//普通地面走路特效
    public static string mMoveEffectCement = "Prefab/Effect/Common/move";//普通硬水泥地面走路特效
    public static string mMoveEffectWater = "Prefab/Effect/Common/water_ring_1";//走路水特效
    public static string mMoveEffectSand = "Prefab/Effect/Common/move_sand";//走路沙地特效
    public static string mMoveEffectGrass = "Prefab/Effect/Common/move_grass";//走路草地特效

    public static string mCSEffect = "Prefab/Effect/Common/cs_effect_";//传送特效
    public static int mSecKillEffect = 65001309;//斩杀特效id
    public static int mWillResumeEffect = 65434004;//定力回复特效id
    public static int mShieldEffect = 65311016;//护盾吸收伤害特效id
    public static int mTauntEffect = 65404006;//嘲讽特效id
    public static int mWillBreakEffect = 65434003;//定力破除特效

    public static string mWillLoopEffect = "Prefab/Effect/Common/E_Common_WillLoop";//定力恢复特效
    public static string mTargetEffect = "Prefab/Effect/Common/target";//锁怪标记
    public static string mAlertEffect = "Prefab/Effect/Common/E_Common_alert";//警戒特效
    public static string mJoystickEffect = "Prefab/Effect/Common/JoystickEffect";//双摇杆释放技能特效

    public static int mWillSoundPath = 90400054;//定力受击音效
    public static int[] mMoveSoundPathGround = new int[] { 90400042, 90400043, 90400044, 90400045 };//普通陆地走路音效
    public static int[] mMoveSoundPathWater = new int[] { 90400050, 90400051, 90400052, 90400053 };//水上走路音效
    public static int[] mMoveSoundPathSand = new int[] { 90400046, 90400047, 90400048, 90400049 };//沙地走路音效
    public static int[] mMoveSoundPathGrass = new int[] { 90400038, 90400039, 90400040, 90400041 };//草地走路音效

    public static string mBattleReportPath = "Prefab/Sound/ReportBgm/";
    public static string mCommonEffectPath = "Prefab/Effect/Common/";

    public static string mUIEffectPath = "Prefab/Effect/UI/";

    public static string mSkillIconPath = "Textures/Skill/";//技能icon
    public static string mNpcIconPath = "Textures/ChatIcon/";//npc单位icon
    public static string mChatNpcIconPath = "Textures/Npc/";//npc单位icon

    public static string mRoleIconPath = "Textures/Role/";//角色单位icon
    public static string mBuffIconPath = "Textures/Buff/";//buff icon

    public static string mMoviePath = "StreamingAssets/";
    public static string mSceneMapPath = "MiniMap/";    //小地图图片路径

    public static string mNpcDieTex = "Textures/Mat/discard";

    public static string mCutscene_LunaFantasy = "Cutscene/Cutscene_LunaFantasy/"; //御姐视频路径
    public static string mCutscene_RockHunter = "Cutscene/Cutscene_RockHunter/"; //少年视频路径
    public static string mCutscene_ShadowBlade = "Cutscene/Cutscene_ShadowBlade_Instead/"; //萝莉视频路径

    public static string mCutscene_Role = "Cutscene/Cutscene_Role/"; //动态主角

    public static string mloadingShinobidoPath = "Textures/Shinobido/"; //15V15连斩文字
    public static string mSoonLockPath = "Textures/SoonLock/";   //即将解锁原画路径
    public static string mTeamPVPPath = "Textures/TeamPVP/";   //即将解锁原画路径
    public static string mSceneName = "Textures/SceneName/";    //场景名字路径
    public static string mUINewSoundPath = "Sound/ui/"; // 界面声音
}