using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Chaos.Escape
{
    public class HoverboardMovement : Input
    {
        #region INSPECTOR FIELDS

        public HoverboardControlData hoverboardControlData;
        [SerializeField] private Transform hoverboardTransform;

        #endregion

        #region UNITY METHODS

        private void Update()
        {
            Movement(hoverboardTransform);            
        }

        #endregion

        #region PRIVATE METHODS

        private void Movement(Transform target)
        {
            var hoverboardMoveSpeed = hoverboardControlData.hoverboardSpeed;
            if(IsPressedHold(KeyCode.W)) target.Translate(Vector3.forward * (hoverboardMoveSpeed * Time.deltaTime));
            if(IsPressedHold(KeyCode.A)) target.Translate(Vector3.left * (hoverboardMoveSpeed * Time.deltaTime));
            if(IsPressedHold(KeyCode.S)) target.Translate(Vector3.back * (hoverboardMoveSpeed * Time.deltaTime));
            if(IsPressedHold(KeyCode.D)) target.Translate(Vector3.right * (hoverboardMoveSpeed * Time.deltaTime));
        }

        #endregion
    }
}