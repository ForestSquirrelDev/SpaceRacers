using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utils.ScriptableObjects;

namespace UI {
    public class Speedometer : MonoBehaviour {
        [SerializeField] Image leftFillable, rightFillable;
        [SerializeField] FloatVariable shipSpeed, shipTopSpeed;
        [SerializeField] TMP_Text speedText;

        float previousSpeed;

        private void Update() {
            float normalizedSpeed = shipSpeed.Value / shipTopSpeed.Value;
            bool fillAmountUpdateRequired = (leftFillable.fillAmount != normalizedSpeed)
                || (rightFillable.fillAmount != normalizedSpeed);
            bool speedUpdateRequired = previousSpeed != shipSpeed.Value;

            if (fillAmountUpdateRequired) {
                leftFillable.fillAmount = normalizedSpeed;
                rightFillable.fillAmount = normalizedSpeed;
            }
            if (speedUpdateRequired) {
                previousSpeed = shipSpeed.Value;
                speedText.text = shipSpeed.Value.ToString(format: "0");
            }
        }
    }
}
