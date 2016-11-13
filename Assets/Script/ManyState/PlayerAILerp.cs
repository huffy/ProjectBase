
//功能：
//创建者: 胡海辉
//创建时间：


using Pathfinding;
using System;
using System.Collections.Generic;
using UnityEngine;
namespace Assets.Script
{
    public class PlayerAILerp : AILerp
    {

        #region private 
        private PlayerCreator mPlayerCreator;
        private bool bPlayerMoving = false;
        #endregion

        #region overrode
        protected override void Awake()
        {
            base.Awake();
            mPlayerCreator = transform.FindChild("PlayerAnimation").GetComponent<PlayerCreator>();
        }

        protected override void Start()
        {
            base.Start();
            mPlayerCreator.SetPlayerAILerp(this);
        }

        protected override void OnEnable()
        {
            base.OnEnable();
        }


        protected override void Update()
        {
            base.Update();
            if (bPlayerMoving)
            {
                if (Vector3.Dot(Vector3.right, curDirection) > 0)
                {
                    mPlayerCreator.ChangeAIState(PlayerState.RightWalk);
                }
                else 
                {
                    mPlayerCreator.ChangeAIState(PlayerState.LeftWalk);
                }
            }
        }

        public void OnDestroy()
        {

        }

        public override void SearchPath()
        {
            base.SearchPath();
        }
        #endregion

        #region 事件
        public override void OnPathComplete(Path p)
        {
            base.OnPathComplete(p);
            bPlayerMoving = true;
        }

        public override void OnTargetReached()
        {
            base.OnTargetReached();
            bPlayerMoving = false;
            mPlayerCreator.ChangeAIState(PlayerState.Idle);
        }

        
        #endregion

        #region private Fuc

        #endregion

    }
}
