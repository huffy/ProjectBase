///
///角色创建
///
using UnityEngine;
using System.Collections.Generic;
using Assets.Script.ManyState.PlayerStatek;
using Assets.Script.ManyState;
using Assets.Script;
using Assets.Script.EventMgr;
using Assets.Script.Tools;
using Assets.Script.AudioMgr;
using System;

public class PlayerCreator : CreatorBase
{
    #region member

    [HideInInspector]
    public Dictionary<PlayerState, FsmState<PlayerCreator>> StateDic; //状态字典，用来存放状态
    [HideInInspector]
    /// <summary>
    /// 是否交互
    /// </summary>
    public bool bInteractionState = false;
    [HideInInspector]
    /// <summary>
    /// 当前触发的类型
    /// </summary>
    public TiggerTypeParam CurrTigger;
    [HideInInspector]
    /// <summary>
    /// 拾取的物体
    /// </summary>
    public GameObject takeCurrObj;
    [HideInInspector]
    public  enum AnimationStr
    {
        stand,
        walk,
        take,
        run,
    }
    [HideInInspector]
    public bool isForceInterrupt = true;
    [HideInInspector]
    public bool CanMove = true;
    [HideInInspector]
    public List<RoomType> mCurrKeyNumList = new List<RoomType>();
    #endregion

    #region private
    private FsmStateMachine<PlayerCreator> m_Machine;//状态机对象用来管理状态的
    private PlayerData mPlayerData;
    private PlayerState currState = PlayerState.Idle;
    //private bool bTiggetStay = false;
    private MeshRenderer playerRender;
    private TiggerType mCurrTiggerType;
    private Collider2D currTiggerCollider;
    private PlayerAILerp mPlayerAILerp;
    # endregion


    #region unity

    public override void Awake()
    {
        base.Awake();
    }

    public override void Init()
    {
        base.Init();
    }

    public override void InitComponent()
    {
        base.InitComponent();
        StateDic = new Dictionary<PlayerState, FsmState<PlayerCreator>>();
        StateDic.Add(PlayerState.Idle, new PlayerIdleState());
        StateDic.Add(PlayerState.LeftWalk, new PlayerLeftWalkState());
        StateDic.Add(PlayerState.RightWalk, new PlayerRightWalkState());
        StateDic.Add(PlayerState.LeftRun, new PlayerLeftRunState());
        StateDic.Add(PlayerState.RightRun, new PlayerRightRunState());
        StateDic.Add(PlayerState.Pull, new PlayerPullState());
        StateDic.Add(PlayerState.Push, new PlayerPushState());
        StateDic.Add(PlayerState.Fetch, new PlayerFetchState());
        StateDic.Add(PlayerState.Cross, new PlayerCrossingState());
        StateDic.Add(PlayerState.Climb, new PlayerClimbState());

        playerRender = GetComponent<MeshRenderer>();
    }

    public override void InitData()
    {
        base.InitData();
        string path = ReadXmlDataMgr.GetInstance().GetXmlPath(ReadXmlDataMgr.XmlName.PlayerData);
        string XNodeName = ReadXmlDataMgr.XmlName.PlayerData + "1";
        ReadXmlMgr<PlayerData>.SetXmlPath(path);
        mPlayerData = ReadXmlMgr<PlayerData>.ReadXmlElement(XNodeName);
        speed = 0; //mPlayerData.moveSpeed;
        colliderRange = mPlayerData.colliderRange;
        baseAnimationSpeed = mPlayerData.moveSpeed;
        isForceInterrupt = true;
        m_Machine = new FsmStateMachine<PlayerCreator>(this);
        m_Machine.SetCurrentState(StateDic[PlayerState.Idle]);
    }

    public override void InitListener()
    {
        base.InitListener();

        //动画事件委托
        mAnimation.state.Event += EventHandler;
        EventManager.GetInstance().AddListener(EventDefine.TiggerType, OnTiggerType);
    }

    // Update is called once per frame
    public override void BaseUpdate(float time)
    {
        base.BaseUpdate(time);
        m_Machine.Update();
    }

    public override void RemoveListener()
    {
        base.RemoveListener();
        EventManager.GetInstance().RemoveListener(EventDefine.TiggerType, OnTiggerType);
        mAnimation.state.Event -= EventHandler;
    }

    /// <summary>
    ///进入触发器 
    /// </summary>
    private void OnTriggerEnter2D(Collider2D collision)
    {
       
    }

    /// <summary>
    /// 停留
    /// </summary>
    /// <param name="collision"></param>
    public void OnTriggerStay2D(Collider2D collision)
    {
       
    }

    /// <summary>
    /// 离开触发器
    /// </summary>
    private void OnTriggerExit2D(Collider2D collision)
    {
      
    }

    #endregion
    public void SetPlayerAILerp(PlayerAILerp  _mPlayerAILerp) 
    {
        mPlayerAILerp = _mPlayerAILerp;
    }

    public void EventHandler(Spine.AnimationState state, int trackIndex, Spine.Event e)
    {

        if (e.Data.name == "event_footsteps")
        {
            int walkID = UnityEngine.Random.Range(22, 30);
            int woodId = UnityEngine.Random.Range(30, 38);
            int runID = UnityEngine.Random.Range(38, 46);
            if (m_Machine.GetCurrentState() == StateDic[PlayerState.LeftWalk] || m_Machine.GetCurrentState() == StateDic[PlayerState.RightWalk])
            {
                AudioControl.GetInstance().PlayOnceSoundEffectAtLocation(walkID, transform.position);
                Debug.Log("walk" + walkID);
            }
            else if (m_Machine.GetCurrentState() == StateDic[PlayerState.LeftRun] || m_Machine.GetCurrentState() == StateDic[PlayerState.RightRun])
            {
                AudioControl.GetInstance().PlayOnceSoundEffectAtLocation(runID, transform.position);
                Debug.Log("run" + runID);
            }
            AudioControl.GetInstance().PlayOnceSoundEffectAtLocationByProbability(woodId,transform.position, 5);
        }
    }

    public void ChangeAIState(PlayerState state)
    {
        if (isForceInterrupt == false || CanMove == false) return;
        if (state == PlayerState.LeftRun || state == PlayerState.RightRun)
        {
            if (Mathf.Abs(speed) < mPlayerData.runSpeed)
            {
                speed = mPlayerData.runSpeed;
            }
        }
        else
        {
            if (Mathf.Abs(speed) > mPlayerData.moveSpeed)
            {
                speed = mPlayerData.moveSpeed;
            }
        }
        m_Machine.ChangeState(StateDic[state]);
        currState = state;
    }

    public PlayerState GetCurrState()
    {
        return currState;
    }

    /// <summary>
    /// 触发事件
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="e"></param>
    private void OnTiggerType(object obj, EventArgs e)
    {
        //TiggerTypeParam param = (TiggerTypeParam)obj;
    }

    /// <summary>
    /// 设置人物状态
    /// </summary>
    /// <param name="bMove"></param>
    private void SetPlayerState(bool bMove)
    {
        ChangeAIState(PlayerState.Idle);
        playerRender.enabled = bMove;
        CanMove = bMove;
        bInteractionState = bMove;
    }

}
