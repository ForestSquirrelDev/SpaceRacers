using UnityEngine;
using System;
using System.Collections.Generic;
using Utils.Common;

namespace Utils.ScriptableObjects {
    public abstract class ScriptableVariable<T> : ScriptableObject {
        public event Action<T> OnValueChanged;
        public delegate T Modify(T t1, T t2);
        public T BaseValue { get => value; }
        [NonSerialized]
        private T value;
        [NonSerialized]
        private Dictionary<Modify, ReferenceableVariable<T>> modifiers 
            = new Dictionary<Modify, ReferenceableVariable<T>>();

        public virtual void SetValue(T value, bool invokeCallback = false) {
            this.value = value;
            if (invokeCallback) OnValueChanged?.Invoke(this.value);
        }

        public virtual T ModifiedValue() {
            T newValue = value;
            foreach (var modifier in modifiers) {
                newValue = modifier.Key(modifier.Value.Value, newValue);
            }
            return newValue;
        }

        public virtual void AddModifier(Modify modifyingOperation, ReferenceableVariable<T> modifyingValue) {
            if (!modifiers.ContainsKey(modifyingOperation))
                modifiers.Add(modifyingOperation, modifyingValue);
        }

        public virtual void RemoveModifier(Modify modifyingOperation) {
            if (modifiers.ContainsKey(modifyingOperation))
                modifiers.Remove(modifyingOperation);
        }
    }
}
