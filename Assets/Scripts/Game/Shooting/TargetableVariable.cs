using UnityEngine;
using Utils.ScriptableObjects.Variables;

namespace Game.Shooting {
    [CreateAssetMenu(menuName = "Scriptable Variables/Targetable Variable")]
    public class TargetableVariable : ScriptableVariable<ITargetable> { }
}