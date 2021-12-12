using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace Utils {
    public class RuntimeSet<T> : ScriptableObject {
        public event Action<RuntimeSetChangeType> CollectionChanged;
        public ReadOnlyCollection<T> Items => items.AsReadOnly();
        
        [SerializeField, TextArea(5, 10)] private string developerDescription;
        [NonSerialized] private List<T> items = new List<T>();

        public void AddItem(T item, bool invokeChangeEvent = false) {
            if (!items.Contains(item))
                items.Add(item);
            if (invokeChangeEvent)
                CollectionChanged?.Invoke(RuntimeSetChangeType.Addition);
        }

        public void RemoveItem(T item, bool invokeChangeEvent = false) {
            if (items.Contains(item))
                items.Remove(item);
            if (invokeChangeEvent)
                CollectionChanged?.Invoke(RuntimeSetChangeType.Removal);
        }
    }
    public enum RuntimeSetChangeType {
        Removal,
        Addition
    }
}
