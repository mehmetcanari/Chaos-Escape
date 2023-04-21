using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chaos.Escape
{
    public class IndicatorController : MonoBehaviour
    {
        #region INSPECTOR FIELDS

        [SerializeField] private Transform indicatorCore;

        #endregion

        #region UNITY METHODS

        private void Update()
        {
            CoreIndicatorForceLook();
        }

        #endregion

        #region PRIVATE METHODS

        private RaycastHit GettedHitFromMousePosition()
        {
            var ray = Camera.main.ScreenPointToRay(UnityEngine.Input.mousePosition);
            RaycastHit hit;
            Physics.Raycast(ray, out hit);
            return hit;
        }
        
        private void CoreIndicatorForceLook()
        {
            var hit = GettedHitFromMousePosition();
            if (hit.collider == null) return;
            
            var hitPoint = hit.point;
            hitPoint.y = indicatorCore.position.y;
            indicatorCore.LookAt(hitPoint);
        }

        #endregion
        
    }
}

