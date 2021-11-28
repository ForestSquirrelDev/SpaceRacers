using UnityEngine;
using Utils.Maths;

namespace Utils.Vectors {
    public static class VectorUtility {
        public static Vector2 ClampNeg1To1 (this Vector2 v) {
            return new Vector2( v.x.ClampNeg1To1(), v.y.ClampNeg1To1() );
        }
    }
}
