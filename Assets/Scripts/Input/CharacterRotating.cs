using System;
using UnityEngine;

namespace Chaos.Escape
{
    public class CharacterRotating : MonoBehaviour
    {
        #region INSPECTOR FIELDS

        [SerializeField] private Transform indicatorCore;
        [SerializeField] private Transform characterMesh;
        
        #endregion

        #region UNITY METHODS

        private void Update()
        {
            ForceLookToIndicatorRotationY();
        }

        #endregion
        
        #region PRIVATE METHODS

        private void ForceLookToIndicatorRotationY()
        {
            var indicatorRotationY = indicatorCore.rotation.eulerAngles.y;
            characterMesh.rotation = Quaternion.Euler(0, indicatorRotationY, 0);
        }

        #endregion
    }
}