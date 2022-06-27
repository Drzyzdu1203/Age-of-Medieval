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

        [Header("Attack Animations")]
        public string infantry_04_attack_A;
        public string infantry_04_attack_B;
        public string infantry_04_attack_C;       
        public string meleeAttack_TwoHanded;
        public string th_light_attack_01;
        public string th_light_attack_02;

        [Header("Idle Animations")]
        public string th_idle;

        [Header("Stamina Costs")]
        public int baseStamina;
        public float lightAttackMultiplier;
        public float heavyAttackMultiplier;
    }
}
