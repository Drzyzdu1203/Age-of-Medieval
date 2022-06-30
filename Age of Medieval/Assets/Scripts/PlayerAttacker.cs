using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AoM
{
    public class PlayerAttacker : MonoBehaviour
    {
        AnimatorHandler animatorHandler;
        PlayerManager playerManager;
        PlayerStats playerStats;
        PlayerInventory playerInventory;
        InputHandler inputHandler;
        WeaponSlotManager weaponSlotManager;
        public string lastAttack;

        private void Awake()
        {
            animatorHandler = GetComponent<AnimatorHandler>();
            playerManager = GetComponentInParent<PlayerManager>();
            playerStats = GetComponentInParent<PlayerStats>();
            playerInventory = GetComponentInParent<PlayerInventory>();
            weaponSlotManager = GetComponent<WeaponSlotManager>();
            inputHandler = GetComponentInParent<InputHandler>();
        }
        public void HandleWeaponCombo(WeaponItem weapon)
        {
            if (inputHandler.comboFlag)
            {
                animatorHandler.anim.SetBool("canDoCombo", false);

                if (lastAttack == weapon.infantry_04_attack_A)
                {
                    animatorHandler.PlayTargetAnimation(weapon.infantry_04_attack_C, true);

                }
            }
        }
        public void HandleLightAttack(WeaponItem weapon)
        {
            weaponSlotManager.attackingWeapon = weapon;
            animatorHandler.PlayTargetAnimation(weapon.infantry_04_attack_A, true);
            lastAttack = weapon.infantry_04_attack_A;
        }
        public void HandleHeavyAttack(WeaponItem weapon)
        {
            weaponSlotManager.attackingWeapon = weapon;
            animatorHandler.PlayTargetAnimation(weapon.meleeAttack_TwoHanded, true);
            lastAttack = weapon.meleeAttack_TwoHanded;
        }
        #region Input Actions
        public void HandleLightAttackAction()
        {
            if (playerInventory.rightWeapon.isMeleeWeapon)
            {
                PerformLightAttackMeleeAction();
            }
            else if (playerInventory.rightWeapon.isSpellCaster || playerInventory.rightWeapon.isFaithCaster || playerInventory.rightWeapon.isPyroCaster)
            {
                PerformLightAttackMagicAction(playerInventory.rightWeapon);
            }
        }
        #endregion

        #region Attack Actions
        private void PerformLightAttackMeleeAction()
        {
            if (playerManager.canDoCombo)
            {
                inputHandler.comboFlag = true;
                HandleWeaponCombo(playerInventory.rightWeapon);
                inputHandler.comboFlag = false;
            }
            else
            {
                if (playerManager.isinteracting)
                    return;

                if (playerManager.canDoCombo)
                    return;

                animatorHandler.anim.SetBool("isUsingRightHand", true);
                HandleLightAttack(playerInventory.rightWeapon);
            }
        }

        private void PerformLightAttackMagicAction(WeaponItem weapon)
        {
            if (playerManager.isinteracting)
                return;

            if (weapon.isFaithCaster)
            {
                if (playerInventory.currentSpell != null && playerInventory.currentSpell.isFaithSpell)
                {
                    if (playerStats.currentMana >= playerInventory.currentSpell.manaCost)
                    {
                        playerInventory.currentSpell.AttemptToCastSpell(animatorHandler, playerStats);
                    }
                    else
                    {
                        animatorHandler.PlayTargetAnimation("male_idle_pant", true);
                    }
                }
            }      
        }
        
        private void SuccessfullyCastSpell()
        {
            playerInventory.currentSpell.SuccessfullyCastSpell(animatorHandler, playerStats);
        }
        #endregion
    }
}

