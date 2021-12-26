using System.Collections.Generic;
using UnityEngine;
using Utils.Optimization;

namespace Game.Environment {
    [CreateAssetMenu(menuName = "Object Pools/Cubes Pool")]
    public class CubesPool : ObjectPool<GameObject> {
        [SerializeField] private GameObject cubePrefab;
        private Transform ParentInScene { get; set; }
        private Dictionary<GameObject, Cube> monobehavioursPool { get; } = new();
        
        public override GameObject GetObject() {
            GameObject cube = TryPickExistingObject(out GameObject obj) ? obj : CreateNewObject();
            cube.SetActive(true);
            return cube;
        }

        public Cube GetCubeMonobehaviour(GameObject key) {
            return monobehavioursPool.TryGetValue(key, out Cube cube) ? cube : null;
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
            monobehavioursPool.TryAdd(cube, cube.GetComponent<Cube>());
            SetParent(cube);
            return cube;
        }

        protected override void SetParent(GameObject cube) {
            cube.transform.parent = ParentInScene != null
                ? ParentInScene
                : ParentInScene = new GameObject("Cubes pool").transform;
        }
    }
}