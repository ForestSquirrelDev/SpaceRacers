using Game.Ship;
using UnityEngine;
using Utils.Maths;

namespace Game.Shooting {
    [RequireComponent(typeof(Rigidbody))]
    public class LaserBeam : MonoBehaviour {
        public Rigidbody Rigidbody => rigidbody;
        
        private new Rigidbody rigidbody;
        private Transform target;
        private PID xPID, yPID, zPID = new();

        private float yieldTime = 0.5f;
        private const float speed_multiplyer = 50f;
        private const float rotation_speed = 5f;
        
        public void Init(ITargetable target) {
            this.target = target?.GetTransform();
            this.rigidbody = GetComponent<Rigidbody>();
        }

        private void FixedUpdate() {
            yieldTime -= Time.fixedDeltaTime;
            if (yieldTime > 0 || target == null) return;
            // transform.rotation = QuaternionExtensions.SmoothRotateTowardsTarget(
            //     transform, target, rotation_speed, Time.fixedDeltaTime);
            // rigidbody.AddForce(transform.forward * speed_multiplyer, ForceMode.Acceleration);
        }

        private void OnTriggerEnter(Collider other) {
            if (other.TryGetComponent(out IDestructible destructible)) {
                destructible.Destruct();
            }
            Destroy(gameObject);
        }
    }
}