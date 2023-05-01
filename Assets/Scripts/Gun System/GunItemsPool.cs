using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Chaos.Escape
{
    public sealed class GunItemsPool : MonoBehaviour
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
            Debug.Log(_bullets.Count);
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
}