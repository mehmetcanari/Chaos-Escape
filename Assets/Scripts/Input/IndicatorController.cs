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
        [SerializeField] private Transform aimLaser;

        #endregion

        #region UNITY METHODS

        private void Update()
        {
            SetIndicatorPositionToHitPoint(GettedHitFromMousePosition(), 0.5f);
            ScaleAimLaserByDistanceToHitPoint(GettedHitFromMousePosition());
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

        private void ScaleAimLaserByDistanceToHitPoint(RaycastHit hit)
        {
            var distance = Vector3.Distance(transform.position, hit.point);
            var localScale = aimLaser.transform.localScale;
            
            aimLaser.localScale =
                new Vector3(localScale.x, localScale.y, (distance * 1.25f));
        }

        #endregion
    }
}