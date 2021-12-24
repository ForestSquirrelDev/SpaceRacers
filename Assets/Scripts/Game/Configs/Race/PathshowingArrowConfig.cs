using UnityEngine;

namespace Game.Configs.Race {
    [CreateAssetMenu(menuName = "Configs/Game/Race/Pathshowing Arrow Config")]
    public class PathshowingArrowConfig : ScriptableObject {
        public float tweeningScaleMultiplyer = 0.8f;
        public float tweenDuration = 0.5f;
        public float rotationSpeed = 5f;
    }
}