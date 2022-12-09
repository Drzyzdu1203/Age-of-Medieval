using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AoM
{
    public class DamageCollider : MonoBehaviour
    {
        public CharacterManager characterManager;
        Collider damageCollider;
        public AudioSource audioSource;
        public AudioClip parrySound;
        public AudioClip damage;
        public AudioClip woosh;
        public int currentWeaponDamage;

        private void Awake()
        {
            damageCollider = GetComponent<Collider>();
            damageCollider.gameObject.SetActive(true);
            damageCollider.isTrigger = true;
            damageCollider.enabled = false;
            audioSource = GetComponent<AudioSource>();
        }


        public void EnableDamageCollider()
        {
            damageCollider.enabled = true;
            audioSource.PlayOneShot(woosh);
        }
        public void DisaleDamageCollider()
        {
            damageCollider.enabled = false;
        }
       
    }

}
