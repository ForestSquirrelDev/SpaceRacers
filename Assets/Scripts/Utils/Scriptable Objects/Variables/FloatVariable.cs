using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using Utils.Common;

namespace Utils.ScriptableObjects.Variables {
    [CreateAssetMenu(menuName = "Utils/Referenceable Variables/Float")]
    public class FloatVariable : ScriptableVariable<float> {
        public async Task TryRemoveModifierGraduallyAsync(ReferenceableVariable<float> modifier, float targetValue, float removeTime) {
            await DOTween.To(
                () => modifier.Value, 
                x => modifier.Value = x, targetValue, 
                removeTime).AsyncWaitForCompletion();
            TryRemoveModifier(modifier);
        }
    }
}