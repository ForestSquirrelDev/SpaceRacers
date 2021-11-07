using SpaceRacers.Game.Ship;
using UnityEngine;
using UnityEngine.InputSystem;
using Utils;
using DG.Tweening;
using Game.Configs;
using System;

namespace Game.Ship {
    public class ShipInputProcessor : IDisposable {
        public float Throttle { get; private set; }
        public float Strafe { get; private set; }
        public float Rotation { get; private set; }
        public float Roll { get; private set; }
        public float Pitch { get; private set; }
        public float Yaw { get; private set; }

        private FloatVariable shipThrottle;
        private ShipConfig config;
        private Transform transform;

        private Mouse mouse;
        private ShipInputActions shipInputActions;
        private InputAction strafeAction;
        private InputAction rotationAction;

        private const float throttle_speed = 0.5f;
        private const float strafe_speed = 0.5f;
        private const float rotation_speed = 0.5f;

        public ShipInputProcessor(ShipConfig config, FloatVariable shipThrottle, Transform transform) {
            this.config = config;
            this.shipThrottle = shipThrottle;
            this.transform = transform;

            mouse = Mouse.current;
            shipInputActions = new ShipInputActions();
            strafeAction = shipInputActions.Ship.StrafeAxis;
            rotationAction = shipInputActions.Ship.RotationAxis;

            strafeAction.Enable();
            rotationAction.Enable();

            shipThrottle.OnValueChanged += OnSliderThrottleChanged;
        }

        public void Update(float deltaTime) {
            UpdateKeyboardThrottle(deltaTime);
            UpdateInputAxes(deltaTime);
            Vector2 mouseInput = mouse.position.ReadValue();
            Vector3 mousePos = new Vector3(mouseInput.x, mouseInput.y, 1000f);
            Vector3 gotoPos = Camera.main.ScreenToWorldPoint(mousePos);
            CalculateTurn(gotoPos);
            CalculateRoll(mousePos, Camera.main.transform.up);
            //Debug.Log($"Throttle: {Throttle}, strafe: {Strafe}, rotation: {Rotation}, mousePosition: {mouseWorldPosition}" +
            //    $" Roll: {Roll}, Pitch: {Pitch}, Yaw: {Yaw}");
        }

        public void Dispose() {
            strafeAction.Disable();
            rotationAction.Disable();
            shipThrottle.OnValueChanged -= OnSliderThrottleChanged;
        }

        private void UpdateInputAxes(float deltaTime) {
            float strafe = strafeAction.ReadValue<float>();
            float rotation = rotationAction.ReadValue<float>();
            Strafe = Mathf.MoveTowards(Strafe, strafe, deltaTime * strafe_speed);
            Rotation = Mathf.MoveTowards(Rotation, rotation, deltaTime * rotation_speed);
        }

        private void UpdateKeyboardThrottle(float deltaTime) {
            if (Input.GetKey(KeyCode.W)) {
                Throttle = Mathf.MoveTowards(Throttle, 1f, deltaTime * throttle_speed);
                shipThrottle.SetValue(Throttle, false);
            }
            if (Input.GetKey(KeyCode.S)) {
                Throttle = Mathf.MoveTowards(Throttle, 0f, deltaTime * throttle_speed);
                shipThrottle.SetValue(Throttle, false);
            }
        }

        private void CalculateTurn(Vector3 gotoPos) {
            int version = 1;
            if (version == 0) {
                Vector3 localGotoPos = transform.InverseTransformVector(gotoPos - transform.position).normalized;

                Pitch = Mathf.Clamp(-localGotoPos.y * config.pitchSensitivity, -1f, 1f);
                Yaw = Mathf.Clamp(localGotoPos.x * config.yawSensitivity, -1f, 1f);
            }
            if (version == 1) {
                var mousePos = mouse.position.ReadValue();

                Pitch = (mousePos.y - (Screen.height * 0.5f)) / (Screen.height * 0.5f);
                Yaw = (mousePos.x - (Screen.width * 0.5f)) / (Screen.width * 0.5f);

                Pitch = -Mathf.Clamp(Pitch, -1.0f, 1.0f);
                Yaw = Mathf.Clamp(Yaw, -1.0f, 1.0f);
            }
        }

        private void CalculateRoll(Vector3 mousePos, Vector3 upVector) {
            float bankInfluence = (mousePos.x - (Screen.width * 0.5f)) / (Screen.width * 0.5f);
            bankInfluence = Mathf.Clamp(bankInfluence, -1f, 1f) * Throttle;

            float bankTarget = bankInfluence * config.bankLimit;

            float bankError = Vector3.SignedAngle(transform.up, upVector, transform.forward) - bankTarget;

            bankError = Mathf.Clamp(bankError * 0.1f, -1f, 1f);

            Roll = bankError;
            Debug.Log($"mousePos: {mousePos}, upVector: {upVector} bankInfluence: {bankInfluence}, bankTarget: {bankTarget}, bankError: {bankError}, Roll: {Roll}");
        }

        private void OnSliderThrottleChanged(float value) {
            DOTween.To(() => Throttle, x => Throttle = x, value, throttle_speed);
        }
    }
}