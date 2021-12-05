using UnityEngine;
using UnityEngine.UI;
using Utils.ScriptableObjects;

namespace UI.HUD {
    public class NitroBankDisplayer : MonoBehaviour {
        [SerializeField] FloatVariable shipNitroBank;
        [SerializeField] Image fillable;

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
