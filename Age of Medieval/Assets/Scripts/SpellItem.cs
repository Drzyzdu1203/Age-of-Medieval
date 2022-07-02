﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AoM
{
    public class SpellItem : Item
    {
        public GameObject spellWarmUpFX;
        public GameObject spellCastFX;
        public string spellAnimation;

        [Header("Spell Cost")]
        public int manaCost;

        [Header("Spell Type")]
        public bool isFaithSpell;
        public bool isMagicSpell;
        public bool isPyroSpell;

        [Header("Spell Description")]
        [TextArea]
        public string spellDescription;

        public virtual void AttemptToCastSpell(PlayerAnimatorManager animatorHandler, PlayerStats playerStats)
        {
            Debug.Log("You attempt to cast a spell!");
        }

        public virtual void SuccessfullyCastSpell(PlayerAnimatorManager animatorHandler, PlayerStats playerStats)
        {
            Debug.Log("You successfully cast a spell!");
            playerStats.DeductMana(manaCost);
        }
    }
}