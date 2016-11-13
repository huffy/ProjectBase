
//功能：
//创建者: 胡海辉
//创建时间：


using UnityEngine;

namespace Assets.Script.BallMgr
{
    public class NormalBall : BallCreator
    {
        public override void InitComponent()
        {
            ballPath = "Orb_Blue";
            base.InitComponent();
            ballVelocity = new Vector2(Random.Range(4f, 8f), Random.Range(4f, 8f));
        }

        public override void LogicCollision(BaseCreator _Ball)
        {
            base.LogicCollision(_Ball);
            
        }

    }
}
