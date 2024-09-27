using System;
using System.Collections;
using System.Collections.Generic;
using Data.Loaders;
using Entities.Enemy;
using Entities.Player;
using UnityEngine;
using Utils.CoroutineUtils;
using Utils.Level;
using Utils.ObjectPool;
using Zenject;

namespace Spawners
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private Asteroid _asteroidPrefab;

        private int _enemiesToKill;
        private int _currentEnemies;
        private PlayerInstance _player;
        private ObjectPool _objectPool;
        private SpawnProvider _spawnProvider;
        private CoroutineLauncher _coroutineLauncher;
        private readonly List<Asteroid> _enemies = new();
        private readonly Stack<UpdateLine> _enemiesToSpawnLine = new();

        [Inject]
        private void Construct(ObjectPool objectPool, PlayerInstance player, CoroutineLauncher coroutineLauncher)
        {
            _objectPool = objectPool;
            _player = player;
            _coroutineLauncher = coroutineLauncher;
        }

        public void Init(SpawnProvider spawnProvider)
        {
            _spawnProvider = spawnProvider;
            _enemiesToKill = DataLoader.GetPlayerData().enemiesToKill;
        }
        
        public void InitialSpawn()
        {
            StartCoroutine(SpawnCoroutine());
        }
        
        public void StopEnemySpawn()
        {
            while (_enemiesToSpawnLine.Count > 0)
            {
                var spawnLine = _enemiesToSpawnLine.Pop();
                _coroutineLauncher.RemoveUpdate(spawnLine);
            }
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
        
        private IEnumerator SpawnCoroutine()
        {
            yield return new WaitForSecondsRealtime(DataLoader.GetInitialSpawnDelay());

            while (_currentEnemies < DataLoader.GetMaxEnemies())
            {
                Spawn();
            }
        }

        private void Spawn()
        {
            _currentEnemies++;
            var enemy = _objectPool.GetObject(_asteroidPrefab);
            enemy.Init(GetSpawnPosition(), DataLoader.GetEnemyData(), OnEnemyDestroy(enemy));
            _enemies.Add(enemy);
        }

        private Action OnEnemyDestroy(Asteroid enemy)
        {
            return () =>
            {
                _enemies.Remove(enemy);
                var t = GetSpawnDelay();
                Debug.Log($"t = {t}");
                var spawnLine = new UpdateLine(SpawnLater, t);
                _enemiesToSpawnLine.Push(spawnLine);
                _coroutineLauncher.AddUpdate(spawnLine);
                _currentEnemies--;
                _enemiesToKill--;
            };
        }

        private void SpawnLater()
        {
            Spawn();
            if (_enemiesToSpawnLine.Count > 0)
            {
                var spawnLine = _enemiesToSpawnLine.Pop();
                _coroutineLauncher.RemoveUpdate(spawnLine);
            }
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

        private long GetSpawnDelay()
        {
            return (long)((DataLoader.GetSpawnDelay() - (DataLoader.GetPlayerData().enemiesToKill - _enemiesToKill) * DataLoader.GetSpawnDelta()) * 1000L);
        }
    }
}
