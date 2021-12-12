using System;
using Game.Ship;
using UnityEngine;

namespace Game.Race {
    public class StargateController : MonoBehaviour {
        public event Action<StargateController> ShipEntered;
        
        [SerializeField] private StargatesRuntimeSet allStargates;

        private void OnEnable() {
            allStargates.AddItem(this, true);
        }

        private void OnTriggerEnter(Collider other) {
            if (!allStargates.Items.Contains(this)) return;
            ShipController ship = other.GetComponentInParent<ShipController>();
            if (ship == null) return;
            Debug.Log("Ship entered");
            ShipEntered?.Invoke(this);
            allStargates.RemoveItem(this, true);
        }

        private void OnDisable() {
            allStargates.RemoveItem(this);
        }
    }
}