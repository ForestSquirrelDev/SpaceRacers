using Game.Configs.Ship;
using Game.Ship.PlayerInput;
using Game.Shooting;
using UnityEngine;

namespace Game.Ship {
    public class ShipController : MonoBehaviour {
        [SerializeField] private ShipConfig config;
        [SerializeField] private Transform gunOne, gunTwo;

        private ShipMovementInputProcessor movementInput;
        private ShipShootingInputProcessor shootingInput;
        private ShipMovement movement;
        private NitroBooster nitroBooster;
        private TargetingSystem targetingSystem;
        private LaserCannon laserCannon;
        private ShipDistanceCalculator distanceCalculator;
        
        private void Awake() {
            Rigidbody rb = GetComponent<Rigidbody>();
            Transform t = transform;
            Camera cam = Camera.main;

            movementInput = new ShipMovementInputProcessor(config, config.shipInputThrottle, t, cam);
            shootingInput = new ShipShootingInputProcessor();
            movement = new ShipMovement(rb, config, movementInput, config.shipSpeed,
                config.shipTopSpeed, config.shipThrottlePower);
            nitroBooster = new NitroBooster(config.shipThrottlePower, config, config.shipNitroBank, movementInput);
            targetingSystem = new TargetingSystem(cam, t, movementInput, config.currentTargetVariable);
            laserCannon = new LaserCannon((config.gunOne, config.gunTwo),
                shootingInput, config.laserBeamConfig, config.currentTargetVariable,
                config.shipSpeed, config, config.lasersPool);
            distanceCalculator = new ShipDistanceCalculator(config.distanceToCurrentTarget, config.distanceToCurrentWaypoint,
                config.shipTransform, config.currentWaypointTransform, config.currentTargetVariable);
            
            config.shipTransform.SetValue(t, true);
            config.shipRigidbody.SetValue(rb);
            config.gunOne.SetValue(gunOne);
            config.gunTwo.SetValue(gunTwo);
        }

        private void FixedUpdate() {
            movement.FixedUpdate();
            targetingSystem.FixedUpdate();
        }

        private void Update() {
            float dt = Time.deltaTime;
            movementInput.Update(dt);
            nitroBooster.Update(dt);
            distanceCalculator.Update();
        }

        private void OnDestroy() {
            movementInput.Dispose();
            shootingInput.Dispose();
            laserCannon.Dispose();
        }
    }
}