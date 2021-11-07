using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace UI
{
    public class ThrottleSlider : MonoBehaviour {
        [SerializeField] private Slider slider;
        [SerializeField] private FloatVariable shipThrottle;

        private bool invokeCallback = true;

        private void OnEnable() {
            slider.onValueChanged.AddListener((call) => shipThrottle.SetValue(call, invokeCallback));
        }

        private void Update() {
            if (slider.value != shipThrottle.Value) {
                invokeCallback = false;
                slider.value = shipThrottle.Value;
                invokeCallback = true;
            } 
        }

        private void OnDisable() {
            slider.onValueChanged.RemoveAllListeners();
        }
    }
}
