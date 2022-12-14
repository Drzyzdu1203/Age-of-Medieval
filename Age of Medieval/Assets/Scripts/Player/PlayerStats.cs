using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AoM

{
    public class PlayerStats : CharacterStats
    {
        PlayerManager playerManager;
        
        HealthBar healthBar;
        StaminaBar staminaBar;
        ManaBar manaBar;
        PlayerAnimatorManager animatorHandler;
        EnemyManager enemyManager;


        public UIManager gameManager;

        public float staminaRegenerationAmount = 1;
        public float staminaRegenTimer = 0;
        private void Awake()
        {
            playerManager = GetComponent<PlayerManager>();
            enemyManager = GetComponent<EnemyManager>();
            healthBar = FindObjectOfType<HealthBar>();
            staminaBar = FindObjectOfType<StaminaBar>();
            manaBar = FindObjectOfType<ManaBar>();
            animatorHandler = GetComponentInChildren<PlayerAnimatorManager>();
        }

        void Start()
        {
            maxHealth = SetMaxHealthFromHealthLevel();
            currentHealth = maxHealth;
            healthBar.SetMaxHealth(maxHealth);
            healthBar.SetCurrentHealth(currentHealth);

            maxStamina = SetMaxStaminaFromStaminaLevel();
            currentStamina = maxStamina;
            staminaBar.SetMaxStamina(maxStamina);
            staminaBar.SetCurrentStamina(currentStamina);

            maxMana = SetMaxManaFromManaLevel();
            currentMana = maxMana;
            manaBar.SetMaxMana(maxMana);
            manaBar.SetCurrentMana(currentMana);
        }

        private int SetMaxHealthFromHealthLevel()
        {
            maxHealth = healthLevel * 10;
            return maxHealth;
        }
        private float SetMaxStaminaFromStaminaLevel()
        {
            maxStamina = staminaLevel * 10;
            return maxStamina;
        }
        private float SetMaxManaFromManaLevel()
        {
            maxMana = manaLevel * 10;
            return maxMana;
        }
        public void TakeDamage (int damage, string damageAnimation = "infantry_05_damage") 
        {
            if (playerManager.isInvulerable)
                return;

            if (isDead)
                return;

            currentHealth = currentHealth - damage;
            healthBar.SetCurrentHealth(currentHealth);

            animatorHandler.PlayTargetAnimation(damageAnimation, true);

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                animatorHandler.PlayTargetAnimation("twohanded_06_death_B", true);
                gameManager.gameOver();
                isDead = true;
                
            }
            if (isDead)
            {
                playerManager.enabled = false;
            }

            
        }
        public void TakeDamageNoAnimation(int damage)
        {
            currentHealth = currentHealth - damage;

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                isDead = true;
                
            }
        }
        public void TakeStaminaDamage(int damage)
        {
            currentStamina = currentStamina - damage;
            staminaBar.SetCurrentStamina(currentStamina);
        }

        public void RegenerateStamina()
        {
            if (playerManager.isinteracting)
            {
                staminaRegenTimer = 0;
            }
            else
            {
                staminaRegenTimer += Time.deltaTime;

                if (currentStamina < maxStamina && staminaRegenTimer > 0.5f)
                {
                    currentStamina += staminaRegenerationAmount * Time.deltaTime;
                    staminaBar.SetCurrentStamina(Mathf.RoundToInt(currentStamina));
                }
            }
        }
        public void HealPlayer(int healAmount)
        {
            currentHealth = currentHealth + healAmount; 
            if(currentHealth > maxHealth)
            {
                currentHealth = maxHealth;
            }

            healthBar.SetCurrentHealth(currentHealth);
        }
        public void DeductMana(int mana)
        {
            currentMana = currentMana - mana;

            if(currentMana <0)
            {
                currentMana = 0;
            }
            manaBar.SetCurrentMana(currentMana);
        }

        public void AddSouls(int souls)
        {
            soulCount = soulCount + souls;
        }
    }
}   
