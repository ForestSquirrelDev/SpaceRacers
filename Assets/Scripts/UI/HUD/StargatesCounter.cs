using System;
using System.Globalization;
using Game.Race;
using TMPro;
using UI.Configs;
using UnityEngine;
using Utils;

namespace UI.HUD {
    public class StargatesCounter : MonoBehaviour {
        [SerializeField] private StargatesRuntimeSet allStargates;
        [SerializeField] private TMP_Text counterText;
        [SerializeField] private StargatesCounterConfig config;

        private int totalStargates;
        private int stargatesPassed;
        private IFormatProvider provider;

        private void Awake() {
            provider = new CultureInfo("en-US");
        }

        private void Start() {
            totalStargates = allStargates.Items.Count;
            allStargates.CollectionChanged += OnStargateCollectionChanged;
            UpdateText();
        }

        private void OnDestroy() {
            allStargates.CollectionChanged -= OnStargateCollectionChanged;
        }

        private void OnStargateCollectionChanged(RuntimeSetChangeType type) {
            if (type == RuntimeSetChangeType.Removal) {
                stargatesPassed++;
            }
            UpdateText();
        }

        private void UpdateText() {
            counterText.text = $"{stargatesPassed}<space={config.dividerSpacingX.ToString(provider)}em>" +
                               $"<size={config.dividerSizePercents}>" +
                               $"<voffset={config.dividerheightOffset.ToString(provider)}em>" +
                               "<b>" +
                               "/" +
                               $"</b></voffset></line-height></size><space={config.dividerSpacingY.ToString(provider)}em>{totalStargates}";
        }
    }
}