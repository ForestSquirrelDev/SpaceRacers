using Game.Configs;
using TMPro;
using UnityEngine;
using Utils;

namespace Game.Ship {
    public class ShipController : MonoBehaviour {
        [SerializeField] private ShipConfig shipConfig;
        [SerializeField] private FloatVariable shipThrottle;
        [SerializeField] private TMP_Text debugText;

        private ShipInputProcessor input;
        private ShipMovement movement;

        private void Awake() {
            input = new ShipInputProcessor(shipConfig, shipThrottle, transform, debugText);
            movement = new ShipMovement(GetComponent<Rigidbody>(), shipConfig, input);
        }

        private void FixedUpdate() {
            movement.FixedUpdate(Time.fixedDeltaTime);
        }

        private void Update() {
            input.Update(Time.deltaTime);
        }

        private void OnDestroy() {
            input.Dispose();
        }
    }
}
