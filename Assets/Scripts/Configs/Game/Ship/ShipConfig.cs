using UnityEngine;
using UnityEngine.Serialization;
using Utils.ScriptableObjects.Variables;

namespace Configs.Game.Ship {
    [CreateAssetMenu(menuName = "Configs/Game/Ship/Ship Config")]
    public class ShipConfig : ScriptableObject {
         [Header("Scriptable variables")] 
        [FormerlySerializedAs("shipThrottle")] public FloatVariable shipInputThrottle;
        public FloatVariable shipSpeed;
        public FloatVariable shipTopSpeed;
        public FloatVariable shipThrottlePower;
        public FloatVariable shipNitroBank;
        public RigidbodyVariable shipRigidbody;
        public TransformVariable shipTransform;

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

        [Header("PID input")]
        public float pitchYawKp = 0.2f;
        public float pitchYawKi = 0.2f;
        public float pitchYawKd = 0.2f;
        [Space(5f)]
        public float rollKp = 1f;
        public float rollKi = 0.2f;
        public float rollKd = 0.2f;
    }
}
