using System;
using System.Collections.Generic;
using Game.Configs.Ship;
using Game.Configs.Shooting;
using Game.Ship.PlayerInput;
using Game.Shooting;
using UnityEngine;
using Utils.Maths;
using Utils.ScriptableObjects.Variables;

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
            TargetableVariable currentTarget, FloatVariable shipSpeed, ShipConfig config, LaserBeamsPool lasersPool,
            float initialProjectileSpeed) {
            this.shootingInput = shootingInput;
            this.guns.Add(guns.Item1);
            this.guns.Add(guns.Item2);
            this.currentTarget = currentTarget;
            this.shipSpeed = shipSpeed;
            this.lasersPool = lasersPool;
            this.laserBeamConfig = laserBeamConfig;
            this.projectileSpeed = initialProjectileSpeed;

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
                LaserBeam laserBeam = lasersPool.GetLaserMonobehaviour(laser);
                laserBeam.Init(currentTarget.BaseValue, laserBeamConfig);
                Vector3 direction = gun.BaseValue.transform.forward;
                Vector3 initialForce = direction * projectileSpeed;
                laserBeam.Rigidbody.AddForce(initialForce + initialForce.normalized * 
                    shipSpeed.ModifiedValue().ClampPos1ToMaxValue() 
                    * (laserBeam.Rigidbody.drag * laserBeam.Rigidbody.mass), ForceMode.Impulse);
                laser.transform.rotation = gun.BaseValue.rotation;
            }
        }
    }
}