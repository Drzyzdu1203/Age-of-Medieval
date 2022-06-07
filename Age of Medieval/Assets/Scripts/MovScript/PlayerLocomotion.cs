using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AoM
{
    public class PlayerLocomotion : MonoBehaviour
    {
        PlayerManager playerManager;
        AnimationManager animatorManager;
        InputManager inputManager;
        Vector3 moveDirection;
        Transform cameraObject;
        Rigidbody playerRigidbody;

        [Header("Falling")]
        public float inAirTimer;
        public float leapingVelocity;
        public float fallingVelocity;
        public float rayCastHeightOffSet = 0.5f;
        public LayerMask groundLayer;


        [Header("Movement Flags")]
        public bool isSprinting;
        public bool isGrounded;
        public bool isJumping;

        [Header("Movement Speeds")]
        public float walkingSpeed = 1.5f;
        public float runningSpeed = 5;
        public float sprintingSpeed = 7;
        public float rotationSpeed = 5;

        [Header("Jump Speeds")]
        public float jumpHeight = 3;
        public float gravityIntensity = -15;

        private void Awake()
        {
            playerManager = GetComponent<PlayerManager>();
            animatorManager = GetComponent<AnimationManager>();
            inputManager = GetComponent<InputManager>();
            playerRigidbody = GetComponent<Rigidbody>();
            cameraObject = Camera.main.transform;
        }

        public void HandleAllMovement()
        {


            HandleFallingAndLanding();
            if (playerManager.isinteracting)
            {
                return;
            }
            HandleMovement();
            HandleRotation();
        }

        private void HandleMovement()
        {

            if (isJumping)
            {
                return;
            }

            moveDirection = cameraObject.forward * inputManager.verticalInput;
            moveDirection = moveDirection + cameraObject.right * inputManager.horizontalInput;
            moveDirection.Normalize();
            moveDirection.y = 0;

            if (isSprinting)
            {
                moveDirection = moveDirection * sprintingSpeed;
            }
            else
            {
                if (inputManager.moveAmount >= 0.5f)
                {
                    moveDirection = moveDirection * runningSpeed;
                }
                else
                {
                    moveDirection = moveDirection * walkingSpeed;
                }
            }

            

            Vector3 movementVelocity = moveDirection;
            playerRigidbody.velocity = movementVelocity;
        }

        private void HandleRotation()
        {

            if (isJumping)
            {
                return;
            }
            Vector3 targetDirection = Vector3.zero;

            targetDirection = cameraObject.forward * inputManager.verticalInput;
            targetDirection = targetDirection + cameraObject.right * inputManager.horizontalInput;
            targetDirection.Normalize();
            targetDirection.y = 0;

            if (targetDirection == Vector3.zero)
            {
                targetDirection = transform.forward;
            }

            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
            Quaternion playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            transform.rotation = playerRotation;
        }

        private void HandleFallingAndLanding()
        {
            RaycastHit hit;
            Vector3 rayCastOrigin = transform.position;
            rayCastOrigin.y = rayCastOrigin.y + rayCastHeightOffSet;

            if (!isGrounded && !isJumping)
            {
                if (!playerManager.isinteracting)
                {
                    animatorManager.PlayTargetAnimation("Falling", true);
                }

                inAirTimer = inAirTimer + Time.deltaTime;

                playerRigidbody.AddForce(transform.forward * leapingVelocity);
                playerRigidbody.AddForce(Vector3.down * fallingVelocity * inAirTimer);
            }

            if (Physics.SphereCast(rayCastOrigin, 0.2f, Vector3.down, out hit, 0.5f, groundLayer))
            {
                if (!isGrounded && !playerManager.isinteracting)
                {
                    animatorManager.PlayTargetAnimation("Land", true);
                }
                inAirTimer = 0;
                isGrounded = true;
            }
            else
            {
                isGrounded = false;
            }
        }

        public void HandleJumping()
        {
            if (isGrounded)
            {
                animatorManager.animator.SetBool("isJumping", true);
                animatorManager.PlayTargetAnimation("Jump", false);

                float jumpingVelocity = Mathf.Sqrt(-2 * gravityIntensity * jumpHeight);
                Vector3 playerVelocity = moveDirection;
                playerVelocity.y = jumpingVelocity;
                playerRigidbody.velocity = playerVelocity;
            }
        }
    }
}
