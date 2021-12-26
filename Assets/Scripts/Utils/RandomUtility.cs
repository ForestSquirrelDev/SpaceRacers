using UnityEngine;

namespace Utils {
    public static class RandomUtility {
        public static Vector3 RandomPointInsideBox(Vector3 center, Vector3 size) {
            return center + new Vector3(
                (Random.value - 0.5f) * size.x,
                (Random.value - 0.5f) * size.y,
                (Random.value - 0.5f) * size.z
            );
        }
    }
}