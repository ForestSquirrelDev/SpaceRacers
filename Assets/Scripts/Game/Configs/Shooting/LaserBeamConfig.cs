using UnityEngine;

namespace Game.Configs.Shooting {
    [CreateAssetMenu(menuName = "Configs/Game/Shooting/Laser Beam Config")]
    public class LaserBeamConfig : ScriptableObject {
        public float accelerationSpeed = 50f;
        public float rotationSpeed = 5f;
        public float disablingDistance = 1000f;
        public float onEnableYieldTime = 0.5f;
    }
}