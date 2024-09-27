using UnityEngine;
using Utils.Level;
using Zenject;

namespace Spawners
{
    [RequireComponent(typeof(PlayerSpawner), typeof(EnemySpawner))]
    public class SpawnProvider : MonoBehaviour
    {
        [SerializeField] private PlayerSpawner _playerSpawner;
        [SerializeField] private EnemySpawner _enemySpawner;
        [HideInInspector]
        [SerializeField] private SpawnSpace[] _spawnSpaces;

        public void Init(DiContainer diContainer)
        {
            _playerSpawner.Init(this, diContainer);
            _enemySpawner.Init(this);
        }
        
        public void InitialSpawn()
        {
            SpawnPlayer();
            SpawnEnemies();
        }

        public void RespawnPlayer()
        {
            _playerSpawner.Respawn();
        }
        
        public Vector3 GetSpawnPosition()
        {
            var index = Random.Range(0, _spawnSpaces.Length);
            return _spawnSpaces[index].GetPosition();
        }
        
        public Vector3 GetRespawnPosition()
        {
            Vector3 result;
            while (true)
            {
                var index = Random.Range(0, _spawnSpaces.Length);
                result = _spawnSpaces[index].GetPosition();
                if (_enemySpawner.IsSpaceFree(result))
                {
                    return result;
                }
            }
        }
        
        private void SpawnPlayer()
        {
            _playerSpawner.Spawn();
        }

        private void SpawnEnemies()
        {
            _enemySpawner.InitialSpawn();
        }

#if UNITY_EDITOR
        [ContextMenu("GetSpawnSpaces")]
        private void GetSpawnSpaces()
        {
            _spawnSpaces = GetSpawnSpace<SpawnSpace>();
        }
        
        private T[] GetSpawnSpace<T>() where T : MonoBehaviour
        {
            return FindObjectsByType<T>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        }
#endif
    }
}
