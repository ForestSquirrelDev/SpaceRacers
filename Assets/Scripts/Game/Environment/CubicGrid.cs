using System.Collections.Generic;
using UnityEngine;

namespace Editor {
    public class CubicGrid : MonoBehaviour {
        public int width = 3;
        public int height = 3;
        public int depth = 3;

        public int gap = 300;
    
        public GameObject prefab;
        public List<Vector3> points = new List<Vector3>();

        private void Awake() {
            DefineCubicalGrid();
        }
        
        private void DefineCubicalGrid() {
            int xGap = gap;
            int yGap = gap;
            int zGap = gap;
            Vector3 selfPos = transform.position;

            int xLeft = -Mathf.FloorToInt((float)width / 2);
            int xRight = Mathf.CeilToInt((float)width / 2);

            int yBottom = -Mathf.FloorToInt((float)height / 2);
            int yTop = Mathf.CeilToInt((float)height / 2);

            int zClose = -Mathf.FloorToInt((float)depth / 2);
            int zFurther = Mathf.CeilToInt((float)depth / 2);

            for (int i = 0, x = xLeft; x < xRight; x++) {
                if (xGap < gap) {
                    xGap++;
                    continue;
                }
                xGap = 0;
                for (int y = yBottom; y < yTop; y++) {
                    if (yGap < gap) {
                        yGap++;
                        continue;
                    }
                    yGap = 0;
                    for (int z = zClose; z < zFurther; z++, i++) {
                        if (zGap < gap) {
                            zGap++;
                            continue;
                        }
                        zGap = 0;
                        Vector3 newPos = new Vector3(x, y, z);
                        Vector3 localPos = transform.InverseTransformDirection(selfPos + newPos);
                        Instantiate(prefab, localPos, Quaternion.identity, transform);
                    }
                }
            }
        }
    }
}