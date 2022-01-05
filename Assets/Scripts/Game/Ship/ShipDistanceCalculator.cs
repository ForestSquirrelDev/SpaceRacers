using Game.ScriptableVariables;
using Game.Shooting;
using UnityEngine;
using Utils.ScriptableObjects.Variables;
// ReSharper disable PossibleNullReferenceException

namespace Game.Ship {
    public class ShipDistanceCalculator {
        private FloatVariable currentWaypointDistance;
        private DistanceToTargetVariable currentTargetDistance;
        private TargetableVariable currentTarget;
        private TransformVariable shipTransform;
        private TransformVariable currentWaypointTransform;
        
        public ShipDistanceCalculator(DistanceToTargetVariable currentTargetDistance, FloatVariable currentWaypointDistance,
            TransformVariable shipTransform, TransformVariable currentWaypointTransform, TargetableVariable currentTarget) {
            this.currentTargetDistance = currentTargetDistance;
            this.currentWaypointDistance = currentWaypointDistance;
            this.shipTransform = shipTransform;
            this.currentWaypointTransform = currentWaypointTransform;
            this.currentTarget = currentTarget;
        }

        public void Update() {
            if (shipTransform.BaseValue == null) return;
            SetCurrentTargetDistance();
            SetCurrentWaypointDistance();
        }

        private void SetCurrentTargetDistance() {
            if (currentTarget.BaseValue == null
                || !currentTarget.BaseValue.GetTransform().gameObject.activeSelf) {
                currentTargetDistance.UpdatingDistance = false;
                return;
            }
            currentTargetDistance.UpdatingDistance = true;
            currentTargetDistance.SetValue(Vector3.Distance(
                shipTransform.BaseValue.position, currentTarget.BaseValue.GetTransform().position));
        }

        private void SetCurrentWaypointDistance() {
            if (currentWaypointTransform.BaseValue == null) return;
            currentWaypointDistance.SetValue(Vector3.Distance(
                shipTransform.BaseValue.position, currentWaypointTransform.BaseValue.position));
        }
    }
}