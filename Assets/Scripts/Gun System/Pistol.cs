using UnityEngine;

namespace Chaos.Escape
{
    public class Pistol : GunHandler
    {
        #region INSPECTOR FIELDS

        [SerializeField] private Transform muzzle;
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private float bulletForce = 20f;
        [SerializeField] private float fireRate;
        [SerializeField] private ParticleSystem muzzleFlash;

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
            var bullet = new Bullet(bulletPrefab, muzzle, bulletForce);
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

        #endregion
    }

    internal class Bullet
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

            DestroyOverTime(1, bullet);
        }

        private void DestroyOverTime(float time, GameObject bulletClone)
        {
            Object.Destroy(bulletClone, time);
        }
    }
}