using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Script2
{
    public class InputHandler : MonoBehaviour
    {
        public float horizontal;
        public float vertical;
        public float moveAmount;
        public float mouseX;
        public float mouseY;

        //Roll
        public bool b_Input;
        public bool rollFlag;
        public bool isinteracting;

        //dodanie sprintu
        public bool sprint_input;
        public float rollInputTimer;
        public bool sprintFlag;

        PlayerControls inputActions;
        #region Film z kamer¹2
        //Dodanie rzeczy z filmu trzeciego o kamera handling. przedtem dzia³a³o
        CameraHandler cameraHandler;
        //END
        #endregion

        Vector2 movementInput;
        Vector2 cameraInput;
        #region Film z kamer¹
        //Dodanie rzeczy z filmu trzeciego o kamera handling. przedtem dzia³a³o
        private void Awake()
        {
            cameraHandler = CameraHandler.singleton;
        }

        private void FixedUpdate()
        {
            float delta = Time.deltaTime;

            if (cameraHandler != null)
            {
                cameraHandler.FollowTarget(delta);
                cameraHandler.HandleCameraRotation(delta, mouseX, mouseY);
            }
        }
        //END
        #endregion
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
            System.Console.WriteLine("test");
            MoveInput(delta);
            HandleRollInput(delta);
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
            //b_Input = inputActions.PlayerActions.Roll.phase == UnityEngine.InputSystem.InputActionPhase.Started;
            b_Input = inputActions.PlayerActions.Roll.triggered;
            sprint_input = inputActions.PlayerActions.Sprint.phase == UnityEngine.InputSystem.InputActionPhase.Performed;


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
                sprintFlag = true;
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
    }
}
