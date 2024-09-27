using UnityEngine;
using Utils.Level;

namespace Spawners
{
    [RequireComponent( typeof(EnemySpawner))]
    public class SpawnProvider : MonoBehaviour
    {
        [SerializeField] private EnemySpawner _enemySpawner;
        [HideInInspector]
        [SerializeField] private SpawnSpace[] _spawnSpaces;

        public void Init()
        {
            _enemySpawner.Init(this);
        }
        
        public void InitialSpawn()
        {
            SpawnEnemies();
        }
        
        public Vector3 GetSpawnPosition()
        {
            var index = Random.Range(0, _spawnSpaces.Length);
            return _spawnSpaces[index].GetPosition();
        }
        
        public void StopEnemySpawn()
        {
            _enemySpawner.StopEnemySpawn();
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
