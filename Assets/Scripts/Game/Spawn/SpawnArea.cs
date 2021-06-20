using UnityEngine;

namespace Game.Spawn
{
    [RequireComponent(typeof(BoxCollider))]
    public class SpawnArea : MonoBehaviour
    {
        public BoxCollider BoxCollider
        {
            get
            {
                if (!m_boxCollider)
                {
                    m_boxCollider = GetComponent<BoxCollider>();
                }

                return m_boxCollider;
            }
        }
        
        private BoxCollider m_boxCollider;
    }
}