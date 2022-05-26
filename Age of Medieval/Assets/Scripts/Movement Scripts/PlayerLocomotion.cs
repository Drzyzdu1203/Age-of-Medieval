using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Script2
{
    public class PlayerLocomotion : MonoBehaviour
    {
        Transform cameraObject;
        InputHandler inputHandler;
        PlayerManager playerManager;
        Vector3 moveDirection;

        [HideInInspector]
        public Transform myTransform;
        //Dodanie adnimatorHandler. Zanim to doda�em wsyzstko dzia�a�o.
        [HideInInspector]
        public AnimatorHandler animatorHandler;
        // END

        public new Rigidbody rigidbody;
        public GameObject normalCamera;

        [Header("Movement Stats")]
        [SerializeField]
        float movementSpeed = 5;
        [SerializeField]
        float rotationSpeed = 10;
        //sprint
        [SerializeField]
        float sprintSpeed = 7;


        void Start()
        {
            playerManager = GetComponent<PlayerManager>();
            rigidbody = GetComponent<Rigidbody>();
            inputHandler = GetComponent<InputHandler>();
            //Dodanie adnimatorHandler. Zanim to doda�em wsyzstko dzia�a�o.
            animatorHandler = GetComponentInChildren<AnimatorHandler>();
            //END
            cameraObject = Camera.main.transform;
            myTransform = transform;
            //Dodanie adnimatorHandler. Zanim to doda�em wsyzstko dzia�a�o.
            animatorHandler.Initialize();
            //END

        }

        

        #region Movement
        Vector3 normalVector;
        Vector3 targetPosition;

        private void HandleRotation(float delta)
        {
            Vector3 targetDir = Vector3.zero;
            float moveOverride = inputHandler.moveAmount;

            targetDir = cameraObject.forward * inputHandler.vertical;
            targetDir += cameraObject.right * inputHandler.horizontal;

            targetDir.Normalize();
            targetDir.y = 0;

            if (targetDir == Vector3.zero)
            {
                targetDir = myTransform.forward;
            }

            float rs = rotationSpeed;

            Quaternion tr = Quaternion.LookRotation(targetDir);
            Quaternion targetRotation = Quaternion.Slerp(myTransform.rotation, tr, rs * delta);

            myTransform.rotation = targetRotation;
        }


        public void HandleMovement(float delta)
        {

            //sprint
            if (inputHandler.rollFlag)
            {
                return;
            }

            moveDirection = cameraObject.forward * inputHandler.vertical;
            moveDirection += cameraObject.right * inputHandler.horizontal;
            moveDirection.Normalize();

            #region Naprawa wspinania si� w g�r�
            //Naprawia chodzenie do gURY
            moveDirection.y = 0;
            #endregion

            float speed = movementSpeed;

            //sprint
            if (inputHandler.sprintFlag)
            {
                speed = sprintSpeed;
                playerManager.isSprinting = true;
                moveDirection *= speed;
            }
            else
            {
                moveDirection *= speed;

            }


            Vector3 projectedVelocity = Vector3.ProjectOnPlane(moveDirection, normalVector);
            rigidbody.velocity = projectedVelocity;

            //Dodanie po animacji. Dzia�a�o przed tym pt.2
            //Dodanie sprintu
            animatorHandler.UpdateAnimatorValues(inputHandler.moveAmount, 0, playerManager.isSprinting);


            //Dodanie adnimatorHandler. Zanim to doda�em wsyzstko dzia�a�o.
            if (animatorHandler.canRotate)
            {
                HandleRotation(delta);
            }
            //END
        }

        public void HandleRollingAndSprinting(float delta)
        {
            if (animatorHandler.anim.GetBool("isinteracting"))
            {
                return;
            }

            if (inputHandler.rollFlag)
            {
                moveDirection = cameraObject.forward * inputHandler.vertical;
                moveDirection += cameraObject.right * inputHandler.horizontal;

                if (inputHandler.moveAmount > 0)
                {
                    animatorHandler.PlayTargetAnimation("Rolling", true);
                    moveDirection.y = 0;
                    Quaternion rollRotation = Quaternion.LookRotation(moveDirection);
                    myTransform.rotation = rollRotation;
                }
                else
                {
                    animatorHandler.PlayTargetAnimation("Backstep", true);
                }
            }
            if (inputHandler.sprintFlag)
            {
                playerManager.isSprinting = true;
            }
        }
        #endregion

    }
}
