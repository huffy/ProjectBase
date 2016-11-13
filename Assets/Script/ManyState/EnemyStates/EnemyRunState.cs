
//功能：
//创建者: 胡海辉
//创建时间：


using Assets.Script.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.ManyState.EnemyStates
{
    class EnemyRunState : FsmState<EnemyCreator>
    {
        private float baseSpeed;
        private float currSpeed;
        private Vector3 moveDir;
        private Vector3 targetPos;
        private float colliderRange = 0;
        public override void Enter()
        {
            string animaStr = EnemyCreator.AnimationStr.walk.ToString();
            base.Enter();
            baseSpeed = Target.baseAnimationSpeed;
            currSpeed = Target.mEnemyAIData.moveSpeed;
            Target.SetAnimationSpeed(animaStr, Target.GetAnimationSpeed(baseSpeed, currSpeed));
            if (Target.currTarget == null)
            {
                targetPos = Target.nextPathPos;
                colliderRange = 0;
            }
            else
            {
                targetPos = Target.currTarget.transform.position;
                colliderRange = Target.currTarget.colliderRange;
            }
            moveDir = (Target.transform.position - Target.currTarget.transform.position).normalized;
            moveDir.z = 0;
        }

        public override void Update()
        {
            base.Update();
            float dis = CharActorHelper.GetInstance().Distance(Target.transform.position, Target.colliderRange, targetPos, colliderRange);
            if (Target.currTarget == null)
            {
                if (dis <= 0)
                {
                    Target.ChangeAIState(EnemyState.Idle);
                    Target.SetNextPath(Target.nextPathPos);
                }
                else 
                {
                    MoveContorlManager.GetInstance().Move(Target.transform, moveDir, Target.speed);
                }
            }
            //Target.ChangeAIState(EnemyState.Run);
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}
