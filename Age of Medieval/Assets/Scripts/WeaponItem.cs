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

        [Header("One Handed Attack Animations")]
        public string infantry_04_attack_A;
        public string infantry_04_attack_B;
        public string infantry_04_attack_C;       
        public string meleeAttack_TwoHanded;

        [Header("Stamina Costs")]
        public int baseStamina;
        public float lightAttackMultiplier;
        public float heavyAttackMultiplier;
    }
}
