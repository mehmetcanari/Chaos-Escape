using UnityEngine;

namespace Chaos.Escape
{
    public class Pistol : GunHandler
    {
        #region INSPECTOR FIELDS

        [SerializeField] private Transform muzzle;
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private float bulletForce = 20f;
        [SerializeField] private float fireRate = 1f;

        #endregion
        
        private float _fireInterval;

        #region UNITY METHODS

        private void Update()
        {
            _fireInterval += Time.deltaTime;
            
            if (IsClicked() && NextFireAllowed())
            {
                Shoot();
                _fireInterval = 0f;
            }
        }

        #endregion


        #region PRIVATE METHODS

        private bool NextFireAllowed()
        {
            if (_fireInterval >= fireRate)
            {
                return true;
            }
            return false;
        }

        private void Shoot()
        {
            GameObject bullet = Instantiate(bulletPrefab, muzzle.position, muzzle.rotation);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            rb.AddForce(muzzle.forward * bulletForce, ForceMode.Impulse);
        }

        #endregion
        
    }
}