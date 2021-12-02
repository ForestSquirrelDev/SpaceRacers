using Game.Configs;
using UnityEngine;
using Utils.ScriptableObjects;

namespace Game.Ship {
    public class ShipController : MonoBehaviour {
        [Header("Configs")]
        [SerializeField] ShipConfig shipConfig;

        [Header("Referenceable variables")]
        [SerializeField] FloatVariable shipThrottle;
        [SerializeField] FloatVariable shipSpeed;
        [SerializeField] FloatVariable shipTopSpeed;
        [SerializeField] TransformVariable shipTransform;

        private ShipInputProcessor input;
        private ShipMovement movement;

        private void Awake() {
            input = new ShipInputProcessor(shipConfig, shipThrottle, transform);
            movement = new ShipMovement(GetComponent<Rigidbody>(), shipConfig, input, shipSpeed, shipTopSpeed);
            shipTransform.SetValue(transform, true);
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
