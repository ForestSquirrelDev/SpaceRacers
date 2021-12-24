
using UnityEngine;

namespace Game.Shooting {
    public interface ITargetable {
        public float DotProduct(Vector3 oppositePosition, Vector3 projected);
        public Vector3 Position();
    }
}