using Game.Configs;
using UnityEngine;
using Utils.ScriptableObjects;

namespace Game.Ship {
    public class ShipController : MonoBehaviour {
        [Header("Configs")]
        [SerializeField] ShipConfig shipConfig;

        [Header("Scriptable variables")]
        [SerializeField] FloatVariable shipThrottle;
        [SerializeField] FloatVariable shipSpeed;
        [SerializeField] FloatVariable shipTopSpeed;
        [SerializeField] FloatVariable shipThrottlePower;
        [SerializeField] FloatVariable shipNitroBank;
        [SerializeField] TransformVariable shipTransform;

        private ShipInputProcessor input;
        private ShipMovement movement;
        private NitroBooster nitroBooster;

        private void Awake() {
            input = new ShipInputProcessor(shipConfig, shipThrottle, transform);
            movement = new ShipMovement(GetComponent<Rigidbody>(), shipConfig, input, shipSpeed, shipTopSpeed, shipThrottlePower);
            nitroBooster = new NitroBooster(shipThrottlePower, shipConfig, shipNitroBank, input);
            shipTransform.SetValue(transform, true);
        }

        private void FixedUpdate() {
            movement.FixedUpdate(Time.fixedDeltaTime);
        }

        private void Update() {
            float dt = Time.deltaTime;
            input.Update(dt);
            nitroBooster.Update(dt);
        }

        private void OnDestroy() {
            input.Dispose();
        }
    }
}