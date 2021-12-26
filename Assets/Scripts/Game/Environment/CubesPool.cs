using System.Collections.Generic;
using UnityEngine;
using Utils.Optimization;

namespace Game.Environment {
    [CreateAssetMenu(menuName = "Object Pools/Cubes Pool")]
    public class CubesPool : ObjectPool<GameObject> {
        [SerializeField] private GameObject cubePrefab;
        private Transform ParentInScene { get; set; }
        private Dictionary<GameObject, CubeData> cubesDataPool { get; } = new();
        
        public override GameObject GetObject() {
            GameObject cube = TryPickExistingObject(out GameObject obj) ? obj : CreateNewObject();
            cube.SetActive(true);
            return cube;
        }

        public CubeData GetCubeData(GameObject key) {
            return cubesDataPool.GetValueOrDefault(key);
        }

        protected override bool TryPickExistingObject(out GameObject obj) {
            foreach (var cubeGameObject in objects) {
                if (cubeGameObject.activeSelf) continue;
                obj = cubeGameObject;
                return true;
            }
            obj = null;
            return false;
        }

        protected override GameObject CreateNewObject() {
            GameObject cube = Instantiate(cubePrefab, ParentInScene, true);
            objects.Add(cube);
            CubeData data = new CubeData {
                gameObject = cube,
                cubeMonobehaviour = cube.GetComponent<Cube>(),
                meshRenderer = cube.GetComponent<MeshRenderer>(),
                rb = cube.GetComponent<Rigidbody>()
            };
            cubesDataPool.TryAdd(cube, data);
            SetParent(cube);
            return cube;
        }

        protected override void SetParent(GameObject cube) {
            cube.transform.parent = ParentInScene != null
                ? ParentInScene
                : ParentInScene = new GameObject("Cubes pool").transform;
        }
        
        public struct CubeData {
            public GameObject gameObject;
            public Cube cubeMonobehaviour;
            public Rigidbody rb;
            public MeshRenderer meshRenderer;
        }
    }
}