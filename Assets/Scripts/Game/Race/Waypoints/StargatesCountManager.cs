using System;
using UnityEngine;
using Utils.ScriptableObjects.Variables;

namespace Game.Race {
    public class StargatesCountManager : MonoBehaviour {
        [SerializeField] private StargatesRuntimeSet allStargates;
        [SerializeField] private FloatVariable passedStargatesCount;

        private void Start() {
            foreach (var stargate in allStargates.Items) {
                stargate.ShipEntered += OnShipEnteredStargate;
            }
        }

        private void OnDestroy() {
            foreach (var stargate in allStargates.Items) {
                stargate.ShipEntered -= OnShipEnteredStargate;
            }
        }

        private void OnShipEnteredStargate(StargateController stargate) {
            stargate.ShipEntered -= OnShipEnteredStargate;
            passedStargatesCount.SetValue(passedStargatesCount.BaseValue + 1);
        }
    }
}