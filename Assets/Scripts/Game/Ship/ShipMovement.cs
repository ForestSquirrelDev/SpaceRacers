using Game.Configs;
using UnityEngine;

namespace Game.Ship {
    public class ShipMovement {
        private Rigidbody rb;
        private ShipConfig config;
        private ShipInputProcessor input;

        public ShipMovement(Rigidbody rb, ShipConfig config, ShipInputProcessor input) {
            this.rb = rb;
            this.config = config;
            this.input = input;
        }

        public void FixedUpdate(float fixedDeltaTime) {
            rb.AddRelativeForce(
                x: input.Strafe * config.strafeSpeed,
                y: 0f,
                z: input.Throttle * config.throttleSpeed,
                mode: ForceMode.Force);
            rb.AddRelativeTorque(new Vector3(input.Pitch, input.Yaw, input.Roll) * config.angularSpeed);
        }
    }
}