
using UnityEngine;

namespace Game.Shooting {
    /// <summary>
    /// Targetable gameObject.activeSelf may be false.
    /// </summary>
    public interface ITargetable {
        public bool CantBeTargeted { get; set; }
        public float ProjectVectorOnOffset(Vector3 oppositePosition, Vector3 projected);
        public Transform GetTransform();
    }
}