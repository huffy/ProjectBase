﻿
///
///角色行走状态
///
using UnityEngine;
using System.Collections;
using Assets.Script.AudioMgr;
using Spine.Unity;

namespace Assets.Script.ManyState.PlayerStatek
{
    public class PlayerLeftWalkState : FsmState<PlayerCreator>
    {
        Spine.Bone fist; 
       


        public override void Enter()
        {
            Target.SetAnimationState(PlayerCreator.AnimationStr.walk.ToString(), true);
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
            //Debug.Log("takeing");
        }

        public override void Exit()
        {
            base.Exit();
            string animaStr = PlayerCreator.AnimationStr.stand.ToString();
            bool bLoop = true;
            Target.SetAnimationName(animaStr, bLoop);
           // AudioControl.GetInstance().StopAudioByID(audioID);
            //RemoveListener();
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