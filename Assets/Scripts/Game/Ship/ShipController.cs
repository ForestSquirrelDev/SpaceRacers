using Game.Configs;
using UnityEngine;
using Utils.ScriptableObjects.Variables;

namespace Game.Ship {
    public class ShipController : MonoBehaviour {
        [Header("Configs")]
        [SerializeField] private ShipConfig shipConfig;

        [Header("Scriptable variables")]
        [SerializeField] private FloatVariable shipThrottle;
        [SerializeField] private FloatVariable shipSpeed;
        [SerializeField] private FloatVariable shipTopSpeed;
        [SerializeField] private FloatVariable shipThrottlePower;
        [SerializeField] private FloatVariable shipNitroBank;
        [SerializeField] private RigidbodyVariable shipRigidbody;
        [SerializeField] private TransformVariable shipTransform;

        private ShipInputProcessor input;
        private ShipMovement movement;
        private NitroBooster nitroBooster;

        private void Awake() {
            Rigidbody rb = GetComponent<Rigidbody>();
            Transform t = transform;
            
            input = new ShipInputProcessor(shipConfig, shipThrottle, t, Camera.main);
            movement = new ShipMovement(rb, shipConfig, input, shipSpeed, shipTopSpeed, shipThrottlePower);
            nitroBooster = new NitroBooster(shipThrottlePower, shipConfig, shipNitroBank, input);
            shipTransform.SetValue(t, true);
            shipRigidbody.SetValue(rb);
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