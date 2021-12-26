
using UnityEngine;

namespace Game.Shooting {
    public interface ITargetable {
        public bool CantBeTargeted { get; set; }
        public float DotProduct(Vector3 oppositePosition, Vector3 projected);
        public Transform GetTransform();
    }
}