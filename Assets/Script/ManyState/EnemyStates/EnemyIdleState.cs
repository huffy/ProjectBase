
//功能：敌人待机状态
//创建者: 胡海辉
//创建时间：


using UnityEngine;
using System.Collections;

namespace Assets.Script.ManyState.EnemyStates
{
    class EnemyIdleState : FsmState<EnemyCreator>
    {
        float currStayTime = 0;
        public override void Enter()
        {
            Target.SetAnimationState(EnemyCreator.AnimationStr.stand.ToString(), true);
            base.Enter();
            //bool bLoop = true;
            currStayTime = 0;
        }

        public override void Update()
        {
            base.Update();
            if (currStayTime > Target.mEnemyAIData.stayTime)
            {
                Target.ChangeAIState(EnemyState.TurnAround);
            }
            else 
            {
                currStayTime += Time.deltaTime;
            }
        }

        public override void Exit()
        {
            base.Exit();
            currStayTime = 0;
        }
    }
}
