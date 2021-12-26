using UnityEngine;
using UnityEngine.Serialization;
using Vector3 = UnityEngine.Vector3;

namespace Game.Configs.Environment {
    [CreateAssetMenu(menuName = "Configs/Game/Environment/Cube Config")]
    public class CubeConfig : ScriptableObject {
        public GameObject cubePrefab;
        [FormerlySerializedAs("onDestroyCubesCount")]
        public Vector2Int onDestroyCubesCountRange = new Vector2Int(5, 15);
        [FormerlySerializedAs("childCubeSize")]
        public Vector2 childCubeScaleMultiplierRange = new Vector2(0.1f, 0.3f);
        public Vector2Int explosionForceRange = new Vector2Int(50, 100);
        public Vector3 appliedTorque = Vector3.one * 10;
        
        public AnimationCurve fadeAnimationCurve;
    }
}