using Game.Configs;
using UnityEngine;
using Utils;

namespace Game.Ship {
    public class ShipController : MonoBehaviour {
        [Header("Configs")]
        [SerializeField] ShipConfig shipConfig;

        [Header("Referenceable variables")]
        [SerializeField] FloatVariable shipThrottle;
        [SerializeField] FloatVariable shipSpeed;
        [SerializeField] FloatVariable shipTopSpeed;

        private ShipInputProcessor input;
        private ShipMovement movement;

        private void Awake() {
            input = new ShipInputProcessor(shipConfig, shipThrottle, transform);
            movement = new ShipMovement(GetComponent<Rigidbody>(), shipConfig, input, shipSpeed, shipTopSpeed);
        }

        private void FixedUpdate() {
            movement.FixedUpdate(Time.fixedDeltaTime);
        }

        private void Update() {
            input.Update(Time.deltaTime);
        }

        private void OnDestroy() {
            input.Dispose();
        }
    }
}
