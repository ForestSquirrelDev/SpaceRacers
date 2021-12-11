using UnityEngine;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Utils.Common;

namespace Utils.ScriptableObjects.Variables {
    public abstract class ScriptableVariable<T> : ScriptableObject {
        public event Action<T> OnValueChanged;
        public delegate T Modify(T t1, T t2);
        public T BaseValue => value;

        [SerializeField, TextArea(5, 10)] private string developerDescription;
        [NonSerialized] private T value;
        [NonSerialized] private Dictionary<ReferenceableVariable<T>, Modify> modifiers = new();

        public void SetValue(T value, bool invokeCallback = false) {
            this.value = value;
            if (invokeCallback) OnValueChanged?.Invoke(this.value);
        }

        public T ModifiedValue() {
            T newValue = value;
            foreach (var modifier in modifiers) {
                newValue = modifier.Value(modifier.Key.Value, newValue);
            }
            return newValue;
        }

        public void TryAddModifier(Modify modifyingOperation, ReferenceableVariable<T> modifyingValue) {
            if (!modifiers.ContainsKey(modifyingValue))
                modifiers.Add(modifyingValue, modifyingOperation);
        }

        public void TryRemoveModifier(ReferenceableVariable<T> modifier) {
            if (modifiers.ContainsKey(modifier))
                modifiers.Remove(modifier);
        }
    }
}
