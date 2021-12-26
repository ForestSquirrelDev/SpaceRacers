using System;
using System.Collections.Generic;
using Game.Configs.Ship;
using Game.Configs.Shooting;
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
        private LaserBeamsPool lasersPool;
        private LaserBeamConfig laserBeamConfig;
        private List<TransformVariable> guns = new();

        private float projectileSpeed;
         
        public LaserCannon((TransformVariable, TransformVariable) guns, 
            ShipShootingInputProcessor shootingInput, LaserBeamConfig laserBeamConfig,
            TargetableVariable currentTarget, FloatVariable shipSpeed, ShipConfig config, LaserBeamsPool lasersPool) {
            this.shootingInput = shootingInput;
            this.guns.Add(guns.Item1);
            this.guns.Add(guns.Item2);
            this.currentTarget = currentTarget;
            this.shipSpeed = shipSpeed;
            this.lasersPool = lasersPool;
            this.laserBeamConfig = laserBeamConfig;
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
                GameObject laser = lasersPool.GetObject();
                laser.transform.position = gun.BaseValue.position;
                LaserBeam laserMonobehaviour = lasersPool.GetLaserMonobehaviour(laser);
                laserMonobehaviour.Init(currentTarget.BaseValue, laserBeamConfig);
                Vector3 direction = gun.BaseValue.transform.forward;
                laserMonobehaviour.Rigidbody.AddForce(direction * ( projectileSpeed + 
                                                       shipSpeed.ModifiedValue().ClampPos1ToMaxValue()), ForceMode.Impulse);
                laser.transform.rotation = gun.BaseValue.rotation;
            }
        }
    }
}