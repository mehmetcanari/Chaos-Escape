using System;
using UnityEngine;

namespace Chaos.Escape
{
    public partial class BulletTask : MonoBehaviour
    {
        #region INSPECTOR FIELDS

        [SerializeField] private ParticleSystem hitParticle;

        #endregion
        
        #region UNITY METHODS

        private void OnCollisionEnter(Collision other)
        {
            SpawnParticleAtHitPoint(other);
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
            var bulletParticle = Instantiate(hitParticle, other.GetContact(0).point, Quaternion.identity);
            Destroy(bulletParticle, 1f);
        }

        #endregion
    }
}