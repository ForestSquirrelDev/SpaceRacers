using UnityEngine;

namespace Utils.Maths {
    public static class QuaternionExtensions {
        public static Quaternion SmoothRotateTowardsTarget(Transform origin, Transform target, float rotationSpeed, float timeDelta) {
            Quaternion targetRotation = Quaternion.LookRotation(target.position - origin.position);
            return Quaternion.Slerp(
                origin.rotation, targetRotation, rotationSpeed * timeDelta);
        }
    }
}