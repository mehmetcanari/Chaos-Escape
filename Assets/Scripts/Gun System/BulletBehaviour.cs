using System;
using DG.Tweening;
using UnityEngine;

namespace Chaos.Escape
{
    public class BulletBehaviour : MonoBehaviour
    {
        #region INSPECTOR FIELDS

        [SerializeField] private ParticleSystem hitParticle;

        #endregion
        
        #region UNITY METHODS

        private void OnCollisionEnter(Collision other)
        {
            SpawnParticleAtHitPoint(other);
            DestroyBulletWithDelay(0.5f);
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
        
        private void DestroyBulletWithDelay(float delay)
        {
            GameObject o;
            
            (o = gameObject).SetActive(false);
            Destroy(o, delay);
        }

        private void SpawnParticleAtHitPoint(Collision other)
        {
            var particle = Instantiate(hitParticle, other.GetContact(0).point, Quaternion.identity);
            
            DOVirtual.DelayedCall(0.4f, () => Destroy(particle.gameObject));
        }

        #endregion
    }
}