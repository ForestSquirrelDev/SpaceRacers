using Game;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

namespace Editor {
    [CustomEditor(typeof(CameraFollow))]
    public class CameraPositioner : UnityEditor.Editor {
        [FormerlySerializedAs("offset")] [SerializeField] private Vector3 positionOffset;

        public override void OnInspectorGUI() {
            CameraFollow cameraFollow = target as CameraFollow;
            Transform transform = cameraFollow.transform;
            Transform targetTransform = cameraFollow.Target;
            positionOffset = EditorGUILayout.Vector3Field("Position offset", positionOffset);

            if (GUILayout.Button("Position")) {
                transform.position = targetTransform.position + positionOffset;
                transform.rotation = Quaternion.LookRotation(targetTransform.forward, Vector3.up);
            }
        }
    }
}