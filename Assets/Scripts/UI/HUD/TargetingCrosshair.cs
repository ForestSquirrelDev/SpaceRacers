using Game.Shooting;
using UnityEngine;

namespace UI.HUD {
    public class TargetingCrosshair : MonoBehaviour {
        [SerializeField] private RectTransform crosshair;
        [SerializeField] private TargetableVariable targetable;

        private Camera cam;

        private void Awake() {
            cam = Camera.main;  
        }

        private void Update() {
            if (targetable.BaseValue != null) {
                if (crosshair.gameObject.activeSelf == false)
                    crosshair.gameObject.SetActive(true);
                Vector3 newPos = cam.WorldToScreenPoint(targetable.BaseValue.Position());
                newPos.z = 0;
                crosshair.transform.position = newPos;
            }
            else {
                if (crosshair.gameObject.activeSelf)
                    crosshair.gameObject.SetActive(false);
            }
        }
    }
}