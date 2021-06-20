using System;
using UnityEngine;

namespace Game
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float m_health = 100;
        [SerializeField] private float m_currentHealth;

        public event EventHandler<GameObject> Death;
        
        private void OnEnable()
        {
            m_currentHealth = m_health;
        }

        public void Hit(float damage)
        {
            m_currentHealth -= damage;

            if (m_currentHealth <= 0)
            {
                Death?.Invoke(this, gameObject);
            }
        }
    }
}