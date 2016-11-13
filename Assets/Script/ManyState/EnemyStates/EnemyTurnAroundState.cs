
//功能： 敌人四周看状态
//创建者: 胡海辉
//创建时间：


using Spine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.ManyState.EnemyStates
{
    class EnemyTurnAroundState : FsmState<EnemyCreator>
    {
        public override void Enter()
        {
            Target.SetAnimationState(EnemyCreator.AnimationStr.stand.ToString(), true);
            base.Enter();
            InitListener();
        }

        public override void Update()
        {
            base.Update();
            //Target.ChangeAIState(EnemyState.Run);
        }

        public override void Exit()
        {
            base.Exit();
            RemoveListener();
        }


        private void InitListener() 
        {
            Target.mAnimation.state.End += AnimationEnd;
        }

        private void RemoveListener() 
        {
            Target.mAnimation.state.End -= AnimationEnd;
        }

        private void AnimationEnd(Spine.AnimationState state, int trackIndex)
        {
            Debug.Log("Attack   trackIndex....." + trackIndex);
            Target.ChangeAIState(EnemyState.Run);
        }
    }
}
