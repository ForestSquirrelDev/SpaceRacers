using UnityEngine;
using Utils;
using Utils.ScriptableObjects.Variables;

namespace Game.Race {
    public class StargatesManager : MonoBehaviour {
        [SerializeField] private StargatesRuntimeSet stargatesSet;
        [SerializeField] private TransformVariable currentStargate;

        private int currentStargateIndex = 0;
        
        private void Awake() {
            StargateController[] sceneStargates = GetComponentsInChildren<StargateController>();
            foreach (var stargate in sceneStargates) 
                stargatesSet.AddItem(stargate);
            currentStargate.SetValue(sceneStargates[currentStargateIndex].transform);

            stargatesSet.CollectionChanged += OnStargatesCollectionChanged;
        }

        private void OnDestroy() {
            stargatesSet.CollectionChanged -= OnStargatesCollectionChanged;
        }

        private void OnStargatesCollectionChanged(RuntimeSetChangeType changeType) {
            if (changeType != RuntimeSetChangeType.Removal) return;
            currentStargateIndex++;
            currentStargate.SetValue(stargatesSet.Items[currentStargateIndex].transform);
        }
    }
}