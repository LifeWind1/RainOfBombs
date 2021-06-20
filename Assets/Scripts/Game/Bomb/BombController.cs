using System;
using UnityEngine;

namespace Game.Bomb
{
    public class BombController : MonoBehaviour
    {
        [SerializeField] private DamageController m_damageController;
        [SerializeField] private HitController m_hitController;
        [SerializeField] private float m_damage;

        public event EventHandler<GameObject> BombFelt;

        private void Start()
        {
            m_damageController.Damage += OnDamage;
            m_hitController.Hit += OnHit;
        }

        private void OnHit(object sender, EventArgs e)
        {
            BombFelt?.Invoke(this, gameObject);
            BombFelt = null;
        }

        private void OnDamage(object sender, PlayerController playerController)
        {
            playerController.Hit(m_damage);
        }
    }
}