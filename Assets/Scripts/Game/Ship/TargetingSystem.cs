using System.Diagnostics.CodeAnalysis;
using Game.Ship.PlayerInput;
using JetBrains.Annotations;
using UnityEngine;

namespace Game.Shooting {
    [SuppressMessage("ReSharper", "UseNullPropagation")]
    public class TargetingSystem {
        [CanBeNull] private ITargetable CurrentTarget;
        private Camera camera;
        private Transform transform;
        private ShipMovementInputProcessor movementInput;
        private TargetableVariable targetableVariable;

        private const float threshold = 0.98f;

        public TargetingSystem(Camera mainCamera, Transform transform, 
            ShipMovementInputProcessor movementInput, TargetableVariable targetableVariable) {
            this.camera = mainCamera;
            this.transform = transform;
            this.movementInput = movementInput;
            this.targetableVariable = targetableVariable;
        }
        
        public void FixedUpdate() {
            Ray ray = camera.ScreenPointToRay(movementInput.MouseInputRaw);
            if (Physics.Raycast(ray, out RaycastHit hit, 5000f)) {
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
            float dot = CurrentTarget.ProjectVectorOnOffset(transform.position, ray.direction);
            if (dot < threshold) {
                CurrentTarget = null;
                targetableVariable.SetValue(null);
            }
        }
    }
}