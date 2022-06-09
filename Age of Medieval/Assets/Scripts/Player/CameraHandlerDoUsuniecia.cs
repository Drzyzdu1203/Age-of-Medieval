//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//namespace AoM
//{
//    public class CameraHandler : MonoBehaviour
//    {
//        public Transform targetTransform;
//        public Transform cameraTransform;
//        public Transform cameraPivotTransform;
//        private Transform myTransform;
//        private Vector3 cameraTransformPosition;
//        private LayerMask ignoreLayers;

//        //Usprawnienie kamery
//        private Vector3 cameraFollowVelocity = Vector3.zero;

//        public static CameraHandler singleton;

//        public float lookSpeed = 0.1f;
//        public float followSpeed = 0.1f;
//        public float pivotSpeed = 0.03f;

//        //kolizja kamery
//        private float targetPosition;

//        private float defaultPosition;
//        private float lookAngle;
//        private float pivotAngle;
//        public float minimumPivot = -35;
//        public float maximumPivot = 35;

//        //kolizja
//        public float cameraSphereRadius = 0.2f;
//        public float cameraCollisionOffSet = 0.2f;
//        public float minimumCollisionOffSet = 0.2f;


//        private void Awake()
//        {
//            singleton = this;
//            myTransform = transform;
//            defaultPosition = cameraPivotTransform.localPosition.z;
//            ignoreLayers = ~(1 << 8 | 1 << 9 | 1 << 10);

//        }

//        public void FollowTarget(float delta)
//        {
//            Vector3 targetPosition = Vector3.SmoothDamp(myTransform.position, targetTransform.position, ref cameraFollowVelocity, delta / followSpeed);
//            myTransform.position = targetPosition;
//            //Kolizja kamery
//            HandleCameraCollision(delta);
//        }

//        public void HandleCameraRotation(float delta, float mouseXInput, float mouseYInput)
//        {
//            lookAngle += (mouseXInput * lookSpeed) / delta;
//            pivotAngle -= (mouseYInput * pivotSpeed) / delta;
//            pivotAngle = Mathf.Clamp(pivotAngle, minimumPivot, maximumPivot);

//            Vector3 rotation = Vector3.zero;
//            rotation.y = lookAngle;
//            Quaternion targetRotation = Quaternion.Euler(rotation);
//            myTransform.rotation = targetRotation;

//            rotation = Vector3.zero;
//            rotation.x = pivotAngle;

//            targetRotation = Quaternion.Euler(rotation);
//            cameraPivotTransform.localRotation = targetRotation;
//        }


//        //Kolizja kamery
//        private void HandleCameraCollision(float delta)
//        {
//            targetPosition = defaultPosition;
//            RaycastHit hit;
//            Vector3 direction = cameraTransform.position - cameraPivotTransform.position;
//            direction.Normalize();

//            if (Physics.SphereCast(cameraPivotTransform.position, cameraSphereRadius, direction, out hit, Mathf.Abs(targetPosition), ignoreLayers))
//            {
//                float dis = Vector3.Distance(cameraPivotTransform.position, hit.point);
//                targetPosition = -(dis - cameraCollisionOffSet);
//            }
//            if (Mathf.Abs(targetPosition) < minimumCollisionOffSet)
//            {
//                targetPosition = -minimumCollisionOffSet;
//            }

//            cameraTransformPosition.z = Mathf.Lerp(cameraTransform.localPosition.z, targetPosition, delta / 0.2f);
//            cameraTransform.localPosition = cameraTransformPosition;
//        }
//    }
//}
