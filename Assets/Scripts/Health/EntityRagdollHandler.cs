using System;
using UnityEngine;
using System.Linq;
using Sirenix.OdinInspector;

namespace Chaos.Escape
{
    public class EntityRagdollHandler : MonoBehaviour
    {
        #region INSPECTOR FIELDS

        private Rigidbody[] _rigidbodies;
        private Collider[] _colliders;
        private Animator _animator;
        private Collider _mainCollider;

        #endregion
        
        #region PRIVATE PROPERTIES

        private Rigidbody[] GetAllRigidbodiesInChildren => GetComponentsInChildren<Rigidbody>();
        private Collider[] GetAllCollidersInChildren => GetComponentsInChildren<Collider>();
        private Animator GetAnimatorInChild => GetComponentInChildren<Animator>();
        private Collider GetMainCollider => transform.parent.GetComponent<Collider>();

        #endregion
        
        #region UNITY METHODS

        private void OnEnable()
        {
            InitiliazeRagdoll();
            DisableRagdollComponents();
        }

        #endregion

        #region PRIVATE METHODS

        protected void InitiliazeRagdoll()
        {
            _rigidbodies = GetAllRigidbodiesInChildren;
            _colliders = GetAllCollidersInChildren;
            _animator = GetAnimatorInChild;
            _mainCollider = GetMainCollider;
        }
        
        [Button]
        public void DisableRagdollComponents()
        {
            foreach (var rigidbody in _rigidbodies)
            {
                rigidbody.isKinematic = true;
            }
            
            foreach (var collider in _colliders)
            {
                collider.enabled = false;
            }
            
            _animator.enabled = true;
            _mainCollider.enabled = true;
        }
        
        [Button]
        public void EnableRagdollComponents()
        {
            foreach (var rigidbody in _rigidbodies)
            {
                rigidbody.isKinematic = false;
            }
            
            foreach (var collider in _colliders)
            {
                collider.enabled = true;
            }
            
            _animator.enabled = false;
            _mainCollider.enabled = false;
        }

        #endregion
    }
}