///
///角色待机状态
///
using UnityEngine;
using System.Collections;

namespace Assets.Script.ManyState.PlayerStatek
{
    public class PlayerIdleState : FsmState<PlayerCreator>
    {
        Spine.Bone fist;
        public override void Enter()
        {
            Target.SetAnimationState(PlayerCreator.AnimationStr.stand.ToString(), true);
            base.Enter();
            fist = Target.mAnimation.skeleton.FindBone("hand1");
        }

        public override void Update()
        {
            base.Update();
            //Target.ChangeAIState(EnemyState.Run);
            if (Target.bTakeThing) Target.takeCurrObj.transform.position = Target.mAnimation.transform.TransformPoint(fist.WorldX, fist.WorldY, 0f);
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}
