using UnityEngine;

namespace Game.CameraManagement {
    public class CameraFollow : MonoBehaviour {
        public Transform Target => target;
        
        [SerializeField] private Transform target;
        [SerializeField, Range(0.01f, 0.5f)] private float cameraHeight = 0.25f;
        [SerializeField, Range(1.0f, 100.0f)] private float distance = 12.0f;
        [SerializeField, Range(1.0f, 10.0f)] private float rotationSpeed = 3.0f;
        [SerializeField, Range(0.01f, 1.0f)] private float smoothTime = 0.2f;

        private Vector3 cameraPos;
        private Vector3 velocity;
        private Transform t;
        
        private float angle;

        private void Awake() {
            t = transform;
        }

        private void FixedUpdate() {
            FollowPlayer();
        }

        private void FollowPlayer() {
            cameraPos = target.position - (target.forward * distance) + target.up * distance * cameraHeight;
            t.position = Vector3.SmoothDamp(t.position, cameraPos, ref velocity, smoothTime);
            angle = Mathf.Abs(Quaternion.Angle(t.rotation, target.rotation));
            t.rotation = Quaternion.RotateTowards(
                t.rotation, target.rotation, (angle * rotationSpeed) * Time.deltaTime);
        }   
    }
}
