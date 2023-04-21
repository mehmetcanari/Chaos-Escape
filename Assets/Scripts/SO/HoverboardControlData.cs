using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

namespace Chaos.Escape
{
    [CreateAssetMenu(fileName = "Hoverboard Data", menuName = "Hoverboard Data", order = 0)]
    public class HoverboardControlData : ScriptableObject
    {
        #region SHARED DATA

        public int hoverboardAccelerationValue;
        public int hoverboardFinalVelocity;

        #endregion
        
    }
}