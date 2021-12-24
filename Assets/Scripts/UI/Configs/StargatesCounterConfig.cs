using UnityEngine;

namespace UI.Configs {
    [CreateAssetMenu(menuName = "Configs/UI/Stargates Counter Config")]
    public class StargatesCounterConfig : ScriptableObject {
        public float dividerSpacingX = 0.1f;
        public float dividerSpacingY = 0.1f;
        public int dividerSizePercents = 50;
        public float dividerheightOffset = 0.1f;
    }
}