using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using Utils.Common;

namespace Utils.ScriptableObjects.Variables {
    [CreateAssetMenu(menuName = "Utils/Referenceable Variables/Float")]
    public class FloatVariable : ScriptableVariable<float> {
        public override void TryRemoveModifierGradually(ReferenceableVariable<float> modifier, float targetValue, float removeTime) {
            base.TryRemoveModifierGradually(modifier, targetValue, removeTime);
            _ = RemoveModifierAsync(modifier, targetValue, removeTime);
        }

        private async Task RemoveModifierAsync(ReferenceableVariable<float> modifier, float targetValue, float removeTime) {
            await DOTween.To(
                () => modifier.Value, 
                x => modifier.Value = x, targetValue, 
                removeTime).AsyncWaitForCompletion();
            TryRemoveModifier(modifier);
        }
    }
}