using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AoM
{
    public class PlayerManager : MonoBehaviour
    {
        InputManager inputManager;
        PlayerLocomotion playerLocomotion;
        private void Awake()
        {
            inputManager = GetComponent<InputManager>();
            playerLocomotion = GetComponent<PlayerLocomotion>();
        }

        private void Update()
        {
            inputManager.HandleAllInputs();

        }
        private void FixedUpdate()
        {
            playerLocomotion.HandleAllMovement();
        }
    }
}
