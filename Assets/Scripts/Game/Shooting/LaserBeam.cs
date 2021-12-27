using Game.Configs.Shooting;
using UnityEngine;
using Utils.Maths;

namespace Game.Shooting {
    [RequireComponent(typeof(Rigidbody))]
    public class LaserBeam : MonoBehaviour {
        public Rigidbody Rigidbody => rigidbody;
        
        private new Rigidbody rigidbody;
        private Transform target;
        private LaserBeamConfig config;
        private Transform thisTransform;
        private Vector3 lastPosition;

        private float distanceTraveled;
        private float yieldTime;
        
        public void Init(ITargetable target, LaserBeamConfig config) {
            yieldTime = config.onEnableYieldTime;
            this.target = target?.GetTransform();
            this.config = config;
            this.rigidbody = GetComponent<Rigidbody>();
            thisTransform = transform;
        }

        private void FixedUpdate() {
            yieldTime -= Time.fixedDeltaTime;
            if (yieldTime > 0) return;
            distanceTraveled += Vector3.Distance(thisTransform.position, lastPosition);
            lastPosition = thisTransform.position;
            if (target != null && target.gameObject.activeSelf) {
                transform.rotation = QuaternionExtensions.SmoothRotateTowardsTarget(
                    transform, target, config.rotationSpeed, Time.fixedDeltaTime);
            }
            rigidbody.AddForce(thisTransform.forward * config.accelerationSpeed);
            if (distanceTraveled >= config.disablingDistance) {
                ResetLaserBeam();
            }
        }
        
        private void OnTriggerEnter(Collider other) {
            if (other.GetComponent<LaserBeam>() != null) return;
            if (other.TryGetComponent(out IDestructible destructible))
                destructible.Destruct();
            ResetLaserBeam();
        }
        
        private void ResetLaserBeam() {
            distanceTraveled = 0;
            rigidbody.velocity = Vector3.zero;
            yieldTime = config.onEnableYieldTime;
            lastPosition = Vector3.zero;
            gameObject.SetActive(false);
        }
    }
}