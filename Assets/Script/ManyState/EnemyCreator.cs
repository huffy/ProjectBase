///
///敌人AI

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Script.ManyState;
using Assets.Script.Tools;
using Spine.Unity;
using Assets.Script.ManyState.EnemyStates;

namespace Assets.Script.ManyState
{
    public class EnemyCreator : CreatorBase
    {

        [HideInInspector]
        public enum AnimationStr
        {
            stand,
            stand1,
            walk,
            attack,
        }

        FsmStateMachine<EnemyCreator> m_Machine;//状态机对象用来管理状态的
        public Dictionary<EnemyState, FsmState<EnemyCreator>> StateDic; //状态字典，用来存放状态
        public EnemyAIData mEnemyAIData = null;  //敌人AI数据
        public CreatorBase currTarget;//敌人目标
        public Vector3 lastPathPos = Vector3.zero;//上个路点
        public Vector3 nextPathPos = Vector3.zero;//下个路点
        public List<Vector3> pathPocList;

        public override void Awake()
        {
            base.Awake();
            StateDic = new Dictionary<EnemyState, FsmState<EnemyCreator>>();
            StateDic.Add(EnemyState.Run, new EnemyRunState());
            StateDic.Add(EnemyState.Idle, new EnemyIdleState());
            StateDic.Add(EnemyState.Attack, new EnemyAttackState());
            StateDic.Add(EnemyState.TurnAround, new EnemyTurnAroundState());
            string path = ReadXmlDataMgr.GetInstance().GetXmlPath(ReadXmlDataMgr.XmlName.EnemyAI);
            string XNodeName = ReadXmlDataMgr.XmlName.EnemyAI + "1";
            ReadXmlMgr<EnemyAIData>.SetXmlPath(path);
            mEnemyAIData = ReadXmlMgr<EnemyAIData>.ReadXmlElement(XNodeName);
            colliderRange = mEnemyAIData.colliderRange;
            speed = 0; //mEnemyAIData.moveSpeed;
            baseAnimationSpeed = mEnemyAIData.moveSpeed;
            pathPocList = new List<Vector3>();
            SetPathPosList(transform.FindChild("Path"));
            if (pathPocList.Count > 0)
            {
                nextPathPos = pathPocList[0];
            }
            else
            {
                Debug.LogError("error !!!  None Path,  Check Path..........");
            }
        }

        // Use this for initialization
        private void Start()
        {
            m_Machine = new FsmStateMachine<EnemyCreator>(this);
            m_Machine.SetCurrentState(StateDic[EnemyState.Idle]);
        }

        // Update is called once per frame
        public override void BaseUpdate(float time)
        {
            base.BaseUpdate(time);
            m_Machine.Update();
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
            pathPocList.Clear();
            pathPocList = null;
        }

        /// <summary>
        /// 设置路点List
        /// </summary>
        private void SetPathPosList(Transform root)
        {
            pathPocList.Clear();
            for (int i = 0; i < root.childCount; i++)
            {
                pathPocList.Add(root.GetChild(i).position);
            }
        }

        /// <summary>
        /// 设置下一个路点位置
        /// </summary>
        public void SetNextPath(Vector3 currPath)
        {
            for (int i = 0; i < pathPocList.Count; i++)
            {
                if (pathPocList[i] == currPath)
                {
                    lastPathPos = nextPathPos;
                    nextPathPos = pathPocList[i + 1];
                    return;
                }
            }
        }

        /// <summary>
        /// 改变AI状态
        /// </summary>
        public void ChangeAIState(EnemyState state)
        {
            m_Machine.ChangeState(StateDic[state]);
        }
    }
}
