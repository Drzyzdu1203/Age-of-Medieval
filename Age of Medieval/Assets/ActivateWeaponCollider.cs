using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AoM
{
    public class ActivateWeaponCollider : MonoBehaviour
    {
        private Animator anim;
        public BoxCollider boxCollider;
        public int currentWeaponDamage = 20;
        

        private void Start()
        {
            anim = GetComponentInParent<Animator>();
            //boxCollider.enabled = false;
        }
        private void Update()
        {

            if (anim.GetCurrentAnimatorStateInfo(1).IsName("infantry_04_attack_A") ||
                anim.GetCurrentAnimatorStateInfo(1).IsName("infantry_04_attack_B") ||
                anim.GetCurrentAnimatorStateInfo(1).IsName("infantry_04_attack_C")) 
            {
               // boxCollider.enabled = true;
            }

            else if (anim.GetCurrentAnimatorStateInfo(1).IsName("Empty"))
            {
               // boxCollider.enabled = false;
            }
        }
        private void OnTriggerEnter(Collider collision)
        {
            if (collision.tag == "Player")
            {
                PlayerStats playerStats = collision.GetComponent<PlayerStats>();




                if (playerStats != null)
                {
                    playerStats.TakeDamage(currentWeaponDamage);
                    
                }
                
            }

            if (collision.tag == "Enemy")
            {
                EnemyStats enemyStats = collision.GetComponent<EnemyStats>();
                


                if (enemyStats != null)
                {
                    enemyStats.TakeDamage(currentWeaponDamage);
                    
                }
                
            }
 
        }
        private void OnTriggerExit(Collider collision)
        {
            if (collision.tag == "Player")
            {
                boxCollider.enabled = false;
            }
            if (collision.tag == "Enemy")
            {
                boxCollider.enabled = false;
            }
        }
    }
}
