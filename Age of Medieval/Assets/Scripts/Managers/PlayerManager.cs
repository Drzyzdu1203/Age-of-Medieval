using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AoM
{
    public class PlayerManager : MonoBehaviour
    {
        Animator animator;
        InputManager inputManager;
        CameraManager cameraManager;
        PlayerLocomotion playerLocomotion;

        public bool isinteracting;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            inputManager = GetComponent<InputManager>();
            cameraManager = FindObjectOfType<CameraManager>();
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
        private void LateUpdate()
        {
            cameraManager.HandleAllCameraMovement();

            isinteracting = animator.GetBool("isInteracting");
            playerLocomotion.isJumping = animator.GetBool("isJumping");
            animator.SetBool("isGrounded", playerLocomotion.isGrounded);
        }
    }
}
