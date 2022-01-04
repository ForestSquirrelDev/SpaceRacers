using TMPro;
using UnityEngine;
using Utils.ScriptableObjects.Variables;

namespace UI.HUD {
    public class DistanceDisplayer : MonoBehaviour {
        [SerializeField] private FloatVariable currentTargetDistance;

        private const string distance_units = "m";
        private TMP_Text distanceText;
        
        private void Awake() {
            distanceText = GetComponent<TMP_Text>();
        }

        private void Update() {
            distanceText.text = $"{currentTargetDistance.ModifiedValue().ToString("0")}{distance_units}";
        }
    }
}