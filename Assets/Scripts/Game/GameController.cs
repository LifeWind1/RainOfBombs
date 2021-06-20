using System.Collections;
using Game.Bomb;
using Game.Spawn;
using MoDI;
using UnityEngine;

namespace Game
{
    public class GameController : DIMonoBehaviour
    {
        [SerializeField] private PlayerController m_playerPrefab;
        [SerializeField] private BombController m_bombPrefab;
        
        [Inject]
        private WallCreator m_wallCreator;

        [Inject]
        private SpawnSystem m_spawnSystem;

        private void Start()
        {
            m_wallCreator.CreateWalls();
            StartCoroutine(CreatePlayers());
            StartCoroutine(CreateBombs());
        }

        private IEnumerator CreatePlayers()
        {
            var waitForSeconds = new WaitForSeconds(8);
            float yPosition = m_playerPrefab.transform.position.y;
            
            while (true)
            {
                var player = m_spawnSystem.Spawn(m_playerPrefab.gameObject, yPosition).GetComponent<PlayerController>();
                player.Death += OnObjectDeath;
                yield return waitForSeconds;
            }
        }
        
        private IEnumerator CreateBombs()
        {
            float yPosition = 15;
            
            while (true)
            {
                var bomb = m_spawnSystem.Spawn(m_bombPrefab.gameObject, yPosition).GetComponent<BombController>();
                bomb.BombFelt += OnObjectDeath;
                yield return new WaitForSeconds(Random.Range(1, 5));
            }
        }
        
        private void OnObjectDeath(object sender, GameObject e)
        {
            m_spawnSystem.ReturnObject(e);
        }
    }
}