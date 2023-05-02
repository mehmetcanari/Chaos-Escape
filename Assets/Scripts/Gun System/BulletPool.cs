using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Sirenix.OdinInspector;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Chaos.Escape
{
    public sealed class BulletPool : MonoBehaviour
    {
        #region INSPECTOR FIELDS
        
        [FoldoutGroup("Pool Settings")]
        [SerializeField] private Pistol pistol;
        [SerializeField] private int bulletAmount;
        private Queue<Bullet> _bullets;

        #endregion
        
        #region UNITY METHODS

        private void Awake()
        {
            _bullets = new Queue<Bullet>();
            InitializeBullets();
        }

        #endregion

        #region PRIVATE METHODS
        
        private void InitializeBullets()
        {
            for (var i = 0; i < bulletAmount; i++)
            {
                var bullet = new Bullet(pistol.bulletPrefab, pistol.muzzle, pistol.bulletForce);
                _bullets.Enqueue(bullet);
            }
        }

        #endregion

        #region PUBLIC METHODS

        public Bullet GetBullet()
        {
            var bullet = _bullets.Dequeue();
            _bullets.Enqueue(bullet);
            return bullet;
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