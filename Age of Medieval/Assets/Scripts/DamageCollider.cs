using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AoM
{
    public class DamageCollider : MonoBehaviour
    {
        Collider damageCollider;
        AudioSource audioSource;

        public AudioClip damage;
        public AudioClip whoosh;


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
            audioSource.PlayOneShot(whoosh);
        }
        public void DisaleDamageCollider()
        {
            damageCollider.enabled = false;
        }
        private void OnTriggerEnter(Collider collision)
        {
            if (collision.tag == "Player")
            {
                PlayerStats playerStats = collision.GetComponent<PlayerStats>();



                if (playerStats != null)
                {
                    playerStats.TakeDamage(currentWeaponDamage);
                    audioSource.PlayOneShot(damage);

                }

            }

            if (collision.tag == "Enemy")
            {
                EnemyStats enemyStats = collision.GetComponent<EnemyStats>();



                if (enemyStats != null)
                {
                    enemyStats.TakeDamage(currentWeaponDamage);
                    audioSource.PlayOneShot(damage);
                }
            }
        }
    }

}
