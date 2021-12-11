using UnityEngine;
using Utils.Common;
using Utils.ScriptableObjects;
using Utils.ScriptableObjects.Variables;

namespace Game.Environment {
    public class ForceField : MonoBehaviour {
        [SerializeField] private TransformVariable shipTransform;
        [SerializeField] private FloatVariable shipThrottle;
        [SerializeField] private float radius = 1000f;
        [SerializeField] private float lookAtThreshold = 0.7f;

        private ReferenceableVariable<float> modifier = new (0.001f);

        private void Update() {
            bool isInside = Vector3.Distance(transform.position, shipTransform.BaseValue.position) < radius;
            if (isInside) return;
            
            Vector3 offset = transform.position - shipTransform.BaseValue.position;
            float dot = Vector3.Dot(offset.normalized, shipTransform.BaseValue.forward);
            bool looksAtCenter = dot >= lookAtThreshold;
            if (looksAtCenter) {
                shipThrottle.TryRemoveModifierGradually(modifier, 1f, 3f);
            }
            else {
                shipThrottle.TryAddModifier((f1, f2) => f1 * f2, modifier);
            }
        }

        private void OnDrawGizmos() {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, radius);
        }
    }
}
