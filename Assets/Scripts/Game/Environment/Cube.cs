using System.Collections;
using Game.Configs.Environment;
using Game.Shooting;
using UnityEngine;
using Utils;

namespace Game.Environment {
    public class Cube : MonoBehaviour, ITargetable, IDestructible {
        bool ITargetable.CantBeTargeted { get; set; }
        [SerializeField] private TargetablesRuntimeSet allTargetables;
        [SerializeField] private CubeConfig config;
        [SerializeField] private AnimationCurve fadeAnimatiovCurve;
        
        private static readonly int Opacity = Shader.PropertyToID("_OPACITY");
        private Transform thisTransform;
        
        void Awake() {
            allTargetables.AddItem(this);
            thisTransform = transform;
        }

        private void Update() {
            if (Input.GetKeyDown(KeyCode.H)) {
                Destruct();
            }
            if (Input.GetKeyDown(KeyCode.A)) {
                MaterialPropertyBlock props = new MaterialPropertyBlock();
                props.SetFloat(Opacity, 0.1f);
                GetComponent<MeshRenderer>().SetPropertyBlock(props);
            }
        }
        

        public Transform GetTransform() {
            return thisTransform;
        }
        
        public float DotProduct(Vector3 position, Vector3 projected) {
            Vector3 offset = thisTransform.position - position;
            return Vector3.Dot(offset.normalized, projected.normalized);
        }

        public void Destruct() {
            int cubesCount = Random.Range(config.onDestroyCubesCountRange.x, config.onDestroyCubesCountRange.y + 1);
            GetComponent<Collider>().enabled = false;
            Vector3 localScale = thisTransform.localScale;
            for (int i = 0; i < cubesCount; i++) {
                CubeData cubeData = InstantiateChildCube(localScale);
                cubeData.rb.mass *= cubeData.scaleModifier;
                cubeData.rb.AddTorque(config.appliedTorque.x * cubeData.scaleModifier * Random.value, 
                    config.appliedTorque.y * cubeData.scaleModifier  * Random.value, 
                    config.appliedTorque.z * cubeData.scaleModifier * Random.value, 
                    ForceMode.Impulse);
                cubeData.rb.AddExplosionForce(Random.Range(
                    config.explosionForceRange.x * cubeData.scaleModifier,
                    config.explosionForceRange.y * cubeData.scaleModifier),
                    thisTransform.position, 
                    (localScale.x + localScale.y + localScale.z) / 3);
                ((ITargetable)cubeData.cubeMonobehaviour).CantBeTargeted = true;
                cubeData.cubeMonobehaviour.StartCoroutine(
                    FadeChildCubeOutRoutine(cubeData.meshRenderer, cubeData.gameObject));
            }
            gameObject.SetActive(false);
        }
        
        private CubeData InstantiateChildCube(Vector3 parentLocalScale) {
            GameObject cube = Instantiate(config.cubePrefab, RandomUtility.RandomPointInsideBox(
                thisTransform.position, parentLocalScale), Quaternion.identity);
            float scaleModifier = Random.Range(
                config.childCubeScaleMultiplierRange.x, config.childCubeScaleMultiplierRange.y);
            cube.transform.localScale = parentLocalScale * scaleModifier;
            return new CubeData {
                gameObject = cube,
                cubeMonobehaviour = cube.GetComponent<Cube>(),
                meshRenderer = cube.GetComponent<MeshRenderer>(),
                rb = cube.GetComponent<Rigidbody>(),
                scaleModifier = scaleModifier
            };
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

        private struct CubeData {
            public GameObject gameObject;
            public Cube cubeMonobehaviour;
            public Rigidbody rb;
            public MeshRenderer meshRenderer;
            public float scaleModifier;
        }
    }
}