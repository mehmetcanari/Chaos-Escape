using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Chaos.Escape
{
    public class IndicatorController : MonoBehaviour
    {
        #region INSPECTOR FIELDS

        [SerializeField] private Transform indicator;

        #endregion

        #region UNITY METHODS

        private void Update()
        {
            SetIndicatorPositionToHitPoint(GettedHitFromMousePosition(), 0.5f);
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
        
        private void SetIndicatorPositionToHitPoint(RaycastHit hit, float offset)
        {
            indicator.position = new Vector3(hit.point.x, hit.point.y + offset, hit.point.z);
        }

        #endregion
        
    }
}

