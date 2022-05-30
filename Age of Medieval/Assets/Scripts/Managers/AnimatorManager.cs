using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AoM
{
    public class AnimatorManager : MonoBehaviour
    {
        public Animator anim;
        public void PlayTargetAnimation(string targetAnim, bool isinteracting)
        {
            anim.applyRootMotion = isinteracting;
            anim.SetBool("isinteracting", isinteracting);
            anim.CrossFade(targetAnim, 0.2f);
        }

    }
}
