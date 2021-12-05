using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utils.ScriptableObjects;

namespace UI.HUD {
    public class Speedometer : MonoBehaviour {
        [SerializeField] Image fillable;
        [SerializeField] FloatVariable shipSpeed, shipTopSpeed;
        [SerializeField] TMP_Text speedText;

        float previousSpeed;

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
