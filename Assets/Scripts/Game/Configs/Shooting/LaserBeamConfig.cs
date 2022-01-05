using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Configs.Shooting {
    [CreateAssetMenu(menuName = "Configs/Game/Shooting/Laser Beam Config")]
    public class LaserBeamConfig : ScriptableObject {
        public float projectileInitialSpeed = 50f; 
        public float constantAcceleration = 50f;
        public float accelerationRaiseOverTime = 1;
        public float rotationSpeed = 5f;
        public float disablingDistance = 1000f;
        public float onEnableYieldTime = 0.5f;
    }
}