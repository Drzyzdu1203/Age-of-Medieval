using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AoM
{
    public class AmbushState : State
    {
        public bool isMinning;
        public float detectionRadius = 2;
        public string miningAnimation;
        public string rageAnimation;

        public LayerMask detectionLayer;

        public PursueTargetState PursueTargetState;

        public override State Tick(EnemyManager enemyManager, EnemyStats enemyStats, EnemyAnimatorManager enemyAnimatorManager)
        {
            if (isMinning && enemyManager.isinteracting == false)
            {
                enemyAnimatorManager.PlayTargetAnimation(miningAnimation, true);
            }

            #region Handle Target Detection

            Collider[] colliders = Physics.OverlapSphere(enemyManager.transform.position, detectionRadius, detectionLayer);
            for (int i = 0; i < colliders.Length; i++)
            {
                CharacterStats characterStats = colliders[i].transform.GetComponent<CharacterStats>();

                if(characterStats != null)
                {
                    Vector3 targetsDirection = characterStats.transform.position - enemyManager.transform.position;
                    float viewableAngle = Vector3.Angle(targetsDirection, enemyManager.transform.forward);

                    if(viewableAngle > enemyManager.minimumDetectionAngle
                    && viewableAngle < enemyManager.maximumDetectionAngle)
                    {
                        enemyManager.currentTarget = characterStats;
                        isMinning = false;
                        enemyAnimatorManager.PlayTargetAnimation(rageAnimation, true);
                    }
                }
            }
            #endregion

            #region Handle State Change

            if(enemyManager.currentTarget != null)
            {
                return PursueTargetState;
            }
            else
            {
                return this;
            }
            #endregion
        }
    }
}
