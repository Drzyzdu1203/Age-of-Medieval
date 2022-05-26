using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Script2
{
    public class PlayerManager : MonoBehaviour
    {
        InputHandler inputHandler;
        Animator anim;
        CameraHandler cameraHandler;
        PlayerLocomotion playerLocomotion;



        public bool isinteracting;
        //Dodanie sprintu
        [Header("Player Flags")]
        public bool isSprinting;
        // Start is called before the first frame update
        void Start()
        {
            inputHandler = GetComponent<InputHandler>();
            anim = GetComponentInChildren<Animator>();
            playerLocomotion = GetComponent<PlayerLocomotion>();
            cameraHandler = CameraHandler.singleton;
        }

        

        // Update is called once per frame
        void Update()
        {
            float delta = Time.deltaTime;
            isinteracting = anim.GetBool("isinteracting");


            inputHandler.TickInput(delta);
            playerLocomotion.HandleMovement(delta);
            //rolling
            playerLocomotion.HandleRollingAndSprinting(delta);
        }

        private void FixedUpdate()
        {
            float delta = Time.deltaTime;

            if (cameraHandler != null)
            {
                cameraHandler.FollowTarget(delta);
                cameraHandler.HandleCameraRotation(delta, inputHandler.mouseX, inputHandler.mouseY);
            }
        }

        private void LateUpdate()
        {

            inputHandler.rollFlag = false;
            inputHandler.sprintFlag = false;
            isSprinting = inputHandler.sprint_input;
        }
    }
}
