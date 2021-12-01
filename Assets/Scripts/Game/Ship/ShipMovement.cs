using Game.Configs;
using UnityEngine;
using Utils;

namespace Game.Ship {
    public class ShipMovement {
        private Rigidbody rb;
        private ShipConfig config;
        private ShipInputProcessor input;
        private FloatVariable speed, topSpeed;

        public ShipMovement(Rigidbody rb, ShipConfig config, ShipInputProcessor input, FloatVariable speed, FloatVariable topSpeed) {
            this.rb = rb;
            this.config = config;
            this.input = input;
            this.topSpeed = topSpeed;
            this.speed = speed;

            this.topSpeed.SetValue(((config.throttleSpeed / rb.drag) - Time.fixedDeltaTime * config.throttleSpeed) / rb.mass, false);
        }

        public void FixedUpdate(float fixedDeltaTime) {
            rb.AddRelativeForce(
                x: input.Strafe * config.strafeSpeed,
                y: 0f,
                z: input.Throttle * config.throttleSpeed,
                mode: ForceMode.Force);
            rb.AddRelativeTorque(new Vector3(input.Yaw, input.Pitch, input.Roll) * config.angularSpeed);
            speed.SetValue(rb.velocity.magnitude, false);
        }
    }
}