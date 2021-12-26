using System;
using System.Collections.Generic;
using UnityEngine;

namespace Utils.Optimization {
    public abstract class ObjectPool<T> : ScriptableObject {
        [NonSerialized] protected List<T> objects = new();

        public abstract T GetObject();
        
        protected abstract bool TryPickExistingObject(out T obj);

        protected abstract T CreateNewObject();

        protected abstract void SetParent(T obj);
    }
}