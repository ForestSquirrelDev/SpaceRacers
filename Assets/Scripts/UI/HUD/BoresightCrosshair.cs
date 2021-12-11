using UnityEngine;
using UnityEngine.UI;
using Utils.ScriptableObjects;
using Utils.ScriptableObjects.Variables;

namespace UI.HUD {
    public class BoresightCrosshair : MonoBehaviour {
        [SerializeField] private Image crosshair;
        [SerializeField] private TransformVariable shipTransform;
        [SerializeField] private float boresightDistance = 2000f;

        private Camera cam;

        private void Awake() {
            cam = Camera.main;
        }

        private void Update() {
            Vector3 boresightPos = (shipTransform.ModifiedValue().forward * boresightDistance) + shipTransform.ModifiedValue().position;
            Vector3 screenPos = cam.WorldToScreenPoint(boresightPos);
            screenPos.z = 0f;

            crosshair.transform.position = screenPos;
        }
    }
}
