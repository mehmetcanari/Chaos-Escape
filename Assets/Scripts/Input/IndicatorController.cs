using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace Chaos.Escape
{
    public class IndicatorController : MonoBehaviour
    {
        #region INSPECTOR FIELDS

        [FoldoutGroup("Indicator Settings")]
        [SerializeField] private Transform indicator;
        [SerializeField] private Transform laserTransform;
        [SerializeField] private MeshRenderer laserRenderer;

        #endregion

        #region UNITY METHODS

        private void Awake()
        {
            SetLaserVisibility(true);
        }

        private void Update()
        {
            SetIndicatorPositionToHitPoint(GettedHitFromMousePosition(), 2);
            ScaleAimLaserByDistanceToHitPoint(GettedHitFromMousePosition());
            ClampAimLaserScale(5);
        }

        #endregion

        #region PRIVATE METHODS

        private void SetLaserVisibility(bool isVisible)
        {
            laserRenderer.gameObject.SetActive(isVisible);
        }
        
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
            var localScale = laserTransform.transform.localScale;

            laserTransform.localScale =
                new Vector3(localScale.x, localScale.y, distance / 1.4f);
        }
        
        private void ClampAimLaserScale(float maxScale)
        {
            var localScale = laserTransform.transform.localScale;
            var clampedScale = Mathf.Clamp(localScale.z, 0, maxScale);
            laserTransform.localScale = new Vector3(localScale.x, localScale.y, clampedScale);
        }
        
#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            var position = indicator.position;
        
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(new Vector3(position.x, position.y - 0.5f, position.z), 0.5f);
        }
#endif

        #endregion
    }
}