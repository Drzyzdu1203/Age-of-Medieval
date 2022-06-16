using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace AoM
{
    public class EnemyStats : CharacterStats
    {


        Animator animator;

        private void Awake()
        {
            animator = GetComponentInChildren<Animator>();
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
            currentHealth = currentHealth - damage;

            animator.Play("infantry_05_damage");

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                animator.Play("twohanded_06_death_B");
            }
        }
    }
}
