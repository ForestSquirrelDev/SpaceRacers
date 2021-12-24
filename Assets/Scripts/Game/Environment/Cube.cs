using System;
using Game.Shooting;
using UnityEngine;

namespace Game.Environment {
    public class Cube : MonoBehaviour, ITargetable {
        [SerializeField] TargetablesRuntimeSet allTargetables;

        void Awake() {
            allTargetables.AddItem(this);
        }

        public float DotProduct(Vector3 position, Vector3 projected) {
            Vector3 offset = transform.position - position;
            return Vector3.Dot(offset.normalized, projected.normalized);
        }

        public Vector3 Position() {
            return transform.position;
        }
    }
}