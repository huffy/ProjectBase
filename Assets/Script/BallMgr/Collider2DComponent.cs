
//功能：
//创建者: 胡海辉
//创建时间：


using Assets.Script.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.BallMgr
{
    public class Collider2DComponent : BaseMonoBehaviour
    {
        public delegate void CollisionDelegate(Collision2D collision);
        public delegate void TriggerDelegate(Collider2D collision);

        public CollisionDelegate CollisionEnter2D;
        public CollisionDelegate CollisionStay2D;
        public CollisionDelegate CollisionExit2D;
        public TriggerDelegate TriggerStay2D;
        public TriggerDelegate TriggerExit2D;
        public TriggerDelegate TriggerEnter2D;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (CollisionEnter2D != null)
                CollisionEnter2D(collision);
        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            if (CollisionStay2D != null)
                CollisionStay2D(collision);
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            if (CollisionExit2D != null)
                CollisionExit2D(collision);
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (TriggerStay2D != null)
                TriggerStay2D(collision);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (TriggerExit2D != null)
                TriggerExit2D(collision);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (TriggerEnter2D != null)
                TriggerEnter2D(collision);
        }


    }
}
