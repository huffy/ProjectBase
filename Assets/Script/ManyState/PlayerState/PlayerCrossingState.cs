﻿///
///角色翻越
///
using UnityEngine;
using System.Collections;

namespace Assets.Script.ManyState.PlayerStatek
{
    public class PlayerCrossingState : FsmState<PlayerCreator>
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