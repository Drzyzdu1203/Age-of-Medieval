using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AoM
{
    public class AnimatorManager : MonoBehaviour
    {
        public Animator anim;
        public bool canRotate;
        public void PlayTargetAnimation(string targetAnim, bool isinteracting)
        {
            anim.applyRootMotion = isinteracting;
            anim.SetBool("canRotate", false);
            anim.SetBool("isinteracting", isinteracting);
            anim.CrossFade(targetAnim, 0.2f);
        }
        public virtual void TakeCriticalDamageAnimationEvent()
        {

        }
    }
}
