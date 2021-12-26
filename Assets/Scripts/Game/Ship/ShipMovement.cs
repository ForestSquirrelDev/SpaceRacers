using Game.Configs.Ship;
using Game.Ship.PlayerInput;
using UnityEngine;
using Utils.ScriptableObjects.Variables;

namespace Game.Ship {
    public class ShipMovement {
        private Rigidbody rb;
        private ShipConfig config;
        private ShipMovementInputProcessor movementInput;
        private FloatVariable speed, topSpeed, throttlePower;

        public ShipMovement(Rigidbody rb, ShipConfig config, ShipMovementInputProcessor movementInput, 
            FloatVariable speed, FloatVariable topSpeed, FloatVariable throttlePower) {
            this.rb = rb;
            this.config = config;
            this.movementInput = movementInput;
            this.topSpeed = topSpeed;
            this.speed = speed;
            this.throttlePower = throttlePower;

            this.topSpeed.SetValue(((config.throttlePower / rb.drag) - Time.fixedDeltaTime * config.throttlePower) / rb.mass, false);
            this.throttlePower.SetValue(config.throttlePower, false);
        }

        public void FixedUpdate() {
            rb.AddRelativeForce(
                movementInput.Strafe * config.strafeSpeed,
                0f,
                movementInput.Throttle * throttlePower.ModifiedValue(),
                ForceMode.Force);
            rb.AddRelativeTorque(new Vector3(movementInput.Yaw, 
                movementInput.Pitch, 
                movementInput.Roll) * config.angularSpeed);
            speed.SetValue(rb.velocity.magnitude);
        }
    }
}