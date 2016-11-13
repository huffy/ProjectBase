
//功能：
//创建者: 胡海辉
//创建时间：


using Assets.Script.BallMgr.Brick;
using Assets.Script.BallMgr.HostCenter;
using Assets.Script.BallMgr.Paddle;
using Assets.Script.Base;
using UnityEngine;

namespace Assets.Script.BallMgr
{
    public class BallCreator : BaseCreator
    {
        [HideInInspector]
        public Collider2DComponent ballCollider;
        [HideInInspector]
        public string ballPath = "";
        [HideInInspector]
        public Vector2 ballVelocity = new Vector2(1, 1);
        [HideInInspector]
        public Rigidbody2D mRigidbody2D;
        public bool bbb
        {
            get;
            private set;
        }


        public override void InitComponent()
        {
            base.InitComponent();
            ballCollider = transform.FindChildComponent<Collider2DComponent>(ballPath, true);
            mRigidbody2D = transform.FindChildComponent<Rigidbody2D>(ballPath, true);
        }

        public override void InitData()
        {
            base.InitData();
            mRigidbody2D.velocity = ballVelocity;
        }

        public override void InitListener()
        {
            base.InitListener();
            ballCollider.CollisionEnter2D += BallCollisionCallBack;
        }

        public override void RemoveListener()
        {
            base.RemoveListener();
            ballCollider.CollisionEnter2D -= BallCollisionCallBack;
        }

        public override void BaseUpdate(float time)
        {
            base.BaseUpdate(time);
        }

        public override void LogicCollision(BaseCreator _Ball)
        {
            if (IsNeedChangeVelocity(mRigidbody2D)) 
            {
                //Vector2 velocityTemp= mRigidbody2D.velocity;
                //float sqrMagnitude = velocityTemp.sqrMagnitude;
                //float changeXNum=Random.Range(velocityTemp.x * 0.1f, velocityTemp.x * 0.5f);
                //velocityTemp.x = Mathf.Abs(changeXNum) < 0.5f ? changeXNum + 1 : changeXNum;
                //float changeYSqrMagnitude = sqrMagnitude - velocityTemp.x * velocityTemp.x;
                //if (changeYSqrMagnitude <= 0) DebugHelper.DebugLogError("changeYSqrMagnitude==" + changeYSqrMagnitude);
                //else velocityTemp.y = Mathf.Sqrt(sqrMagnitude - velocityTemp.x * velocityTemp.x);
                mRigidbody2D.velocity = ballVelocity;
            }
        }

        #region private Fuction

        private void BallCollisionCallBack(Collision2D collision)
        {
            switch (collision.gameObject.tag)
            {
                case SceneTagAndLayerMgr.hostCenterTag:
                    HostCenter(collision.gameObject.GetComponentInParent<HostCenterCreator>());
                    break;
                case SceneTagAndLayerMgr.brickTag:
                    Brick(collision.gameObject.GetComponentInParent<BrickCreator>());
                    break;
                case SceneTagAndLayerMgr.paddleTag:
                    Paddle(collision.gameObject.GetComponentInParent<PaddleCreator>());
                    break;
                default:
                    break;
            }
            LogicCollision(null);
        }

        private void HostCenter(HostCenterCreator creator)
        {
            SetCreatorInfo(creator);
        }

        private void Brick(BrickCreator creator)
        {
            SetCreatorInfo(creator);
        }

        private void Paddle(PaddleCreator creator)
        {
            SetCreatorInfo(creator);
        }

        public void SetCreatorInfo(BaseCreator creator)
        {
            try
            {
                creator.SetBaseCreator(this);
                creator.LogicCollision(this);

               
            }
            catch (System.Exception)
            {

                DebugHelper.DebugLogError("-------------------------------");
            }
        }

        private bool IsNeedChangeVelocity(Rigidbody2D mRig2D) 
        {
            float downDot = Vector2.Dot(mRig2D.velocity.normalized, Vector2.down);
            float leftDot = Vector2.Dot(mRig2D.velocity.normalized, Vector2.left);
            if (CheckDirNum(downDot) || CheckDirNum(leftDot)) 
            {
                return true;
            }
            return false;

        }

        private bool CheckDirNum(float mDot) 
        {
            return (Mathf.Abs(mDot) > 0.95f || Mathf.Abs(mDot) < 0.05f);
        }

        #endregion

    }
}
