using Extensions;
using MoDI;
using UnityEngine;

namespace Game.Spawn
{
    public class SpawnSystem : DIMonoBehaviour
    {
        [Inject]
        private SpawnArea m_spawnArea;

        [Inject] 
        private ObjectPoolerManager m_poolManager;

        private Bounds m_spawnAreaBounds;

        private void Awake()
        {
            m_spawnAreaBounds = m_spawnArea.BoxCollider.bounds;
        }

        public GameObject Spawn(GameObject objectPrefab, float yPosition)
        {
            GameObject currentObject = m_poolManager.GetObject(objectPrefab, m_spawnArea.transform);
            
            currentObject.transform.position =
                GeometryExtension.RandomPointInBox(m_spawnAreaBounds.center, m_spawnAreaBounds.size, yPosition);

            return currentObject;
        }

        public void ReturnObject(GameObject gameObject)
        {
            m_poolManager.ReturnObject(gameObject);
        }
    }
}