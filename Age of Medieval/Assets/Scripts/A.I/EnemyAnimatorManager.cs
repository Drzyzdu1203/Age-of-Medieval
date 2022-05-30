using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AoM
{
    public class EnemyAnimatorManager : AnimatorManager
    {
        private void Awake()
        {
            anim = GetComponent<Animator>();
        }
    }
}
