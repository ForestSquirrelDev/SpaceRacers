using UnityEngine;

namespace Game
{
    public class CameraFollow : MonoBehaviour {
        [SerializeField] private Transform target;
        [Range(0.01f, 0.5f)]
        [SerializeField]
        private float cameraHeight = 0.25f;
        [Range(1.0f, 100.0f)]
        [SerializeField]
        private float distance = 12.0f;
        [Range(1.0f, 10.0f)]
        [SerializeField]
        private float rotationSpeed = 3.0f;
        [Range(0.01f, 1.0f)]
        [SerializeField]
        private float smoothTime = 0.2f;

        private Vector3 cameraPos;
        private Vector3 velocity;

        private float angle;

        private void Awake() {
            if (target == null) {
                Debug.LogWarning($"Field '{nameof(target)}' is not set in the inspector for {gameObject.name}. Script will be disabled");
                enabled = false;
            }
        }

        private void FixedUpdate() {
            FollowPlayer();
        }

        private void FollowPlayer() {
            cameraPos = target.position - (target.forward * distance) + target.up * distance * cameraHeight;

            transform.position = Vector3.SmoothDamp(transform.position, cameraPos, ref velocity, smoothTime);

            angle = Mathf.Abs(Quaternion.Angle(transform.rotation, target.rotation));

            transform.rotation = Quaternion.RotateTowards(from: transform.rotation,
                                                              to: target.rotation,
                                                              maxDegreesDelta: (angle * rotationSpeed) * Time.deltaTime);
        }
    }
}
