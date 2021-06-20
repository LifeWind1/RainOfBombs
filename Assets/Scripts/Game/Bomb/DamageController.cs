using System;
using UnityEngine;

namespace Game.Bomb
{
    [RequireComponent(typeof(Collider))]
    public class DamageController : MonoBehaviour
    {
        public event EventHandler<PlayerController> Damage;
        
        private void OnTriggerEnter(Collider other)
        {
            PlayerController playerController = other.GetComponent<PlayerController>();
            
            if (playerController != null)
            {
                Damage?.Invoke(this, playerController);
            }
        }
    }
}