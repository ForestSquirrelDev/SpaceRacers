using System;
using System.Collections.Generic;
using Game.Configs.Ship;
using Game.Ship.PlayerInput;
using Game.Shooting;
using UnityEngine;
using Utils.Maths;
using Utils.ScriptableObjects.Variables;
using Object = UnityEngine.Object;

namespace Game.Ship {
    public class LaserCannon : IDisposable {
        private ShipShootingInputProcessor shootingInput;
        private TargetableVariable currentTarget;
        private FloatVariable shipSpeed;
        private GameObject laserPrefab;
        private List<TransformVariable> guns = new();

        private float projectileSpeed;
         
        public LaserCannon((TransformVariable, TransformVariable) guns, ShipShootingInputProcessor shootingInput, GameObject laserPrefab,
            TargetableVariable currentTarget, FloatVariable shipSpeed, ShipConfig config) {
            this.shootingInput = shootingInput;
            this.laserPrefab = laserPrefab;
            this.guns.Add(guns.Item1);
            this.guns.Add(guns.Item2);
            this.currentTarget = currentTarget;
            this.shipSpeed = shipSpeed;
            this.projectileSpeed = config.projectileSpeed;

            shootingInput.FireRequired += OnFireRequired;
        }

        public void Dispose() {
            shootingInput.FireRequired -= OnFireRequired;
        }

        private void OnFireRequired() {
            foreach (var gun in guns) {
                if (gun.BaseValue == null) {
                    Debug.LogError("Gun transform variable is null.");
                    return;
                }
                var laser = Object.Instantiate(laserPrefab, gun.BaseValue.position, Quaternion.identity)
                    .GetComponent<LaserBeam>();
                laser.Init(currentTarget.BaseValue);
                Vector3 direction = gun.BaseValue.transform.forward;
                laser.Rigidbody.AddForce(direction * ( projectileSpeed + 
                                                       shipSpeed.ModifiedValue().ClampPos1ToMaxValue()), ForceMode.Impulse);
                laser.transform.rotation = gun.BaseValue.rotation;
            }
        }
    }
}