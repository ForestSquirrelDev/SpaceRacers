using UnityEngine;

namespace Utils.Optimization {
    public abstract class GameObjectsPool : ObjectPool<GameObject> {
        [SerializeField] protected GameObject prefab;
        protected Transform ParentInScene { get; set; }

        public override GameObject GetObject() {
            GameObject gameObj = TryPickExistingObject(out GameObject obj) ? obj : CreateNewObject();
            gameObj.SetActive(true);
            return gameObj;
        }

        protected override bool TryPickExistingObject(out GameObject obj) {
            foreach (var gameObj in objects) {
                if (gameObj.activeSelf) continue;
                obj = gameObj;
                return true;
            }
            obj = null;
            return false;
        }

        protected override GameObject CreateNewObject() {
            GameObject obj = Instantiate(prefab, ParentInScene, true);
            objects.Add(obj);
            SetParent(obj);
            return obj;
        }

        protected override void SetParent(GameObject obj) {
            obj.transform.parent = ParentInScene != null
                ? ParentInScene
                : ParentInScene = new GameObject($"{obj.name}s pool").transform;
        }
    }
}