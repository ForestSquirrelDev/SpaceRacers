using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;
using Game.Configs;
using System;
using Utils.Maths;
using Utils.Vectors;
using Utils.ScriptableObjects;
using Utils.ScriptableObjects.Variables;
using static UnityEngine.InputSystem.InputAction;

namespace Game.Ship {
    public class ShipInputProcessor : IDisposable {
        public float Throttle { get; private set; }
        public float Strafe { get; private set; }
        public float Rotation { get; private set; }
        public float Roll { get; private set; }
        public float Pitch { get; private set; }
        public float Yaw { get; private set; }
        public bool NitroRequired { get; private set; }

        private Vector2 mouseInput;

        private FloatVariable shipThrottle;
        private ShipConfig config;
        private Transform transform;
        private Camera mainCamera;

        private Mouse mouse;
        private ShipInputActions shipInputActions;
        private InputAction strafeAction;
        private InputAction rotationAction;
        private InputAction nitroAction;

        public ShipInputProcessor(ShipConfig config, FloatVariable shipThrottle, Transform transform, Camera camera) {
            this.config = config;
            this.shipThrottle = shipThrottle;
            this.transform = transform;
            this.mainCamera = camera;

            mouse = Mouse.current;
            shipInputActions = new ShipInputActions();
            strafeAction = shipInputActions.Ship.StrafeAxis;
            rotationAction = shipInputActions.Ship.RotationAxis;
            nitroAction = shipInputActions.Ship.Nitro;

            strafeAction.Enable();
            rotationAction.Enable();
            nitroAction.Enable();

            nitroAction.performed += SetNitroRequired;
            shipThrottle.OnValueChanged += OnSliderThrottleChanged;
        }

        public void Update(float deltaTime) {
            UpdateKeyboardThrottle(deltaTime);
            UpdateInputAxes(deltaTime);
            FindMousePosition(out Vector3 worldPos);
            CalculateTurn(worldPos);
            CalculateRoll(deltaTime);
        }

        public void Dispose() {
            strafeAction.Disable();
            rotationAction.Disable();
            nitroAction.Disable();

            nitroAction.performed -= SetNitroRequired;
            shipThrottle.OnValueChanged -= OnSliderThrottleChanged;
        }

        private void UpdateInputAxes(float deltaTime) {
            float strafe = strafeAction.ReadValue<float>();
            float rotation = rotationAction.ReadValue<float>();
            Strafe = Mathf.MoveTowards(Strafe, strafe, deltaTime * config.strafeSensitivity);
            Rotation = Mathf.MoveTowards(Rotation, rotation, deltaTime * config.rorationSensitivity);
        }

        private void UpdateKeyboardThrottle(float deltaTime) {
            if (Input.GetKey(KeyCode.W)) {
                Throttle = Mathf.MoveTowards(Throttle, 1f, deltaTime * config.throttleSensitivity);
                shipThrottle.SetValue(Throttle, false);
            }
            if (Input.GetKey(KeyCode.S)) {
                Throttle = Mathf.MoveTowards(Throttle, 0f, deltaTime * config.throttleSensitivity);
                shipThrottle.SetValue(Throttle, false);
            }
        }

        private void FindMousePosition(out Vector3 gotoPos) {
            Vector2 mouseInput = mouse.position.ReadValue();
            Vector3 mousePos = new Vector3(mouseInput.x, mouseInput.y, 1000f);
            gotoPos = mainCamera.ScreenToWorldPoint(mousePos);

            var inputY = (mouseInput.y - (Screen.height * 0.5f)) / (Screen.height * 0.5f);
            var inputX = (mouseInput.x - (Screen.width * 0.5f)) / (Screen.width * 0.5f);

            this.mouseInput = new Vector2(inputX, inputY).ClampNeg1To1();
        }

        private void CalculateTurn(Vector3 gotoPos) {
            Vector3 localGotoPos = transform.InverseTransformVector(gotoPos - transform.position).normalized;
                
            Pitch = Mathf.Clamp(localGotoPos.x * config.pitchSensitivity, -config.pitchSensitivity, config.pitchSensitivity);
            Yaw = Mathf.Clamp(-localGotoPos.y * config.yawSensitivity, -config.yawSensitivity, config.yawSensitivity);
        }

        private void CalculateRoll(float deltaTime) {
            float inputRotation = Input.GetAxis("Roll");
            float rollInfluence = -mouseInput.x * Throttle;
            float yInfluence = MathfExtensions.InverseRelationship(3f, mouseInput.y * 10f);
            yInfluence = Mathf.Clamp(Mathf.Abs(yInfluence), float.MinValue, 1f);
            rollInfluence *= yInfluence;
            rollInfluence *= config.autoRollSensitivity;

            if (inputRotation != 0) Roll = inputRotation * config.customRollSensitivity;
            else Roll = Mathf.MoveTowards(Roll, rollInfluence, deltaTime * 1f);
        }

        private void OnSliderThrottleChanged(float value) {
            DOTween.To(() => Throttle, x => Throttle = x, value, config.throttleSensitivity);
        }

        private void SetNitroRequired(CallbackContext callback) {
            NitroRequired = !NitroRequired;
        }
    }
}