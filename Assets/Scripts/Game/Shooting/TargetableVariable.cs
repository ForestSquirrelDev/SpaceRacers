using JetBrains.Annotations;
using UnityEngine;
using Utils.ScriptableObjects.Variables;

namespace Game.Shooting {
    [CreateAssetMenu(menuName = "Scriptable Variables/Targetable Variable")]
    public class TargetableVariable : ScriptableVariable<ITargetable> {
        [CanBeNull] public new ITargetable BaseValue => value;

        [CanBeNull]
        public override ITargetable ModifiedValue() {
            return base.ModifiedValue();
        }
    }
}