using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


namespace AoM
{
    public class EnemyStats : CharacterStats
    {
        EnemyAnimatorManager enemyAnimatorManager;
        CharacterManager character;
        public int soulsAwardedOnDeath = 100;
        Collider enemyCollider;
        Rigidbody enemyRigidbody;


        public UIEnemyHealthBar enemyHealthBar;

        private void Awake()
        {
            enemyCollider = GetComponent<Collider>();
            enemyRigidbody = GetComponent<Rigidbody>();
            enemyAnimatorManager = GetComponentInChildren<EnemyAnimatorManager>();
        }
        void Start()
        {
            maxHealth = SetMaxHealthFromHealthLevel();
            currentHealth = maxHealth;
            enemyHealthBar.SetMaxHealth(maxHealth);
        }

        private int SetMaxHealthFromHealthLevel()
        {
            maxHealth = healthLevel * 10;
            return maxHealth;
        }
        public void TakeDamageNoAnimation(int damage)
        {
            currentHealth = currentHealth - damage;
            enemyHealthBar.SetHealth(currentHealth);

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                isDead = true;
                
            }
        }
        public void TakeDamage(int damage, string damageAnimation = "infantry_05_damage")
        {
            if (isDead)
                return;

            currentHealth = currentHealth - damage;
            enemyHealthBar.SetHealth(currentHealth);

            enemyAnimatorManager.PlayTargetAnimation(damageAnimation, true);

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
            Destroy(enemyCollider, 10);
            Destroy(gameObject, 15);
        }

    }
}
