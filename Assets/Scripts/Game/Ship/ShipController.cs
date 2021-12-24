using Game.Configs.Ship;
using Game.Shooting;
using UnityEngine;

namespace Game.Ship {
    public class ShipController : MonoBehaviour {
        [SerializeField] private ShipConfig config;

        private ShipInputProcessor input;
        private ShipMovement movement;
        private NitroBooster nitroBooster;
        private TargetingSystem targetingSystem;
        
        private void Awake() {
            Rigidbody rb = GetComponent<Rigidbody>();
            Transform t = transform;
            Camera cam = Camera.main;

            input = new ShipInputProcessor(config, config.shipInputThrottle, t, cam);
            movement = new ShipMovement(rb, config, input, config.shipSpeed,
                config.shipTopSpeed, config.shipThrottlePower);
            nitroBooster = new NitroBooster(config.shipThrottlePower, config, config.shipNitroBank, input);
            targetingSystem = new TargetingSystem(cam, t, input, config.targetableVariable);
            config.shipTransform.SetValue(t, true);
            config.shipRigidbody.SetValue(rb);
        }

        private void FixedUpdate() {
            movement.FixedUpdate(Time.fixedDeltaTime);
            targetingSystem.FixedUpdate();
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