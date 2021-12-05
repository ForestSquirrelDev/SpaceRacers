using Game.Configs;
using UnityEngine;
using Utils.Common;
using Utils.ScriptableObjects;

namespace Game.Ship {
    public class NitroBooster {
        FloatVariable throttlePower, nitroBank;
        ShipConfig config;
        ShipInputProcessor input;
        ReferenceableVariable<float> nitro = new ReferenceableVariable<float>(1f);

        float maxCapacity;

        public NitroBooster(FloatVariable throttlePower, ShipConfig config, FloatVariable nitroBank, ShipInputProcessor input) {
            this.throttlePower = throttlePower;
            this.config = config;
            this.nitroBank = nitroBank;
            this.input = input;

            this.throttlePower.AddModifier((f1, f2) => (f1 * f2), nitro);
            this.nitroBank.SetValue(config.nitroCapacity);
            maxCapacity = config.nitroCapacity;
        }

        public void Update(float deltaTime) {
            if (input.NitroRequired) {
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
