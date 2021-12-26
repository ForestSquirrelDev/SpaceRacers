using System;
using JetBrains.Annotations;
using UnityEngine;

namespace Utils.ScriptableObjects.Variables {
    [CreateAssetMenu(menuName = "Scriptable Variables/Transform Variable")]

    public class TransformVariable : ScriptableVariable<Transform> {
        [CanBeNull] public new Transform BaseValue => value;

        [CanBeNull]
        public override Transform ModifiedValue() {
            return base.ModifiedValue();
        }
    }
}
