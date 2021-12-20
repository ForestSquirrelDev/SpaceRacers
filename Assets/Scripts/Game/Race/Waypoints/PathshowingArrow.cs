using System.Collections.Generic;
using Configs.Game.Race;
using DG.Tweening;
using UnityEngine;
using Utils;

namespace Game.Race {
    public class PathshowingArrow : MonoBehaviour {
        [SerializeField] private StargatesRuntimeSet allStargates;
        [SerializeField] private PathshowingArrowConfig config;
        
        private Queue<StargateController> stargatesQueue = new ();
        private StargateController currentStargate;
        
        private void Start() {
            foreach (var stargate in allStargates.Items) 
                stargatesQueue.Enqueue(stargate);
            currentStargate = stargatesQueue.Dequeue();
            allStargates.CollectionChanged += OnStargatesCollectionChanged;
            
            transform.DOScale(Vector3.one * config.tweeningScaleMultiplyer, config.tweenDuration)
                .SetLoops(-1, LoopType.Yoyo);
        }

        private void Update() {
            Quaternion targetRotation = Quaternion.LookRotation(
                currentStargate.transform.position - transform.position);
            transform.rotation = Quaternion.Slerp(
                transform.rotation, targetRotation, config.rotationSpeed * Time.deltaTime);
        }
        
        private void OnStargatesCollectionChanged(RuntimeSetChangeType changeType) {
            if (changeType != RuntimeSetChangeType.Removal) return;
            if (stargatesQueue.TryDequeue(out StargateController stargate))
                currentStargate = stargate;
        }
    }
}