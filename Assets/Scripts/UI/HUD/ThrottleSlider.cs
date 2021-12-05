using UnityEngine;
using UnityEngine.UI;
using Utils.ScriptableObjects;

namespace UI.HUD {
    public class ThrottleSlider : MonoBehaviour {
        [SerializeField] Slider slider;
        [SerializeField] FloatVariable shipThrottle;

        private bool invokeCallback = true;

        private void OnEnable() {
            slider.onValueChanged.AddListener((call) => shipThrottle.SetValue(call, invokeCallback));
        }

        private void Update() {
            if (slider.value != shipThrottle.ModifiedValue()) {
                invokeCallback = false;
                slider.value = shipThrottle.ModifiedValue();
                invokeCallback = true;
            }
        }

        private void OnDisable() {
            slider.onValueChanged.RemoveAllListeners();
        }
    }
}
