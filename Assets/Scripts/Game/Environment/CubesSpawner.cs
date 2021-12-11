using UnityEngine;

namespace Game.Environment {
    public class CubesSpawner : MonoBehaviour {
        [SerializeField] private int count = 10;
        [SerializeField] private GameObject[] prefabs;
        [SerializeField] private float spawnAreaRadius = 100.0f;
        [SerializeField] private Vector2 scaleRange = new Vector2(5f, 20f);

        private void Start() => SpawnCubes();

        private void OnDrawGizmosSelected() {
            Gizmos.DrawWireSphere(transform.position, spawnAreaRadius);
        }

        private void SpawnCubes() {
            if (prefabs.Length == 0) {
                Debug.LogWarning("Can't instantiate asteroids. No prefabs have been set in the inspector.");
                return;
            }

            for (int i = 0; i <= count; i++) {
                GameObject cube = Instantiate(original: prefabs[Random.Range(0, prefabs.Length)],
                                                  position: Random.insideUnitSphere * spawnAreaRadius,
                                                  rotation: Quaternion.identity,
                                                  parent: transform);

                float randomScale = Random.Range(scaleRange.x, scaleRange.y);

                cube.transform.localScale = new Vector3(x: randomScale, y: randomScale, z: randomScale);
            }
        }
    }
}