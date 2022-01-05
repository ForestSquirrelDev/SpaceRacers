using UnityEngine;
using Utils.ScriptableObjects.Variables;

namespace Game.ScriptableVariables {
    [CreateAssetMenu(menuName = "Scriptable Variables/Distance To Target Variable")]
    public class DistanceToTargetVariable : FloatVariable {
        private bool updatingDistance;
        public bool UpdatingDistance {
            get => updatingDistance;
            set {
                if (updatingDistance == value) return;
                updatingDistance = value;
            } 
        }
    }
}