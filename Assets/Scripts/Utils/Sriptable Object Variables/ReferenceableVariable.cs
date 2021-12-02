using UnityEngine;
using System;

namespace Utils.ScriptableObjects {
    public class ReferenceableVariable<T> : ScriptableObject {
        public event Action<T> OnValueChanged;

        public T Value => value;
        [NonSerialized]
        private T value;

        public void SetValue(T value, bool invokeCallback = false) {
            this.value = value;
            if (invokeCallback) OnValueChanged?.Invoke(this.value);
        }
    }
}
