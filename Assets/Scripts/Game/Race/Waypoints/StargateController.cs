using System;
using Game.Ship;
using UnityEngine;

namespace Game.Race {
    public class StargateController : MonoBehaviour, IWaypoint {
        public event Action<StargateController> ShipEntered;
        
        [SerializeField] private StargatesRuntimeSet allStargates;

        private Collider otherCollider;

        private void OnTriggerEnter(Collider other) {
            otherCollider = other;
            OnWaypointPassed();
        }

        public void OnWaypointPassed() {
            if (!allStargates.Items.Contains(this)) return;
            ShipController ship = otherCollider.GetComponentInParent<ShipController>();
            if (ship == null) return;
            ShipEntered?.Invoke(this);
            allStargates.RemoveItem(this, true);
        }
    }
}