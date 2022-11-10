using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AoM
{
    [CreateAssetMenu(menuName = "Items/Weapon Item")]
    public class WeaponItem : Item
    {
        public GameObject modelPrefab;
        public bool isUnarmed;

        [Header("Damage")]
        public int baseDamage = 25;
        public int criticalDamageMuiltiplier = 4;

        [Header("Attack Animations")]
        public string OH_Light_Attack_1;
        public string OH_Light_Attack_2;
        public string OH_Light_Attack_3;

        public string OH_Heavy_Attack_1;
        public string OH_Heavy_Attack_2;

        public string TH_Light_Attack_1;
        public string TH_Light_Attack_2;
        public string TH_Light_Attack_3;

        public string TH_Heavy_Attack_1;
        public string TH_Heavy_Attack_2;

        [Header("Weapon Art")]
        public string weapon_art;


        [Header("Idle Animations")]
        public string th_idle;

        [Header("Stamina Costs")]
        public int baseStamina;
        public float lightAttackMultiplier;
        public float heavyAttackMultiplier;

        [Header("Weapon Type")]
        public bool isSpellCaster;
        public bool isFaithCaster;
        public bool isPyroCaster;
        public bool isMeleeWeapon;
        public bool isShieldWeapon;
    }
}
