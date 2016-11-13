///
///角色拉动状态
///
using UnityEngine;
using System.Collections;

namespace Assets.Script.ManyState.PlayerStatek
{
    public class PlayerPullState : FsmState<PlayerCreator>
    {

        public override void Enter()
        {
            base.Enter();
        }

        public override void Update()
        {
            base.Update();
            //Target.ChangeAIState(EnemyState.Run);
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}
