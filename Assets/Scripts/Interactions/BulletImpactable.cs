using Chaos.Escape;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Interactions
{
    [RequireComponent( typeof( EntityHealth ))]
    public sealed class BulletImpactable : MonoBehaviour, IDamageable, IHealth
    {
        [FoldoutGroup("Bullet Data")]
        [SerializeField] private EntityHealth health;
        [SerializeField] private int impactDamage = 10;
        [SerializeField] private ParticleSystem hitParticle;
        
        public void TakeDamage()
        {
            if (!health.isDamageable) return;
            ReduceHealth(impactDamage);
        }

        public void ReduceHealth(int amount)
        {
            health.currentHealth -= amount;
        }
        
        private void OnCollisionEnter(Collision other)
        {
            if (!other.gameObject.TryGetComponent(out BulletBehaviour bullet)) return;
            if(hitParticle == null) return;
            
            var particle = new BulletParticle(hitParticle, other);
            particle.SpawnParticleAtHitPoint();
            DestroyBulletWithDelay(0.4f, other.gameObject);
        }
        
        private void DestroyBulletWithDelay(float delay, GameObject other)
        {
            GameObject o;
            
            (o = other).SetActive(false);
            Destroy(o, delay);
        }
    }

    internal class BulletParticle
    {
        private readonly ParticleSystem _hitParticle;
        private readonly Collision _other;
        
        public BulletParticle(ParticleSystem hitParticle, Collision other)
        {
            this._hitParticle = hitParticle;
            this._other = other;
        }
        
        public void SpawnParticleAtHitPoint()
        {
            Quaternion randomRotation = 
                Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));
            
            var particle = Object.Instantiate(_hitParticle, _other.GetContact(0).point, randomRotation);
         
            DOVirtual.DelayedCall(0.4f, () => Object.Destroy(particle.gameObject));
        }
    }
}