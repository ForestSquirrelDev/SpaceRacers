using System.Collections.Generic;
using UnityEngine;
using Utils.Optimization;

namespace Game.Shooting {
    [CreateAssetMenu(menuName = "Object Pools/Lasers Pool")]
    public class LaserBeamsPool : GameObjectsPool {
        private Dictionary<GameObject, LaserBeam> laerMonobehavioursPool { get; } = new();

        public LaserBeam GetLaserMonobehaviour(GameObject key) {
            return laerMonobehavioursPool.GetValueOrDefault(key);
        }

        protected override GameObject CreateNewObject() {
            GameObject laser = base.CreateNewObject();
            laerMonobehavioursPool.TryAdd(laser, laser.GetComponent<LaserBeam>());
            return laser;
        }
    }
}