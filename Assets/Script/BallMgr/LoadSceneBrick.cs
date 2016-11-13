
//功能：
//创建者: 胡海辉
//创建时间：


using Assets.Script.BallMgr.Brick;
using Assets.Script.Base;
using Assets.Script.Tools;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Script.BallMgr
{
    public class LoadSceneBrick : BaseMonoBehaviour
    {
        private List<BrickCreator> brickList;
        private BrickCreator mBrickCreatorPfb;
        private int rankNum = 7, lineNum = 7;
        public override void InitComponent()
        {
            base.InitComponent();
            string path = CharActorHelper.GetInstance().GetPrefabPath(PrefabName.BrickBlue.ToString());
            mBrickCreatorPfb = DynamicPrefabMgr.GetInstance().GetPrefab<BrickCreator>(path);
        }

        public override void InitData()
        {
            base.InitData();
            //brickList = new List<BrickCreator>();
           InitBrick(new Vector2(-2.1f,4f),true);
           InitBrick(new Vector2(-2.1f, -6f), false);
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
        }

        private void InitBrick(Vector2 initPos,bool bTop) 
        {
            Vector3 pos;
            int addNum = 1;
            if (bTop == false) addNum = -1;
            for (int i = 0; i < lineNum; i++)
            {
                for (int j = 0; j < rankNum; j++)
                {
                    pos = new Vector3(initPos.x + i * 0.72f, initPos.y - j * 0.4f*addNum, 0);
                    DynamicPrefabMgr.GetInstance().InstantiatePrefab<BrickCreator>(mBrickCreatorPfb.gameObject, transform, pos);
                }
            }
        }

    }
}
