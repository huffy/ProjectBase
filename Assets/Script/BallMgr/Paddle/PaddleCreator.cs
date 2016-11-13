
//功能：
//创建者: 胡海辉
//创建时间：


using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Script.BallMgr.Paddle
{
    public class PaddleCreator : BaseCreator
    {
        public override void LogicCollision(BaseCreator _Ball)
        {
            //throw new NotImplementedException();
        }

        public void SetPaddleRotation(Vector3 centerPos, Vector3 pos)
        {
            Vector3 vecTrans = transform.position - centerPos;
            Vector3 vecPos = pos - centerPos;
            Vector3 vecCross =Vector3.Cross(vecPos.normalized, vecTrans.normalized);
            float angle = Mathf.Asin(Vector3.Distance(Vector3.zero, vecCross)) * Mathf.Rad2Deg;
            if (vecCross.z > 0) //如果反向就取负角度
            {
                angle = -angle;
            }
            transform.position = centerPos + Quaternion.Euler(0, 0, angle) * (transform.position - centerPos);
            transform.up = transform.position - centerPos;
        }
    }


}
