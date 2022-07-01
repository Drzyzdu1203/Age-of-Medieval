using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace AoM
{
    public class EnemyStats : CharacterStats
    {


        EnemyAnimatorManager enemyAnimatorManager;

        public int soulsAwardedOnDeath = 50;

        private void Awake()
        {
            enemyAnimatorManager = GetComponentInChildren<EnemyAnimatorManager>();
        }
        void Start()
        {
            maxHealth = SetMaxHealthFromHealthLevel();
            currentHealth = maxHealth;
        }

        private int SetMaxHealthFromHealthLevel()
        {
            maxHealth = healthLevel * 10;
            return maxHealth;
        }
       public void TakeDamage(int damage)
        {
            if (isDead)
                return;
            currentHealth = currentHealth - damage;

            enemyAnimatorManager.PlayTargetAnimation("infantry_05_damage", true);

            if (currentHealth <= 0)
            {
                HandleDeath();
            }
        }

        private void HandleDeath()
        {
            currentHealth = 0;
            enemyAnimatorManager.PlayTargetAnimation("twohanded_06_death_B", true);
            isDead = true;


        }

    }
}
