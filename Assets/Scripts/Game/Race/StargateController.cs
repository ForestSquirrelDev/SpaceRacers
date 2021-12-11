using System;
using Game.Ship;
using UnityEngine;

namespace Game.Race {
    public class StargateController : MonoBehaviour {
        public event Action<StargateController> ShipEntered;
        
        [SerializeField] private StargatesRuntimeSet allStargates;

        private void OnEnable() {
            allStargates.AddItem(this);
        }

        private void OnTriggerEnter(Collider other) {
            if (!allStargates.Items.Contains(this)) return;
            if (other.TryGetComponent(out ShipController ship)) {
                ShipEntered?.Invoke(this);
                allStargates.RemoveItem(this);
            }
        }

        private void OnDisable() {
            allStargates.RemoveItem(this);
        }
    }
}