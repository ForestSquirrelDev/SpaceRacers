using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Ship.PlayerInput {
    public class ShipShootingInputProcessor : IDisposable {
        public event Action FireRequired;
        private InputAction fireAction;

        public ShipShootingInputProcessor() {
            var shipInputActions = new ShipInputActions();
            fireAction = shipInputActions.Ship.Fire;
            fireAction.Enable();
            fireAction.performed += OnFireActionPerformed;
        }
        
        public void Dispose() {
            fireAction.performed -= OnFireActionPerformed;
            fireAction.Disable();
            fireAction.Dispose();
        }

        private void OnFireActionPerformed(InputAction.CallbackContext callbackContext) {
            FireRequired?.Invoke();
        }
    }
}