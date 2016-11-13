
//功能：角色基类
//创建者: 胡海辉
//创建时间：


using Assets.Script.Base;
using Spine.Unity;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Script.ManyState
{
    public class CreatorBase : BaseMonoBehaviour
    {
        [HideInInspector]
        public SkeletonAnimation mAnimation;
        [HideInInspector]
        public float colliderRange;
        [HideInInspector]
        public float speed;
        [HideInInspector]
        public float baseAnimationSpeed;
        [HideInInspector]
        public string animaStr;
        [HideInInspector]
        public bool animaLoop;
        [HideInInspector]
        /// <summary>
        ///  是否东西
        /// </summary>
        public bool bTakeThing = false;

        public override void Awake()
        {
            base.Awake();
        }
      
        public override void InitComponent()
        {
            base.InitComponent();
            mAnimation = GetComponent<SkeletonAnimation>();
            baseAnimationSpeed = 1;
        }

        public override void InitData()
        {
            base.InitData();
            bTakeThing = false;
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
            mAnimation = null;
        }

        /// <summary>
        /// 设置动画状态
        /// </summary>
        /// <param name="mAnimationStr"></param>
        /// <param name="bLoop"></param>
        public void SetAnimationState(string mAnimaStr, bool bLoop = false) 
        {
            animaLoop = bLoop;
            animaStr = mAnimaStr;
        }

        /// <summary>
        /// 播放动画
        /// </summary>
        public void AnimationPlay()
        {
            animaStr = RealAnimaName(animaStr, bTakeThing);
            SetAnimationName(animaStr, animaLoop);
        }

        public void SetAnimationName(string animationStr, bool bLoop)
        {
            Spine.Animation animationToUse = mAnimation.Skeleton.Data.FindAnimation(animationStr);
            mAnimation.state.SetAnimation(0, animationToUse, bLoop);
        }

        public void SetAnimationSpeed(string animationStr, float animSpeed = 1)
        {
            //Spine.Animation animationToUse = mAnimation.skeleton.Data.FindAnimation(animationStr);
            mAnimation.state.TimeScale = animSpeed;
        }

        public float GetAnimationSpeed(float baseSpeed, float currSpeed)
        {
            return currSpeed / baseSpeed;
        }

        /// <summary>
        /// 获取真正是动画名字
        /// </summary>
        /// <param name="animaName"></param>
        /// <returns></returns>
        public string RealAnimaName(string animaName, bool bTakeLight)
        {




            if (bTakeLight == false)
            {
                animaName = animaName + "_kongshou";
            }
            return animaName;
        }

    }
}
