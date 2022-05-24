using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Script2
{
    public class PlayerLocomotion : MonoBehaviour
    {
        Transform cameraObject;
        InputHandler inputHandler;
        Vector3 moveDirection;

        [HideInInspector]
        public Transform myTransform;
        //Dodanie adnimatorHandler. Zanim to doda³em wsyzstko dzia³a³o.
        [HideInInspector]
        public AnimatorHandler animatorHandler;
        // END

        public new Rigidbody rigidbody;
        public GameObject normalCamera;

        [Header("Stats")]
        [SerializeField]
        float movementSpeed = 5;
        [SerializeField]
        float rotationSpeed = 10;

        void Start()
        {
            rigidbody = GetComponent<Rigidbody>();
            inputHandler = GetComponent<InputHandler>();
            //Dodanie adnimatorHandler. Zanim to doda³em wsyzstko dzia³a³o.
            animatorHandler = GetComponentInChildren<AnimatorHandler>();
            //END
            cameraObject = Camera.main.transform;
            myTransform = transform;
            //Dodanie adnimatorHandler. Zanim to doda³em wsyzstko dzia³a³o.
            animatorHandler.Initialize();
            //END

        }

        public void Update()
        {
            float delta = Time.deltaTime;

            inputHandler.TickInput(delta);

            moveDirection = cameraObject.forward * inputHandler.vertical;
            moveDirection += cameraObject.right * inputHandler.horizontal;
            moveDirection.Normalize();

            #region Naprawa wspinania siê w górê
            //Naprawia chodzenie do gURY
            moveDirection.y = 0;
            #endregion

            float speed = movementSpeed;
            moveDirection *= speed;

            Vector3 projectedVelocity = Vector3.ProjectOnPlane(moveDirection, normalVector);
            rigidbody.velocity = projectedVelocity;

            //Dodanie po animacji. Dzia³a³o przed tym pt.2
            animatorHandler.UpdateAnimatorValues(inputHandler.moveAmount, 0);


            //Dodanie adnimatorHandler. Zanim to doda³em wsyzstko dzia³a³o.
            if (animatorHandler.canRotate)
            {
                HandleRotation(delta);
            }
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
        #endregion
    }
}
