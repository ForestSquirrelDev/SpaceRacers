using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace Utils {
    public class RuntimeSet<T> : ScriptableObject {
        public ReadOnlyCollection<T> Items => items.AsReadOnly();
        
        [SerializeField, TextArea(5, 10)] private string developerDescription;
        [NonSerialized] private List<T> items = new List<T>();

        public void AddItem(T item) {
            if (!items.Contains(item))
                items.Add(item);
        }

        public void RemoveItem(T item) {
            if (items.Contains(item))
                items.Remove(item);
        }
    }
}
