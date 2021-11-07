using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Utils.Debugging {
    public class SimpleDebugger : MonoBehaviour {
        [SerializeField] bool performing = true;
        [SerializeField] private float updateTime = 0.5f;

        private void Awake() {
            StartCoroutine(DebugRoutine());
        }

        IEnumerator DebugRoutine() {
            while (true) {
                while (performing) {
                    Debug.Log($"old mouse: {Input.mousePosition}, new mouse : {Mouse.current.position.ReadValue()}");
                    yield return new WaitForSeconds(updateTime);
                }
                yield return null;
            }
        }
    }
}
