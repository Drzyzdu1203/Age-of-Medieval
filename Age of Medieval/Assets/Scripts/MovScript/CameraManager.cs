using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AoM
{
    public class CameraManager : MonoBehaviour
    {
        InputManager inputManager;


        public Transform targetTransform;  // obiekt za którym kamera pod¹¿a 
        public Transform cameraPivot;      // góra / dó³ kamera
        public Transform cameraTransform;
        public LayerMask collisionLayers;
        private float defaultPosition;
        private Vector3 cameraFollowVelocity = Vector3.zero;
        private Vector3 cameraVectorPosition;


        public float cameraCollisionOffSet = 0.2f; // odleg³oœæ odsuniêcia kamery od obiektu
        public float minimumCollisionOffSet = 0.2f;
        public float cameraCollisionRadius = 0.2f;
        public float cameraFollowSpeed = 0.2f;
        public float cameraLookSpeed = 0.5f;
        public float cameraPivotSpeed = 0.5f;

        public float lookAngle; //góra / dó³
        public float pivotAngle; // lewo / prawo
        public float minimumPivotAngle = -35;
        public float maximumPivotAngle = 35;

        private void Awake()
        {
            targetTransform = FindObjectOfType<PlayerManager>().transform;
            inputManager = FindObjectOfType<InputManager>();
            cameraTransform = Camera.main.transform;
            defaultPosition = cameraTransform.localPosition.z;
        }

        public void HandleAllCameraMovement()
        {
            FolowTarget();
            RotateCamera();
            HandleCameraCollisions();
        }

        private void FolowTarget()
        {
            Vector3 targetPosition = Vector3.SmoothDamp(transform.position, targetTransform.position, ref cameraFollowVelocity, cameraFollowSpeed);
            transform.position = targetPosition;
        }

        private void RotateCamera()
        {
            Vector3 rotation;
            Quaternion targetRotation;


            lookAngle = lookAngle + (inputManager.cameraInputX * cameraLookSpeed);
            pivotAngle = pivotAngle - (inputManager.cameraInputY * cameraPivotSpeed);

            pivotAngle = Mathf.Clamp(pivotAngle, minimumPivotAngle, maximumPivotAngle);

            rotation = Vector3.zero;
            rotation.y = lookAngle;
            targetRotation = Quaternion.Euler(rotation);
            transform.rotation = targetRotation;

            rotation = Vector3.zero;
            rotation.x = pivotAngle;

            targetRotation = Quaternion.Euler(rotation);
            cameraPivot.localRotation = targetRotation;
        }

        private void HandleCameraCollisions()
        {
            float targetPosition = defaultPosition;
            RaycastHit hit;
            Vector3 direction = cameraTransform.position - cameraPivot.position;
            direction.Normalize();

            if (Physics.SphereCast(cameraPivot.transform.position, cameraCollisionRadius, direction, out hit, Mathf.Abs(targetPosition), collisionLayers))
            {
                float distance = Vector3.Distance(cameraPivot.position, hit.point);
                targetPosition =- targetPosition - (distance - cameraCollisionOffSet);
            }

            if (Mathf.Abs(targetPosition) < minimumCollisionOffSet)
            {
                targetPosition = targetPosition - minimumCollisionOffSet;
            }

            cameraVectorPosition.z = Mathf.Lerp(cameraTransform.localPosition.z, targetPosition, 0.2f);
            cameraTransform.localPosition = cameraVectorPosition;
        }
    }
}
