using System;
using UnityEngine;

namespace Chaos.Escape
{
    public class EnemyAnimationController : MonoBehaviour, IEnemyAnimationHandler
    {
        #region INSPECTOR FIELDS

        public Animator _animator;

        #endregion

        #region UNITY METHODS

        private void Awake()
        {
            InitializeAnimator();
        }

        private void InitializeAnimator()
        {
            _animator = GetComponent<Animator>();
        }

        #endregion
        
        #region PUBLIC METHODS

        public void SetAnimationState(string animationState)
        {
            _animator.SetTrigger(animationState);
        }

        #endregion
    }
}