using UnityEngine;
using UnityEngine.UI;
using Utils.ScriptableObjects;
using Utils.ScriptableObjects.Variables;

namespace UI.HUD {
    public class NitroBankDisplayer : MonoBehaviour {
        [SerializeField] private FloatVariable shipNitroBank;
        [SerializeField] private Image fillable;

        private float maxNitro;
        private float previousValue;

        private void Start() {
            maxNitro = shipNitroBank.ModifiedValue();
        }

        private void Update() {
            float current = shipNitroBank.ModifiedValue();
            if (previousValue != current) {
                previousValue = current;
                float normalizedValue = current / maxNitro;
                fillable.fillAmount = normalizedValue;
            }
        }
    }
}
