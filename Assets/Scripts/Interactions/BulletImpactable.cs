using Chaos.Escape;
using UnityEngine;

namespace Interactions
{
    [RequireComponent( typeof( EntityHealth ))]
    public sealed class BulletImpactable : MonoBehaviour, IDamageable, IHealth
    {
        [SerializeField] private EntityHealth health;
        [SerializeField] private int impactDamage = 10;
        
        public void TakeDamage()
        {
            if (!health.isDamageable) return;
            ReduceHealth(impactDamage);
        }

        public void ReduceHealth(int amount)
        {
            health.currentHealth -= amount;
        }
    }
}