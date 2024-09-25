using System.Collections;
using System.Collections.Generic;
using Data.Loaders;
using Entities.Enemy;
using UnityEngine;
using Utils.Level;
using Utils.ObjectPool;
using Zenject;

namespace Spawners
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private Asteroid _asteroidPrefab;
        [HideInInspector]
        [SerializeField] private SpawnSpace[] _spawnSpaces;

        private ushort _enemiesAmount;
        private ObjectPool _objectPool;
        private readonly List<Asteroid> _enemies = new();

        [Inject]
        private void Construct(ObjectPool objectPool)
        {
            _objectPool = objectPool;
        }
        
        public void InitialSpawn()
        {
            StartCoroutine(SpawnCoroutine());
        }
        
        private IEnumerator SpawnCoroutine()
        {
            yield return new WaitForSecondsRealtime(DataLoader.GetSpawnDelay());

            while (_enemiesAmount < DataLoader.GetMaxEnemies())
            {
                Spawn();
                _enemiesAmount++;
            }
        }

        private void Spawn()
        {
            var enemy = _objectPool.GetObject(_asteroidPrefab);
            enemy.Init(GetSpawnPosition(), DataLoader.GetEnemyData());
            _enemies.Add(enemy);
        }

        private Vector3 GetSpawnPosition()
        {
            var index = Random.Range(0, _spawnSpaces.Length);
            Vector3 result;
            while (true)
            {
                result = _spawnSpaces[index].GetPosition();
                bool isUnique = true;
                for (int i = 0; i < _enemies.Count; i++)
                {
                    if (SpaceUtil.IsOverlap(result, _enemies[i].transform.position))
                    {
                        isUnique = false;
                        break;
                    }
                }
                
                if (isUnique) break;
            }
            
            return result;
        }
        
#if UNITY_EDITOR
        [ContextMenu("GetSpawnSpaces")]
        private void GetAllBuildings()
        {
            _spawnSpaces = GetBuildings<SpawnSpace>();
        }
        
        private T[] GetBuildings<T>() where T : MonoBehaviour
        {
            return FindObjectsByType<T>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        }
#endif
    }
}
