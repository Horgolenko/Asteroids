using System.Collections;
using System.Collections.Generic;
using Data.Loaders;
using Entities;
using Entities.Enemy;
using Entities.Player;
using UnityEngine;
using Utils.Level;
using Utils.ObjectPool;
using Zenject;

namespace Spawners
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private Asteroid _asteroidPrefab;

        private ushort _enemiesAmount;
        private PlayerInstance _player;
        private ObjectPool _objectPool;
        private SpawnProvider _spawnProvider;
        private readonly List<Asteroid> _enemies = new();

        [Inject]
        private void Construct(ObjectPool objectPool, PlayerInstance player)
        {
            _objectPool = objectPool;
            _player = player;
        }

        public void Init(SpawnProvider spawnProvider)
        {
            _spawnProvider = spawnProvider;
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
            Vector3 result;
            while (true)
            {
                result = _spawnProvider.GetSpawnPosition();
                bool isUnique = true;
                for (int i = 0; i < _enemies.Count; i++)
                {
                    if (SpaceUtil.IsEnemyToEnemyOverlap(result, _enemies[i].transform.position) ||
                        SpaceUtil.IsPlayerToEnemyOverlap(result, _player.transform.position))
                    {
                        isUnique = false;
                        break;
                    }
                }
                
                if (isUnique) break;
            }
            
            return result;
        }

        public bool IsSpaceFree(Vector3 position)
        {
            for (int i = 0; i < _enemies.Count; i++)
            {
                if (SpaceUtil.IsPlayerToEnemyOverlap(position, _enemies[i].transform.position))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
