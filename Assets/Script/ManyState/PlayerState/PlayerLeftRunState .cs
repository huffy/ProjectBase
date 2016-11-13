
///
///角色行走状态
///
using UnityEngine;
using System.Collections;
using Assets.Script.AudioMgr;


namespace Assets.Script.ManyState.PlayerStatek
{
    public class PlayerLeftRunState : FsmState<PlayerCreator>
    {
        Spine.Bone fist;
       
        public override void Enter()
        {
            Target.SetAnimationState(PlayerCreator.AnimationStr.run.ToString(), true);
            base.Enter();
            Target.transform.localEulerAngles = new Vector3(0, 180, 0);
            if (Target.speed > 0) Target.speed = -1 * Target.speed;
            //InitListener();
            fist = Target.mAnimation.skeleton.FindBone("hand1");
           
        }

        public override void Update()
        {
            base.Update();
            MoveContorlManager.GetInstance().Move(Target.transform, Target.speed);
            if (Target.bTakeThing) Target.takeCurrObj.transform.position = Target.mAnimation.transform.TransformPoint(fist.WorldX, fist.WorldY, 0f);
        }

        public override void Exit()
        {
            base.Exit();
            Target.SetAnimationState(PlayerCreator.AnimationStr.stand.ToString(), true);
            Target.AnimationPlay();
        }

        
        //private void InitListener()
        //{
        //    Target.mAnimation.state.End += AnimationEnd;
        //}

        //private void RemoveListener()
        //{
        //    Target.mAnimation.state.End -= AnimationEnd;
        //}

        //private void AnimationEnd(Spine.AnimationState state, int trackIndex)
        //{
        //    AudioControl.GetInstance().PlayAudio(Random.Range(8, 11), true);
        //}
    }
}
