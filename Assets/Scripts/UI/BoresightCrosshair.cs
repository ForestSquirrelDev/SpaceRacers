using UnityEngine;
using UnityEngine.UI;
using Utils.ScriptableObjects;

namespace UI.HUD {
    public class BoresightCrosshair : MonoBehaviour {
        [SerializeField] Image crosshair;
        [SerializeField] TransformVariable shipTransform;
        [SerializeField] float boresightDistance = 2000f;

        Camera cam;

        private void Awake() {
            cam = Camera.main;
        }

        private void Update() {
            Vector3 boresightPos = (shipTransform.Value.forward * boresightDistance) + shipTransform.Value.position;
            Vector3 screenPos = cam.WorldToScreenPoint(boresightPos);
            screenPos.z = 0f;

            crosshair.transform.position = screenPos;
        }
    }
}
