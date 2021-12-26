using System.Collections.Generic;
using UnityEngine;
using Utils.Optimization;

namespace Game.Shooting {
    [CreateAssetMenu(menuName = "Object Pools/Lasers Pool")]
    public class LaserBeamsPool : GameObjectsPool {
        private Dictionary<GameObject, LaserBeam> laserMonobehavioursPool { get; } = new();

        public LaserBeam GetLaserMonobehaviour(GameObject key) {
            return laserMonobehavioursPool.GetValueOrDefault(key);
        }

        protected override GameObject CreateNewObject() {
            GameObject laser = base.CreateNewObject();
            laserMonobehavioursPool.TryAdd(laser, laser.GetComponent<LaserBeam>());
            return laser;
        }
    }
}