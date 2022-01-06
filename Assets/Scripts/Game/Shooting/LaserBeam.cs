using Game.Configs.Shooting;
using UnityEngine;
using Utils.Maths;

namespace Game.Shooting {
    [RequireComponent(typeof(Rigidbody))]
    public class LaserBeam : MonoBehaviour {
        public Rigidbody Rigidbody => rigidbody;
        
        private new Rigidbody rigidbody;
        private TrailRenderer[] trailRenderers;
        private ITargetable target;
        private LaserBeamConfig config;
        private Transform thisTransform;
        private Vector3 lastPosition;

        private float distanceTraveled;
        private float constantAcceleration;
        private float yieldTime;

        public void Init(ITargetable target, LaserBeamConfig config) {
            yieldTime = config.onEnableYieldTime;
            this.target = target;
            this.config = config;
            thisTransform = transform;
            constantAcceleration = config.constantAcceleration;
            this.rigidbody ??= GetComponent<Rigidbody>();
            this.trailRenderers ??= GetComponentsInChildren<TrailRenderer>();
        }

        private void FixedUpdate() {
            float dt = Time.fixedDeltaTime;
            yieldTime -= dt;
            if (yieldTime > 0) return;
            CountTraveledDistance();
            Accelerate(dt);
            if (target == null) return;
            Transform targetTransform = target.GetTransform();
            bool cantFollowTarget = targetTransform == null 
                                    || !targetTransform.gameObject.activeSelf 
                                    || PassedTarget();
            if (cantFollowTarget) return;
            transform.rotation = QuaternionExtensions.SmoothRotateTowardsTarget(
                transform, targetTransform, config.rotationSpeed, dt);
        }
        
        private void OnTriggerEnter(Collider other) {
            if (other.GetComponent<LaserBeam>() != null) return;
            if (other.TryGetComponent(out IDestructible destructible))
                destructible.Destruct();
            ResetLaserBeam();
        }
        
        private void CountTraveledDistance() {
            distanceTraveled += Vector3.Distance(thisTransform.position, lastPosition);
            lastPosition = thisTransform.position;
            if (distanceTraveled >= config.disablingDistance) 
                ResetLaserBeam();
        }

        private void Accelerate(float dt) {
            rigidbody.AddForce(thisTransform.forward * constantAcceleration);
            constantAcceleration += config.accelerationRaiseOverTime * dt;
        }
        
        private void ResetLaserBeam() {
            distanceTraveled = 0;
            rigidbody.velocity = Vector3.zero;
            yieldTime = config.onEnableYieldTime;
            lastPosition = Vector3.zero;
            foreach (var trailRenderer in trailRenderers) {
                trailRenderer.Clear();
            }
            gameObject.SetActive(false);
        }

        private bool PassedTarget() {
            float dot = target.ProjectVectorOnOffset(transform.position, transform.forward);
            return dot < -0.5f;
        }
    }
}