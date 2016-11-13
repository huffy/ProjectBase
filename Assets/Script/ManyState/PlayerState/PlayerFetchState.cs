///
///角色拾取状态
///
using UnityEngine;
using System.Collections;
using Assets.Script.EventMgr;

namespace Assets.Script.ManyState.PlayerStatek
{
    public class PlayerFetchState : FsmState<PlayerCreator>
    {
        string animaStr;
        bool onceEnter = true;
        Spine.Bone fist;

        public override void Enter()
        {
            base.Enter();
            Target.isForceInterrupt = false;
            animaStr = PlayerCreator.AnimationStr.take.ToString();
            animaStr = Target.RealAnimaName(animaStr, true);
            bool bLoop = false;
            Target.SetAnimationName(animaStr, bLoop);
            onceEnter = true;
            InitListener();
            fist = Target.mAnimation.skeleton.FindBone("hand1");
            Target.bTakeThing = true;

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
            RemoveListener();
        }

        private void InitListener()
        {
            Target.mAnimation.state.Complete += AnimationComplete;
        }

        private void RemoveListener()
        {
            Target.mAnimation.state.Complete -= AnimationComplete;
        }

        private void AnimationComplete(Spine.AnimationState state, int trackIndex, int loopCount)
        {
            if (state.ToString() == PlayerCreator.AnimationStr.take.ToString() && onceEnter)
            {
                onceEnter = false;
                GameObject takeObj = Target.takeCurrObj;
               // Debug.Log(takeObj.name);
           if (takeObj != null)
                {
                    /*  takeObj.transform.parent = Target.transform;
                      takeObj.transform.localPosition = new Vector3(1.3f, 3.5f, 0.17f);*/
                  //  Target.hasLight = true;
                    if (takeObj.GetComponent<BoxCollider2D>())
                    {
                        takeObj.GetComponent<BoxCollider2D>().enabled = false;
                        EventHelpr.GetInstance().RasieTiggerEvent(TiggerType.none, null);
                    }
                }
                animaStr = "take2";
                animaStr = Target.RealAnimaName(animaStr, true);
                bool bLoop = false;
                Target.SetAnimationName(animaStr, bLoop);
            }
            else if (state.ToString()=="take2")
            {
                Target.isForceInterrupt = true;
                Target.bTakeThing = true;
                Target.ChangeAIState(PlayerState.Idle);
            }
        }
    }
}
