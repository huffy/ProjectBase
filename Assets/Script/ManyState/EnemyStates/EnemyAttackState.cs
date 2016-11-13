
//功能：敌人攻击状态
//创建者: 胡海辉
//创建时间：


using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Script.Tools;

namespace Assets.Script.ManyState.EnemyStates
{
    class EnemyAttackState : FsmState<EnemyCreator>
    {
        public override void Enter()
        {
            base.Enter();
            string animaStr = EnemyCreator.AnimationStr.attack.ToString();
            bool bLoop = true;
            //float baseSpeed=Target.baseAnimationSpeed,currSpeed=Target.mEnemyAIData.attackSpeed;

            Target.SetAnimationName(animaStr, bLoop);
            //Target.SetAnimationSpeed(animaStr,Target.GetAnimationSpeed(baseSpeed,currSpeed));
        }

        public override void Update()
        {
            base.Update();
            if (Target.currTarget != null) 
            {
                float dis = CharActorHelper.GetInstance().Distance(Target.transform.position, Target.colliderRange, Target.currTarget.transform.position, Target.currTarget.colliderRange);
                if (dis > Target.mEnemyAIData.attackRange)
                {
                    Target.ChangeAIState(EnemyState.Run);
                }
                else 
                {

                }
            }
            
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}
