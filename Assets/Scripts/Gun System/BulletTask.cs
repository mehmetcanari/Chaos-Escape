using System;
using UnityEngine;

namespace Chaos.Escape
{
    public partial class BulletTask : MonoBehaviour
    {
        #region UNITY METHODS

        private void OnCollisionEnter(Collision other)
        {
            GiveDamage(other);
            DestroyBullet();
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
        
        private void DestroyBullet()
        {
            Destroy(gameObject);
        }
        
        private void SpawnParticleAtHitPoint(Collision other)
        {
            //Instantiate(particle, other.GetContact(0).point, Quaternion.identity);
        }

        #endregion
    }
}