using System;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Chaos.Escape
{
    public class BulletBehaviour : MonoBehaviour
    {
        #region UNITY METHODS

        private void OnCollisionEnter(Collision other)
        {
            GiveDamage(other);
        }

        #endregion

        #region PRIVATE METHODS

        private void GiveDamage(Collision other)
        {
            if (other.gameObject.TryGetComponent(out IDamageable damageable))
            {
                damageable.TakeDamage();
            }
        }

        #endregion
    }
}