using UnityEngine;

namespace Game.Configs {
    [CreateAssetMenu(menuName = "Configs/Game/Ship Config")]
    public class ShipConfig : ScriptableObject {
        [Header("Movement parameters")]
        public float throttleSpeed = 100f;
        public float strafeSpeed = 100f;
        public float angularSpeed = 100f;

        [Header("Input parameters")]
        public float pitchSensitivity = 2.5f;
        public float yawSensitivity = 2.5f;
        public float rollSensitivity = 1f;
        public float bankLimit = 35f;
    }
}
