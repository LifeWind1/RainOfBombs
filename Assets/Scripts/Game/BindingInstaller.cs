using Game.Spawn;
using MoDI;
using UnityEngine;

namespace Game
{
    [DefaultExecutionOrder(-100)]
    public class BindingInstaller : MonoBehaviour
    {
        [SerializeField] private SpawnArea m_spawnArea;
        [SerializeField] private WallCreator m_wallCreator;
        [SerializeField] private ObjectPoolerManager m_poolerManager;
        [SerializeField] private SpawnSystem m_spawnSystem;
        
        private void Awake()
        {
            Bind();
        }

        private void Bind()
        {
            var di = DI.Get();
            
            di.Bind<SpawnArea>().FromInstance(m_spawnArea);
            di.Bind<WallCreator>().FromInstance(m_wallCreator);
            di.Bind<ObjectPoolerManager>().FromInstance(m_poolerManager);
            di.Bind<SpawnSystem>().FromInstance(m_spawnSystem);
            
            DI.Manager.ApplyInject();
        }
    }
}