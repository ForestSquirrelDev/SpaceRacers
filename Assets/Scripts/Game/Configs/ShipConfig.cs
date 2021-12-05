using UnityEngine;

namespace Game.Configs {
    [CreateAssetMenu(menuName = "Configs/Game/Ship Config")]
    public class ShipConfig : ScriptableObject {
        [Header("Movement parameters")]
        public float throttlePower = 5000f;
        public float strafeSpeed = 100f;
        public float angularSpeed = 100f;

        [Header("Input parameters")]
        public float pitchSensitivity = 2.5f;
        public float yawSensitivity = 2.5f;
        public float customRollSensitivity = 3f;
        public float autoRollSensitivity = 1f;
        public float throttleSensitivity = 0.5f;
        public float strafeSensitivity = 0.5f;
        public float rorationSensitivity = 0.5f;

        [Header("Nitro")]
        public float nitroPower = 1.5f;
        public float nitroSensitivity = 3f;
        public float nitroCapacity = 10f;
        public float nitroRechargeSpeed = 1f;

        [Header("PID Input")]
        public float rollKp = 1f;
        public float rollKi = 0;
        public float rollKd = 0.1f;
    }
}
