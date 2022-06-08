using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AoM
{
    public class EnemyAnimatorManager : MonoBehaviour
    {
        EnemyLocomotionManager enemyLocomotionManager;
        private void Awake()
        {
            anim = GetComponent<Animator>();
            enemyLocomotionManager = GetComponentInParent<EnemyLocomotionManager>();
        }

        private void OnAnimatorMove()
        {
            float delta = Time.deltaTime;
            enemyLocomotionManager.enemyRigidBody.drag = 0;
            Vector3 deltaPosition = anim.deltaPosition;
            deltaPosition.y = 0;
            Vector3 velocity = deltaPosition / delta;
            enemyLocomotionManager.enemyRigidBody.velocity = velocity;
        }
        public Animator anim;
        public void PlayTargetAnimation(string targetAnim, bool isinteracting)
        {
            anim.applyRootMotion = isinteracting;
            anim.SetBool("isinteracting", isinteracting);
            anim.CrossFade(targetAnim, 0.2f);
        }
    }
}
