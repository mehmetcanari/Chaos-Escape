﻿using System;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Serialization;

namespace Chaos.Escape
{
    public class CharacterMovement : Input
    {
        #region INSPECTOR FIELDS

        public HoverboardControlData hoverboardControlData;
        [SerializeField] private Rigidbody hoverboardPhysics;

        [SerializeField] private float velocityCutDuration;
        private Tween _velocityTween;

        [SerializeField] private Animator animator;
        private static readonly int Turn = Animator.StringToHash("Turn");
        private static readonly int Forward = Animator.StringToHash("Forward");

        #endregion

        #region UNITY METHODS

        private void Start()
        {
            SetupAnimator();
        }

        private void FixedUpdate()
        {
            FixVelocityStamp(hoverboardPhysics);
            Movement(hoverboardPhysics);
            CutVelocityWhenStopped();
        }

        #endregion

        #region PRIVATE METHODS

        private void SetupAnimator()
        {
            foreach (var childAnimator in GetComponentsInChildren<Animator>())
            {
                if (childAnimator != animator)
                {
                    animator.avatar = childAnimator.avatar;
                    Destroy(childAnimator);
                    break;
                }
            }
        }
        
        private void Movement(Rigidbody targetPhysics)
        {
            if (!HasAnyMovementKeyPressed()) return;
            var hoverboardAccelerationValue = hoverboardControlData.hoverboardAccelerationValue;
            _velocityTween?.Kill();
            
            float horizontal = UnityEngine.Input.GetAxis("Horizontal");
            float vertical = UnityEngine.Input.GetAxis("Vertical");

            var targetTransform = transform;
            
            var targetVelocity = Vector3.forward * (vertical * hoverboardAccelerationValue) +
                                 Vector3.right * (horizontal * hoverboardAccelerationValue);
            
            targetPhysics.AddForce(targetVelocity, ForceMode.Acceleration);
            
            MovementAnimation(targetTransform, targetVelocity);
        }

        private void MovementAnimation(Transform targetTransform, Vector3 targetVelocity)
        {
            if (Vector3.Angle(targetTransform.forward, targetVelocity) < 90f)
            {
                animator.SetFloat(Forward, 1f);
            }
            else
            {
                animator.SetFloat(Forward, -1f);
            }
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
            if (hoverboardPhysics.velocity.magnitude < 1) return;
            if (!IsPressedHold(KeyCode.W) && !IsPressedHold(KeyCode.A) && !IsPressedHold(KeyCode.S) &&
                !IsPressedHold(KeyCode.D))
            {
                var targetVelocity = hoverboardPhysics.velocity;
                
                _velocityTween = DOVirtual.Float(targetVelocity.magnitude, 0f, velocityCutDuration,
                    value => { hoverboardPhysics.velocity = targetVelocity.normalized * value; });

                CutAnimationWhenStopped();
            }
        }
        
        private void CutAnimationWhenStopped()
        {
            DOVirtual.Float(1f, 0f, velocityCutDuration, value => { animator.SetFloat(Forward, value); });
            DOVirtual.Float(1f, 0f, velocityCutDuration, value => { animator.SetFloat(Turn, value); });
        }

        #endregion
    }
}