using Game.ScriptableVariables;
using TMPro;
using UnityEngine;

namespace UI.HUD {
    public class DistanceDisplayer : MonoBehaviour {
        [SerializeField] private DistanceToTargetVariable currentTargetDistance;

        private const string distance_units = "m";
        private const string default_string = "-";
        private TMP_Text distanceText;
        
        private void Awake() {
            distanceText = GetComponent<TMP_Text>();
        }

        private void Update() {
            if (!currentTargetDistance.UpdatingDistance) {
                if (distanceText.text != default_string)
                    distanceText.text = default_string;
                return;
            }
            distanceText.text = $"{currentTargetDistance.ModifiedValue().ToString("0")}{distance_units}";
        }
    }
}