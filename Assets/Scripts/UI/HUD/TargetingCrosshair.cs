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
                if (targetable.BaseValue.CantBeTargeted) {
                    DisableCrosshair();
                    return;
                }
                if (crosshair.gameObject.activeSelf == false)
                    crosshair.gameObject.SetActive(true);
                Vector3 newPos = cam.WorldToScreenPoint(targetable.BaseValue.GetTransform().position);
                newPos.z = 0;
                crosshair.transform.position = newPos;
            }
            else {
                DisableCrosshair();
            }
        }
        private void DisableCrosshair() {
            if (crosshair.gameObject.activeSelf)
                crosshair.gameObject.SetActive(false);
        }
    }
}