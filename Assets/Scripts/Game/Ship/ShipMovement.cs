using Game.Configs.Ship;
using UnityEngine;
using Utils.ScriptableObjects.Variables;

namespace Game.Ship {
    public class ShipMovement {
        private Rigidbody rb;
        private ShipConfig config;
        private ShipInputProcessor input;
        private FloatVariable speed, topSpeed, throttlePower;

        public ShipMovement(Rigidbody rb, ShipConfig config, ShipInputProcessor input, 
            FloatVariable speed, FloatVariable topSpeed, FloatVariable throttlePower) {
            this.rb = rb;
            this.config = config;
            this.input = input;
            this.topSpeed = topSpeed;
            this.speed = speed;
            this.throttlePower = throttlePower;

            this.topSpeed.SetValue(((config.throttlePower / rb.drag) - Time.fixedDeltaTime * config.throttlePower) / rb.mass, false);
            this.throttlePower.SetValue(config.throttlePower, false);
        }

        public void FixedUpdate(float fixedDeltaTime) {
            rb.AddRelativeForce(
                input.Strafe * config.strafeSpeed,
                0f,
                input.Throttle * throttlePower.ModifiedValue(),
                ForceMode.Force);
            rb.AddRelativeTorque(new Vector3(input.Yaw, input.Pitch, input.Roll) * config.angularSpeed);
            speed.SetValue(rb.velocity.magnitude);
        }
    }
}