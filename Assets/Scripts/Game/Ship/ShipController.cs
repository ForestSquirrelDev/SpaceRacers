using Game.Configs;
using UnityEngine;

namespace Game.Ship {
    public class ShipController : MonoBehaviour {
        [Header("Configs")]
        [SerializeField] private ShipConfig config;
        
        private ShipInputProcessor input;
        private ShipMovement movement;
        private NitroBooster nitroBooster;
        
        private void Awake() {
            Rigidbody rb = GetComponent<Rigidbody>();
            Transform t = transform;
            
            input = new ShipInputProcessor(config, config.shipInputThrottle, t, Camera.main);
            movement = new ShipMovement(rb, config, input, config.shipSpeed,
                config.shipTopSpeed, config.shipThrottlePower);
            nitroBooster = new NitroBooster(config.shipThrottlePower, config, config.shipNitroBank, input);
            config.shipTransform.SetValue(t, true);
            config.shipRigidbody.SetValue(rb);
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