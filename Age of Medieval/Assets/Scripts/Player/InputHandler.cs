using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AoM
{
    public class InputHandler : MonoBehaviour
    {
        public float horizontal;
        public float vertical;
        public float moveAmount;
        public float mouseX;
        public float mouseY;

        public bool lightAttack_Input;
        public bool heavyAttack_Input;

        public bool b_Input;
        public bool jump_input;//
        public bool sprint_input;//
        public bool rollFlag;
        public bool sprintFlag;
        public float rollInputTimer;
        
        PlayerControls inputActions;
        PlayerAttacker playerAttacker;
        PlayerInventory playerInventory;

        Vector2 movementInput;
        Vector2 cameraInput;

        private void Awake()
        {
            playerAttacker = GetComponent<PlayerAttacker>();
            playerInventory = GetComponent<PlayerInventory>();  
        }
        public void OnEnable()
        {
            if (inputActions == null)
            {
                inputActions = new PlayerControls();
                inputActions.PlayerMovement.Movement.performed += inputActions => movementInput = inputActions.ReadValue<Vector2>();
                inputActions.PlayerMovement.Camera.performed += i => cameraInput = i.ReadValue<Vector2>();
            }

            inputActions.Enable();
        }

        private void OnDisable()
        {
            inputActions.Disable();
        }

        public void TickInput(float delta)
        {
            MoveInput(delta);
            HandleRollInput(delta);
            HandleAttackInput(delta);
        }

        private void MoveInput(float delta)
        {
            horizontal = movementInput.x;
            vertical = movementInput.y;
            moveAmount = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));
            mouseX = cameraInput.x;
            mouseY = cameraInput.y;
        }

        private void HandleRollInput(float delta)
        {
            b_Input = inputActions.PlayerActions.Roll.phase == UnityEngine.InputSystem.InputActionPhase.Started;
            b_Input = inputActions.PlayerActions.Roll.triggered;//
            sprint_input = inputActions.PlayerActions.Sprint.phase == UnityEngine.InputSystem.InputActionPhase.Performed; //


            if (sprint_input)
            {
                sprintFlag = true;
                Debug.Log("Wcisniety Shift");
            }
            else
            {
                sprintFlag = false;
                
            }

            if (b_Input)
            {
                rollInputTimer += delta;             
            }
            else
            {
                if (rollInputTimer > 0 && rollInputTimer < 0.5f)
                {
                    //sprintFlag = false;
                    rollFlag = true;
                }

                rollInputTimer = 0;
            }
        }

        private void HandleAttackInput(float delta)
        {
            inputActions.PlayerActions.LightAttack.performed += i => lightAttack_Input = true;
            inputActions.PlayerActions.HeavyAttack.performed += i => heavyAttack_Input = true;

            if(lightAttack_Input)
            {
                playerAttacker.HandleLightAttack(playerInventory.rightWeapon);
            }

            if (heavyAttack_Input)
            {
                playerAttacker.HandleHeavyAttack(playerInventory.rightWeapon);
            }
        }
    }
}
