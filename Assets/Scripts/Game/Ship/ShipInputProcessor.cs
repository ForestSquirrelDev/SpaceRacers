using UnityEngine;
using UnityEngine.InputSystem;
using Utils;
using DG.Tweening;
using Game.Configs;
using System;
using TMPro;
using Utils.Maths;
using Utils.Vectors;

namespace Game.Ship {
    public class ShipInputProcessor : IDisposable {
        public float Throttle { get; private set; }
        public float Strafe { get; private set; }
        public float Rotation { get; private set; }
        public float Roll { get; private set; }
        public float Pitch { get; private set; }
        public float Yaw { get; private set; }

        private Vector2 mouseInput;

        private FloatVariable shipThrottle;
        private ShipConfig config;
        private Transform transform;

        private Mouse mouse;
        private ShipInputActions shipInputActions;
        private InputAction strafeAction;
        private InputAction rotationAction;
        private TMP_Text debugText;

        private const float throttle_speed = 0.5f;
        private const float strafe_speed = 0.5f;
        private const float rotation_speed = 0.5f;

        public ShipInputProcessor(ShipConfig config, FloatVariable shipThrottle, Transform transform, TMP_Text text) {
            this.config = config;
            this.shipThrottle = shipThrottle;
            this.transform = transform;

            mouse = Mouse.current;
            shipInputActions = new ShipInputActions();
            strafeAction = shipInputActions.Ship.StrafeAxis;
            rotationAction = shipInputActions.Ship.RotationAxis;

            this.debugText = text;

            strafeAction.Enable();
            rotationAction.Enable();

            shipThrottle.OnValueChanged += OnSliderThrottleChanged;
        }

        public void Update(float deltaTime) {
            UpdateKeyboardThrottle(deltaTime);
            UpdateInputAxes(deltaTime);
            FindMousePosition(out Vector3 screenPos, out Vector3 worldPos);
            CalculateTurn(worldPos);
            CalculateRoll(screenPos, Camera.main.transform.up, deltaTime);
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

        private void FindMousePosition(out Vector3 mousePos, out Vector3 gotoPos) {
            Vector2 mouseInput = mouse.position.ReadValue();
            mousePos = new Vector3(mouseInput.x, mouseInput.y, 1000f);
            gotoPos = Camera.main.ScreenToWorldPoint(mousePos);

            var inputY = (mouseInput.y - (Screen.height * 0.5f)) / (Screen.height * 0.5f);
            var inputX = (mouseInput.x - (Screen.width * 0.5f)) / (Screen.width * 0.5f);

            this.mouseInput = new Vector2(inputX, inputY).ClampNeg1To1();
        }

        private void CalculateTurn(Vector3 gotoPos) {
            Vector3 localGotoPos = transform.InverseTransformVector(gotoPos - transform.position).normalized;
                
            Pitch = Mathf.Clamp(localGotoPos.x * config.pitchSensitivity, -config.pitchSensitivity, config.pitchSensitivity);
            Yaw = Mathf.Clamp(-localGotoPos.y * config.yawSensitivity, -config.yawSensitivity, config.yawSensitivity);
        }

        private void CalculateRoll(Vector3 mousePos, Vector3 cameraUp, float deltaTime) {
            string s = "";

            float inputRotation = rotationAction.ReadValue<float>() * config.customRollSensitivity;

            float rollInfluence = -mouseInput.x * Throttle;
            float yInfluence = MathfExtensions.InverseRelationship(3f, mouseInput.y * 10f);
            yInfluence = Mathf.Clamp(Mathf.Abs(yInfluence), float.MinValue, 1f);
            rollInfluence *= yInfluence;
            rollInfluence *= config.autoRollSensitivity;

            if (inputRotation != 0) Roll = inputRotation;
            else Roll = Mathf.MoveTowards(Roll, rollInfluence, deltaTime * 1f);

            s = s + " " + $"mousePos: {mousePos}, bankError: {rollInfluence}, Roll: {Roll}, " +
                $"transform up :{ transform.up} cam: {cameraUp} , " +
                $"yInfluence: {yInfluence}" +
                $"rough mouse y: {mouseInput.y}, yinfluence: {yInfluence}";

            this.debugText.text = s;
        }

        private void OnSliderThrottleChanged(float value) {
            DOTween.To(() => Throttle, x => Throttle = x, value, throttle_speed);
        }
    }
}