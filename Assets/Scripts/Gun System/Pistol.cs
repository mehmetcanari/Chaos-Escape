using UnityEngine;

namespace Chaos.Escape
{
    public class Pistol : GunHandler
    {
        #region INSPECTOR FIELDS

        [Header("Pistol Settings")]
        public Transform muzzle;
        public GameObject bulletPrefab;
        public float bulletForce = 20f;
        [SerializeField] private float fireRate;
        
        [Header("Effects")]
        [SerializeField] private ParticleSystem muzzleFlash;
        [SerializeField] private AudioClip shotClip;
        [SerializeField] private AudioSource audioSource;
        private bool _isPlaying;
        
        [Header("Pooling References")]
        [SerializeField] private GunItemsPool gunItemsPool;

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
            var bullet = gunItemsPool.GetBullet();
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
    
    public class Bullet
    {
        private readonly GameObject _bulletPrefab;
        private readonly Transform _muzzle;
        private readonly float _bulletForce;

        public Bullet (GameObject bulletPrefab, Transform muzzle, float bulletForce)
        {
            _bulletPrefab = bulletPrefab;
            _muzzle = muzzle;
            _bulletForce = bulletForce;
        }
        
        public void Initiate()
        {
            GameObject bullet = Object.Instantiate(_bulletPrefab, _muzzle.position, _muzzle.rotation);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            rb.AddForce(_muzzle.forward * _bulletForce, ForceMode.Impulse);
            
            DestroyOverTime(1f, bullet);
        }

        private void DestroyOverTime(float time, GameObject bulletClone)
        {
            Object.Destroy(bulletClone, time);
        }
    }
}