using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Chaos.Escape
{
    public sealed class EntityHealth : MonoBehaviour
    {
        #region INSPECTOR FIELDS

        private const int MaxHealth = 100;
        [SerializeField] private bool isDamageable;
        
        public int currentHealth = MaxHealth;

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
            if (currentHealth > 0) return;
            if (!isDamageable) return;
            Destroy(gameObject);
        }
        
        #endregion
    }
}