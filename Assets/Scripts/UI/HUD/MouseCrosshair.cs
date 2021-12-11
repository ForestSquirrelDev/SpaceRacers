using UnityEngine;
using UnityEngine.UI;

namespace UI.HUD {
    public class MouseCrosshair : MonoBehaviour {
        [SerializeField] private Image crosshair;

        private void Update() {
            crosshair.transform.position = Input.mousePosition;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Confined;
        }
    }
}
