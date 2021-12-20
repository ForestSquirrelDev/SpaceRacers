using UnityEngine;

namespace Game.Race {
    public class StargatesManager : MonoBehaviour {
        [SerializeField] private StargatesRuntimeSet stargatesSet;
        
        private StargateController[] sceneStargates;

        private void Awake() {
            sceneStargates = GetComponentsInChildren<StargateController>();
            foreach (var stargate in sceneStargates) 
                stargatesSet.AddItem(stargate);
        }
    }
}