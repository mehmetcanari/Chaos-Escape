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
            ForceLookToIndicatorPosition();
        }

        #endregion
        
        #region PRIVATE METHODS

        private void ForceLookToIndicatorPosition()
        {
            var indicatorPosition = indicatorCore.position;
            indicatorPosition.y = characterMesh.position.y;
            characterMesh.LookAt(indicatorPosition);
        }

        #endregion
    }
}