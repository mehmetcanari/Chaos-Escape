﻿using System;
using UnityEngine;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine.Serialization;

namespace Chaos.Escape
{
    public class CharacterMovement : Input
    {
        #region INSPECTOR FIELDS

        [FoldoutGroup("Character Movement Data")]
        public CharacterMovementData characterMovementData;
        [SerializeField] private Rigidbody characterPhysics;
        [SerializeField] private float velocityCutDuration;
        private Tween _velocityTween;

        [FoldoutGroup("Character Animation")]
        [SerializeField] private Animator animator;
        private static readonly int Turn = Animator.StringToHash("Turn");
        private static readonly int Forward = Animator.StringToHash("Forward");

        #endregion

        #region SHARED CLASSES

        [SerializeField] private PlayerAudioManager playerAudioManager;
 
        #endregion

        #region UNITY METHODS

        private void Start()
        {
            SetupAnimator();
        }

        private void LateUpdate()
        {
            FixVelocityStamp(characterPhysics);
            Movement(characterPhysics);
        }

        private void Update()
        {
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
            var characterAccelerationValue = characterMovementData.characterAccelerationValue;
            _velocityTween?.Kill();
            
            float horizontal = UnityEngine.Input.GetAxis("Horizontal");
            float vertical = UnityEngine.Input.GetAxis("Vertical");

            var targetTransform = transform;
            
            var targetVelocity = Vector3.forward * (vertical * characterAccelerationValue) +
                                 Vector3.right * (horizontal * characterAccelerationValue);
            
            targetPhysics.AddForce(targetVelocity, ForceMode.Acceleration);

            MovementAnimation(targetTransform, targetVelocity);
            playerAudioManager.PlayStepSound();
        }

        private void MovementAnimation(Transform targetTransform, Vector3 targetVelocity)
        {
            float transitionDuration = 0.2f;
            float angle = Vector3.SignedAngle(targetTransform.forward, targetVelocity, Vector3.up);
            if (angle > -45f && angle < 45f)
            {
                animator.SetFloat(Forward, 1f, transitionDuration, Time.deltaTime);
                animator.SetFloat(Turn, 0f , transitionDuration, Time.deltaTime);
            }
            else if (angle < -135f || angle > 135f)
            {
                animator.SetFloat(Forward, -1f , transitionDuration, Time.deltaTime);
                animator.SetFloat(Turn, 0f , transitionDuration, Time.deltaTime);
            }
            else if (angle > 0f)
            {
                animator.SetFloat(Forward, 1f , transitionDuration, Time.deltaTime);
                animator.SetFloat(Turn, 1f , transitionDuration, Time.deltaTime);
            }
            else
            {
                animator.SetFloat(Forward, 1f , transitionDuration, Time.deltaTime);
                animator.SetFloat(Turn, -1f , transitionDuration, Time.deltaTime);
            }
        }
        
        private bool HasAnyMovementKeyPressed()
        {
            return IsPressedHold(KeyCode.W) || IsPressedHold(KeyCode.A) || IsPressedHold(KeyCode.S) ||
                   IsPressedHold(KeyCode.D);
        }

        private void FixVelocityStamp(Rigidbody targetPhysic)
        {
            var velocity = characterMovementData.characterFinalVelocity;

            if (targetPhysic.velocity.magnitude >= velocity)
            {
                targetPhysic.velocity = targetPhysic.velocity.normalized * velocity;
            }
        }

        private void CutVelocityWhenStopped()
        {
            if (characterPhysics.velocity.magnitude < 1) return;
            if (!IsPressedHold(KeyCode.W) && !IsPressedHold(KeyCode.A) && !IsPressedHold(KeyCode.S) &&
                !IsPressedHold(KeyCode.D))
            {
                var targetVelocity = characterPhysics.velocity;
                
                _velocityTween = DOVirtual.Float(targetVelocity.magnitude, 0f, velocityCutDuration,
                    value => { characterPhysics.velocity = targetVelocity.normalized * value; });

                CutAnimationWhenStopped();
                playerAudioManager.StopStepSound();
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