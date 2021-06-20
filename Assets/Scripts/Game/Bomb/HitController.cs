using System;
using UnityEngine;

namespace Game.Bomb
{
    [RequireComponent(typeof(Collider))]
    public class HitController : MonoBehaviour
    {
        public event EventHandler Hit;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.isTrigger)
            {
                return;
            }
            
            Hit?.Invoke(this, EventArgs.Empty);
            Hit = null;
        }
    }
}