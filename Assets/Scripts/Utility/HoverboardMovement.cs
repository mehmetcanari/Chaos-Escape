using System;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Serialization;

namespace Chaos.Escape
{
    public class HoverboardMovement : Input
    {
        #region INSPECTOR FIELDS

        public HoverboardControlData hoverboardControlData;
        [SerializeField] private Rigidbody hoverboardPhysics;
        
        [SerializeField]private float velocityCutDuration;
        private Tween _velocityTween;
        
        #endregion

        #region UNITY METHODS

        private void FixedUpdate()
        {
            FixVelocityStamp(hoverboardPhysics);
            Movement(hoverboardPhysics);
            CutVelocityWhenStopped();
        }

        #endregion

        #region PRIVATE METHODS

        private void Movement(Rigidbody targetPhysics)
        {
            if (!HasAnyMovementKeyPressed()) return;
            var hoverboardAccelerationValue = hoverboardControlData.hoverboardAccelerationValue;
            DOTween.Complete(_velocityTween);
            
            if (IsPressedHold(KeyCode.W))
                targetPhysics.AddForce(Vector3.forward * (hoverboardAccelerationValue * Time.deltaTime),
                    ForceMode.Acceleration);
            if (IsPressedHold(KeyCode.A))
                targetPhysics.AddForce(Vector3.left * (hoverboardAccelerationValue * Time.deltaTime),
                    ForceMode.Acceleration);
            if (IsPressedHold(KeyCode.S))
                targetPhysics.AddForce(Vector3.back * (hoverboardAccelerationValue * Time.deltaTime),
                    ForceMode.Acceleration);
            if (IsPressedHold(KeyCode.D))
                targetPhysics.AddForce(Vector3.right * (hoverboardAccelerationValue * Time.deltaTime),
                    ForceMode.Acceleration);
        }

        private bool HasAnyMovementKeyPressed()
        {
            return IsPressedHold(KeyCode.W) || IsPressedHold(KeyCode.A) || IsPressedHold(KeyCode.S) ||
                   IsPressedHold(KeyCode.D);
        }

        private void FixVelocityStamp(Rigidbody targetPhysic)
        {
            var velocity = hoverboardControlData.hoverboardFinalVelocity;

            if (targetPhysic.velocity.magnitude >= velocity)
            {
                targetPhysic.velocity = targetPhysic.velocity.normalized * velocity;
            }
        }

        private void CutVelocityWhenStopped()
        {
            if(hoverboardPhysics.velocity.magnitude < 1) return;
            if (!IsPressedHold(KeyCode.W) && !IsPressedHold(KeyCode.A) && !IsPressedHold(KeyCode.S) && !IsPressedHold(KeyCode.D))
            {
                var targetVelocity = hoverboardPhysics.velocity;

                _velocityTween = DOVirtual.Float(targetVelocity.magnitude, 0f, velocityCutDuration,
                    value => { hoverboardPhysics.velocity = targetVelocity.normalized * value; });           
            }
        }

        #endregion
    }
}