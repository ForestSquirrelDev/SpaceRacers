using System;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using Utils.Common;

namespace Utils.ScriptableObjects.Variables {
    [CreateAssetMenu(menuName = "Scriptable Variables/Float Variable")]
    public class FloatVariable : ScriptableVariable<float> {
        [NonSerialized] private bool removalRunning;
        
        public async Task TryRemoveModifierGraduallyAsync(ReferenceableVariable<float> modifier, float targetValue, float removeTime) {
            if (removalRunning) return;
            removalRunning = true;
            await DOTween.To(
                () => modifier.Value, 
                x => modifier.Value = x, targetValue, 
                removeTime).AsyncWaitForCompletion();
            TryRemoveModifier(modifier);
            removalRunning = false;
        }
    }
}