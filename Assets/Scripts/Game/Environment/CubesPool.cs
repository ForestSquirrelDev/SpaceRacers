using System.Collections.Generic;
using UnityEngine;
using Utils.Optimization;

namespace Game.Environment {
    [CreateAssetMenu(menuName = "Object Pools/Cubes Pool")]
    public class CubesPool : GameObjectsPool {
        private Dictionary<GameObject, CubeData> cubesDataPool { get; } = new();

        public CubeData GetCubeData(GameObject key) {
            return cubesDataPool.GetValueOrDefault(key);
        }

        protected override GameObject CreateNewObject() {
            GameObject cube = base.CreateNewObject();
            CubeData data = new CubeData {
                gameObject = cube,
                cubeMonobehaviour = cube.GetComponent<Cube>(),
                meshRenderer = cube.GetComponent<MeshRenderer>(),
                rb = cube.GetComponent<Rigidbody>()
            };
            cubesDataPool.TryAdd(cube, data);
            return cube;
        }

        public struct CubeData {
            public GameObject gameObject;
            public Cube cubeMonobehaviour;
            public Rigidbody rb;
            public MeshRenderer meshRenderer;
        }
    }
}