using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace AoM
{
    public class AttackState : State
    {
        public CombatStanceState combatStanceState;

        public EnemyAttackAction[] enemyAttacks;
        public EnemyAttackAction currentAttack;
        public override State Tick(EnemyManager enemyManager, EnemyStats enemyStats, EnemyAnimatorManager enemyAnimatorManager)
        {
            Vector3 targetDirection = enemyManager.currentTarget.transform.position - transform.position;
            float distanceFromTarget = Vector3.Distance(enemyManager.currentTarget.transform.position, enemyManager.transform.position);
            float viewableAngle = Vector3.Angle(targetDirection, transform.forward);

            if (enemyManager.isPreformingAction)
                return combatStanceState;

            if (currentAttack != null)
            {
                // jesli jestesmy za blisko wroga do wykonania przez niego obecnego ataku, wykona nowy atak
                if(distanceFromTarget < currentAttack.minimumDistanceNeededToAttack)
                {
                    return this;
                }
                //jesli jestesmy wystarczajaco blisko do ataku, wrog nas zaatakuje
                else if (distanceFromTarget < currentAttack.maximumDistanceNeededToAttack)
                {
                    //je�li nasz wr�g znajduje si� w zasi�gu naszych atak�w, atakujemy
                    if (viewableAngle <= currentAttack.maximumAttackAngle &&
                        viewableAngle >= currentAttack.minimumAttackAngle)
                    {
                        if(enemyManager.currentRecoveryTime <= 0 && enemyManager.isPreformingAction == false)
                        {
                            enemyAnimatorManager.anim.SetFloat("Vertical", 0, 0.1f, Time.deltaTime);
                            enemyAnimatorManager.anim.SetFloat("Horizontal", 0, 0.1f, Time.deltaTime);
                            enemyAnimatorManager.PlayTargetAnimation(currentAttack.actionAnimation, true);
                            enemyManager.isPreformingAction = true;
                            enemyManager.currentRecoveryTime = currentAttack.recoveryTime;
                            currentAttack = null;
                            return combatStanceState;
                        }
                    }
                }
            }  
            else
            {
                GetNewAttack(enemyManager);
            }

            return combatStanceState;
        }
            private void GetNewAttack(EnemyManager enemyManager)
            {
                 Vector3 targetDirection = enemyManager.currentTarget.transform.position - transform.position;
                 float viewableAngle = Vector3.Angle(targetDirection, transform.forward);
                 float distanceFromTarget = Vector3.Distance(enemyManager.currentTarget.transform.position, transform.position);

                 int maxScore = 0;

                 for (int i = 0; i < enemyAttacks.Length; i++)
                 {
                     EnemyAttackAction enemyAttackAction = enemyAttacks[i];

                     if (distanceFromTarget <= enemyAttackAction.maximumDistanceNeededToAttack
                     && distanceFromTarget >= enemyAttackAction.minimumDistanceNeededToAttack)
                     {
                         if (viewableAngle <= enemyAttackAction.maximumAttackAngle
                          && viewableAngle >= enemyAttackAction.minimumAttackAngle)
                         {
                             maxScore += enemyAttackAction.attackScore;
                         }
                     }
                 }

                 int randomValue = Random.Range(0, maxScore);
                 int temporaryScore = 0;

                 for (int i = 0; i < enemyAttacks.Length; i++)
                 {
                     EnemyAttackAction enemyAttackAction = enemyAttacks[i];

                     if (distanceFromTarget <= enemyAttackAction.maximumDistanceNeededToAttack
                     &&  distanceFromTarget >= enemyAttackAction.minimumDistanceNeededToAttack)
                     {
                         if (viewableAngle <= enemyAttackAction.maximumAttackAngle
                          && viewableAngle >= enemyAttackAction.minimumAttackAngle)
                         {
                             if (currentAttack != null)
                                 return;

                             temporaryScore += enemyAttackAction.attackScore;

                             if (temporaryScore > randomValue)
                             {
                                 currentAttack = enemyAttackAction;
                             }
                         }
                     }

                 }
            }

        
    }
}