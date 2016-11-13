
//功能：
//创建者: 胡海辉
//创建时间：


using Assets.Script.BallMgr.HostCenter;
using Assets.Script.BallMgr.Paddle;
using Assets.Script.Base;
using UnityEngine;

namespace Assets.Script.BallMgr
{
    public class ContorlPaddle : BaseCommpoent
    {
        public PaddleCreator mPaddleCreator;
        private Transform mHostCenterCreatorTrans;

        public override void InitComponent()
        {
            base.InitComponent();
            HostCenterCreator mHostCenterCreator = MonoBehaviour.FindObjectOfType(typeof(HostCenterCreator)) as HostCenterCreator;
            if (mHostCenterCreator != null) mHostCenterCreatorTrans = mHostCenterCreator.transform;
        }
        public override void InitData()
        {
            base.InitData();

        }
        public override void InitListener()
        {
            base.InitListener();
        }

        public override void RemoveListener()
        {
            base.RemoveListener();
        }

        public override void Dispose()
        {
            base.Dispose();
        }

        public void MovePaddle(Vector3 pos)
        {
            if (mPaddleCreator == null)
            {
                mPaddleCreator = (PaddleCreator)mMonoCreator;
            }
            if (mPaddleCreator != null)
            {
                mPaddleCreator.SetPaddleRotation(mHostCenterCreatorTrans.transform.position, pos);
            }
        }
    }
}
