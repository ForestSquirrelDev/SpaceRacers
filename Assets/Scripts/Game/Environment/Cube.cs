using System.Collections;
using Game.Configs.Environment;
using Game.Shooting;
using UnityEngine;
using Utils;

namespace Game.Environment {
    public class Cube : MonoBehaviour, ITargetable, IDestructible {
        public bool CantBeTargeted { get; set; }
        
        [SerializeField] private TargetablesRuntimeSet allTargetables;
        [SerializeField] private CubesPool cubesPool;
        [SerializeField] private CubeConfig config;
        [SerializeField] private AnimationCurve fadeAnimatiovCurve;
        
        private static readonly int Opacity = Shader.PropertyToID("_OPACITY");
        private Transform thisTransform;
        
        void Awake() {
            allTargetables.AddItem(this);
            thisTransform = transform;
        }

        public Transform GetTransform() {
            return thisTransform;
        }
        
        public float DotProduct(Vector3 position, Vector3 projected) {
            Vector3 offset = thisTransform.position - position;
            return Vector3.Dot(offset.normalized, projected.normalized);
        }

        public void Destruct() {
            CantBeTargeted = true;
            int cubesCount = Random.Range(config.onDestroyCubesCountRange.x, config.onDestroyCubesCountRange.y + 1);
            GetComponent<Collider>().enabled = false;
            Vector3 localScale = thisTransform.localScale;
            for (int i = 0; i < cubesCount; i++) {
                CubesPool.CubeData cubeData = CreateChildCube(localScale, out float scaleModifier);
                cubeData.rb.mass *= scaleModifier;
                cubeData.rb.AddTorque(config.appliedTorque.x * scaleModifier * Random.value, 
                    config.appliedTorque.y * scaleModifier  * Random.value, 
                    config.appliedTorque.z * scaleModifier * Random.value, 
                    ForceMode.Impulse);
                cubeData.rb.AddExplosionForce(Random.Range(
                    config.explosionForceRange.x * scaleModifier,
                    config.explosionForceRange.y * scaleModifier),
                    thisTransform.position, 
                    (localScale.x + localScale.y + localScale.z) / 3);
                cubeData.cubeMonobehaviour.CantBeTargeted = true;
                cubeData.cubeMonobehaviour.StartCoroutine(
                    FadeChildCubeOutRoutine(cubeData.meshRenderer, cubeData.gameObject));
            }
            gameObject.SetActive(false);
        }
        
        private CubesPool.CubeData CreateChildCube(Vector3 parentLocalScale, out float childScaleModifier) {
            GameObject cube = cubesPool.GetObject();
            cube.transform.position = RandomUtility.RandomPointInsideBox(
                thisTransform.position, parentLocalScale);
            childScaleModifier = Random.Range(
                config.childCubeScaleMultiplierRange.x, config.childCubeScaleMultiplierRange.y);
            cube.transform.localScale = parentLocalScale * childScaleModifier;
            return cubesPool.GetCubeData(cube);
        }

        private IEnumerator FadeChildCubeOutRoutine(MeshRenderer meshRenderer, GameObject childCube) {
            MaterialPropertyBlock properties = new MaterialPropertyBlock();
            properties.SetFloat(Opacity, 1f);
            float counter = Random.value;
            float evaluated = 1f;
            while (evaluated > 0.01f) {
                evaluated = fadeAnimatiovCurve.Evaluate(counter);
                properties.SetFloat(Opacity, evaluated);
                counter += Time.deltaTime;
                meshRenderer.SetPropertyBlock(properties);
                yield return null;
            }
            childCube.SetActive(false);
        }
    }
}