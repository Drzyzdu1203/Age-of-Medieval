using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AoM
{
    public class EnemyManager : CharacterManager
    {
        bool isPreformingAction;

        [Header("A.I Settings")]
        public float detectionRadius;
        private void Awake()
        {

        }
        private void Update()
        {

        }
        private void HandleCurrentAcrion()
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, enemyManager.detectionRadius, detectionLayer);

            for (int i = 0; i < colliders.Length; i++)
            {

            }
        }

    }
}
