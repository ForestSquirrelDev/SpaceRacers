using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utils.ScriptableObjects;
using Utils.ScriptableObjects.Variables;

namespace UI.HUD {
    public class Speedometer : MonoBehaviour {
        [SerializeField] private Image fillable;
        [SerializeField] private FloatVariable shipSpeed, shipTopSpeed;
        [SerializeField] private TMP_Text speedText;

        private float previousSpeed;

        private void Update() {
            float normalizedSpeed = shipSpeed.ModifiedValue() / shipTopSpeed.ModifiedValue();
            bool fillAmountUpdateRequired = fillable.fillAmount != normalizedSpeed;
            bool speedUpdateRequired = previousSpeed != shipSpeed.ModifiedValue();

            if (fillAmountUpdateRequired) {
                fillable.fillAmount = normalizedSpeed;
            }
            if (speedUpdateRequired) {
                previousSpeed = shipSpeed.ModifiedValue();
                speedText.text = shipSpeed.ModifiedValue().ToString(format: "0");
            }
        }
    }
}
