using System;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace Chaos.Escape
{
    public sealed class EntityHealth : MonoBehaviour
    {
        #region INSPECTOR FIELDS

        [FoldoutGroup("Health Data")]
        private const int MaxHealth = 100;
        public bool isDamageable;
        public int currentHealth = MaxHealth;
        private bool _doOnce;
        
        [FormerlySerializedAs("_entityRagdollHandler")]
        [FoldoutGroup("Ragdoll Reference")]
        [SerializeField] private EntityRagdollHandler entityRagdollHandler;

        #endregion

        #region PUBLIC PROPERTIES

        public bool IsDead => currentHealth <= 0;

        #endregion

        #region UNITY METHODS

        private void Update()
        {
            EntityDestroy();
        }

        #endregion
        
        #region PUBLIC METHODS

        private void EntityDestroy()
        {
            if (!IsDead) return;
            if (!_doOnce)
            {
                entityRagdollHandler.EnableRagdollComponents();
                DOVirtual.DelayedCall(5f, () => Destroy(gameObject)).SetLink(gameObject);
                _doOnce = true;
            }
        }
        
        #endregion
    }
}