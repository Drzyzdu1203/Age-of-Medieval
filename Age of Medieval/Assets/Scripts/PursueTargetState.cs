using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AoM
{
    public class PursueTargetState : State
    {
        public override State Tick(EnemyManager enemyManager, EnemyStats enemyStats, EnemyAnimatorManager enemyAnimatorManager)
        {
            //cahe taeget
            return this;
        }
    }
}
