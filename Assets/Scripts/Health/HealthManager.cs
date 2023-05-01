using System;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Chaos.Escape
{
    public class HealthManager : MonoBehaviour
    {
        #region INSPECTOR FIELDS

        [FoldoutGroup("Health Data")]
        const float MaxHealth = 100f;
        [SerializeField] private float currentHealth = MaxHealth;
        [SerializeField] private Slider healthSlider;

        #endregion

        #region UNITY METHODS

        private void Awake()
        {
            InitilizeHealth();
        }

        private void Update()
        {
            if (UnityEngine.Input.GetKeyDown(KeyCode.P))
            {
                TakeDamage(10);
            }
        }

        #endregion

        #region PRIVATE METHODS

        private void InitilizeHealth()
        {
            currentHealth = MaxHealth;
            healthSlider.value = currentHealth;
        }
        
        private void TakeDamage(float damage)
        {
            currentHealth -= damage;
            
            DecreaseHealthFromSlider(damage);
        }

        private void DecreaseHealthFromSlider(float damage)
        {
            float targetValue = healthSlider.value - damage;
            DOTween.To(() => healthSlider.value, x => healthSlider.value = x, targetValue, 0.25f);
        }

        #endregion
    }
}