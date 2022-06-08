using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AoM2
{
    public class InputManager : MonoBehaviour
    {
        PlayerControls2 playerControls;
        PlayerLocomotion playerLocomotion;
        AnimationManager animationManager;

        public Vector2 movementInput;
        public Vector2 cameraInput;

        public float cameraInputX;
        public float cameraInputY;

        public float moveAmount;
        public float verticalInput;
        public float horizontalInput;

        public bool sprint_input;
        public bool jump_input;
        //z innego
        public bool roll_input;
        public bool rollFlag;

        private void Awake()
        {
            animationManager = GetComponent<AnimationManager>();
            playerLocomotion = GetComponent<PlayerLocomotion>();
        }

        private void OnEnable()
        {
            if (playerControls == null)
            {
                playerControls = new PlayerControls2();
                playerControls.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();

                playerControls.PlayerMovement.Camera.performed += i => cameraInput = i.ReadValue<Vector2>();

                playerControls.PlayerActions.Sprint.performed += i => sprint_input = true;
                playerControls.PlayerActions.Sprint.canceled += i => sprint_input = false;
                playerControls.PlayerActions.Jump.performed += i => jump_input = true;
                //z innego
                playerControls.PlayerActions.Roll.performed += i => roll_input = true;
            }

            playerControls.Enable();
        }

        private void OnDisable()
        {
            playerControls.Disable();
        }

        public void HandleAllInputs()
        {
            HandleMovementInput();
            HandleSprintingInput();
            HandleJumpingInput();
            HandleRollInput();
        }

        private void HandleMovementInput()
        {
            verticalInput = movementInput.y;
            horizontalInput = movementInput.x;

            cameraInputX = cameraInput.x;
            cameraInputY = cameraInput.y;

            moveAmount = Mathf.Clamp01(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput));

            animationManager.UpdateAnimatorValues(0, moveAmount, playerLocomotion.isSprinting);
        }

        private void HandleSprintingInput()
        {
            if (sprint_input && moveAmount > 0.5f)
            {
                playerLocomotion.isSprinting = true;
            }
            else
            {
                playerLocomotion.isSprinting = false;
            }
        }

        private void HandleJumpingInput()
        {
            if (jump_input && !rollFlag)
            {
                jump_input = false;
                playerLocomotion.HandleJumping();
            }
        }

        //z innego
        private void HandleRollInput()
        {

            //roll_input = playerControls.PlayerActions.Roll.triggered;

            if (roll_input)
            {
                rollFlag = true;
                roll_input = false;
                playerLocomotion.HandleRolling();
                rollFlag = false;
            }
        }
    }
}
