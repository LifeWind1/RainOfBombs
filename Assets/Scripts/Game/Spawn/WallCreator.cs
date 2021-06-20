using Extensions;
using MoDI;
using UnityEngine;

namespace Game.Spawn
{
    public class WallCreator : DIMonoBehaviour 
    {
        [SerializeField] private GameObject m_wallOnePrefab;
        [SerializeField] private GameObject m_wallTwoPrefab;

        [Inject]
        private SpawnArea m_spawnArea;
        
        public void CreateWalls()
        {
            int wallsCount = Random.Range(5, 11);

            Bounds spawnAreaBounds = m_spawnArea.BoxCollider.bounds;
                
            for (int i = 0; i <= wallsCount; i++)
            {
                var wall = Instantiate(Random.Range(0, 2) == 0 ? (m_wallOnePrefab) : (m_wallTwoPrefab));
                wall.transform.position =
                    GeometryExtension.RandomPointInBox(spawnAreaBounds.center, spawnAreaBounds.size, wall.transform.position.y);
            }
        }
    }
}