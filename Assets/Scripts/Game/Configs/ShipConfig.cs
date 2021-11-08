using UnityEngine;

namespace Game.Configs {
    [CreateAssetMenu(menuName = "Configs/Game/Ship Config")]
    public class ShipConfig : ScriptableObject {
        [Header("Movement parameters")]
        public float throttleSpeed = 100f;
        public float strafeSpeed = 100f;
        public float angularSpeed = 100f;

        [Header("Input parameters")]
        public TurnType turnType;
        public float pitchSensitivity = 2.5f;
        public float yawSensitivity = 2.5f;
        public float customRollSensitivity = 3f;
        public float autoRollSensitivity = 1f;
        public float bankLimit = 35f;

        [Header("PID Input")]
        public float rollKp = 1f;
        public float rollKi = 0;
        public float rollKd = 0.1f;

        public enum TurnType {
            FollowWorldSpaceMouse,
            FollowScreenSpaceMouse
        }
    }
}
