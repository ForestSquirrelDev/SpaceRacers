using System.Diagnostics.CodeAnalysis;
using Game.Ship;
using JetBrains.Annotations;
using UnityEngine;

namespace Game.Shooting {
    [SuppressMessage("ReSharper", "UseNullPropagation")]
    public class TargetingSystem {
        [CanBeNull] private ITargetable CurrentTarget;
        private Camera camera;
        private Transform transform;
        private ShipInputProcessor input;
        private TargetableVariable targetableVariable;

        private const float threshold = 0.99f;

        public TargetingSystem(Camera mainCamera, Transform transform, 
            ShipInputProcessor input, TargetableVariable targetableVariable) {
            this.camera = mainCamera;
            this.transform = transform;
            this.input = input;
            this.targetableVariable = targetableVariable;
        }
        
        public void FixedUpdate() {
            Ray ray = camera.ScreenPointToRay(input.MouseInputRaw);
            if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue)) {
                ITargetable targetable = hit.collider.gameObject.GetComponent<ITargetable>();
                if (targetable == null) return;
                if (CurrentTarget != targetable) {
                    CurrentTarget = targetable;
                    targetableVariable.SetValue(targetable);
                }
            }
            LockTarget(ray);
        }

        private void LockTarget(Ray ray) {
            if (CurrentTarget == null) return;
            float dot = CurrentTarget.DotProduct(transform.position, ray.direction);
            if (dot < threshold) {
                CurrentTarget = null;
                targetableVariable.SetValue(null);
            }
        }
    }
}