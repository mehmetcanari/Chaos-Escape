using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

namespace Chaos.Escape
{
    [CreateAssetMenu(fileName = "Character Data", menuName = "Character", order = 0)]
    public class CharacterMovementData : ScriptableObject
    {
        #region SHARED DATA

        public int characterAccelerationValue;
        public int characterFinalVelocity;

        #endregion
        
    }
}