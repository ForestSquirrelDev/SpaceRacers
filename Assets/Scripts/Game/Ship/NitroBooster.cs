using Game.Configs.Ship;
using Game.Ship.PlayerInput;
using UnityEngine;
using Utils.Common;
using Utils.ScriptableObjects.Variables;

namespace Game.Ship {
    public class NitroBooster {
        private FloatVariable throttlePower, nitroBank;
        private ShipConfig config;
        private ShipMovementInputProcessor movementInput;
        private ReferenceableVariable<float> nitro = new(1f);

        private readonly float maxCapacity;

        public NitroBooster(FloatVariable throttlePower, ShipConfig config, FloatVariable nitroBank, ShipMovementInputProcessor movementInput) {
            this.throttlePower = throttlePower;
            this.config = config;
            this.nitroBank = nitroBank;
            this.movementInput = movementInput;

            this.throttlePower.TryAddModifier((f1, f2) => (f1 * f2), nitro);
            this.nitroBank.SetValue(config.nitroCapacity);
            maxCapacity = config.nitroCapacity;
        }

        public void Update(float deltaTime) {
            if (movementInput.NitroRequired) {
                if (nitroBank.ModifiedValue() > 0) {
                    AddNitro(deltaTime);
                    RemoveFromBank(deltaTime);
                }
                else {
                    ReduceNitro(deltaTime);
                }
            } else {
                ReduceNitro(deltaTime);
                AddToBank(deltaTime);
            }
        }

        private void ReduceNitro(float deltaTime) {
            nitro.Value = Mathf.Lerp(nitro.Value, nitro.StartValue, config.nitroSensitivity * deltaTime);
        }

        private void AddNitro(float deltaTime) {
            nitro.Value = Mathf.Lerp(nitro.Value, config.nitroPower, config.nitroSensitivity * deltaTime);
        }

        private void AddToBank(float deltaTime) {
            if (nitroBank.ModifiedValue() < maxCapacity)
                nitroBank.SetValue(nitroBank.ModifiedValue() + deltaTime);
        }

        private void RemoveFromBank(float deltaTime) {
            if (nitroBank.ModifiedValue() > 0)
                nitroBank.SetValue(nitroBank.ModifiedValue() - deltaTime);
        }
    }
}
