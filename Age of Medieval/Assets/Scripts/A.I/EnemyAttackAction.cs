using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AoM
{
    [CreateAssetMenu(menuName = "A.I/Enemy Actions/Attack Action")]
    public class EnemyAttackAction : EnemyAction
    {
        public bool canCombo;
        public EnemyAttackAction comboAction;

        public int attackScore = 3;
        public float recoveryTime = 1;

        public float maximumAttackAngle = 35;
        public float minimumAttackAngle = -35;

        public float minimumDistanceNeededToAttack = 0;
        public float maximumDistanceNeededToAttack = 3;
    }
}