
//功能：
//创建者: 胡海辉
//创建时间：


using Assets.Script.Base;
using UnityEngine;
using UnityEngine.Events;
using System;

namespace Assets.TestScript
{
    [Serializable]
    public class tempUnityEvent : UnityEvent<string> { }
    public class TestAttribute : BaseMonoBehaviour
    {
        [SerializeField]
        public tempUnityEvent passs;


        public override void Awake()
        {
            //checked passs; 
            base.Awake();
        }

        public override void InitComponent()
        {
            base.InitComponent();
            int t1 = 17, t2 = 6;
            DebugHelper.DebugLogFormat("{0}\n", t1 << 3);
            DebugHelper.DebugLogFormat("{0}\n", t1 << 2);
            DebugHelper.DebugLogFormat("{0}\n", t1 >> 3);
            DebugHelper.DebugLogFormat("{0}\n", t1 >> 2);
            DebugHelper.DebugLogFormat("{0}\n", t1 | t2);
            DebugHelper.DebugLogFormat("{0}\n", t1 & t2);
            DebugHelper.DebugLogFormat("{0}\n", t1 & 5);
        }

        public override void BaseUpdate(float time)
        {
            base.BaseUpdate(time);
        }

        public override void LateUpdate()
        {
            base.LateUpdate();
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
        }

    }

}
