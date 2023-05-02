using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace Chaos.Escape
{
    public class Pistol : GunHandler
    {
        #region INSPECTOR FIELDS

        [FoldoutGroup("Pistol Settings")]
        public Transform muzzle;
        public GameObject bulletPrefab;
        public float bulletForce = 20f;
        [SerializeField] private float fireRate;
        
        [FoldoutGroup("Effects")]
        [SerializeField] private ParticleSystem muzzleFlash;
        [SerializeField] private AudioClip shotClip;
        [SerializeField] private AudioSource audioSource;
        private bool _isPlaying;
        
        [FormerlySerializedAs("gunItemsPool")]
        [FoldoutGroup("Pooling References")]
        [SerializeField] private BulletPool bulletPool;

        #endregion
        
        private float _fireInterval;

        #region UNITY METHODS

        private void Update()
        {
            _fireInterval += Time.deltaTime;
            
            Shoot();
        }

        #endregion


        #region PRIVATE METHODS

        private void Shoot()
        {
            if (!IsClicked() || !NextFireAllowed()) return;
            muzzleFlash.Play();
            PlayAudio();
            var bullet = bulletPool.GetBullet();
            bullet.Initiate();
            _fireInterval = 0f;
        }
        
        private bool NextFireAllowed()
        {
            if (_fireInterval >= fireRate)
            {
                return true;
            }
            return false;
        }
        
        private void PlayAudio()
        {
            audioSource.PlayOneShot(shotClip);
        }

        #endregion
    }
}