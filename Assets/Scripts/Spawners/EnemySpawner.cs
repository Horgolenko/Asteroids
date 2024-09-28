using System;
using System.Collections;
using System.Collections.Generic;
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

        private int _enemiesToKill;
        private int _currentEnemies;
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
            _enemiesToKill = Settings.Data.game.enemiesToKill;
        }
        
        public void InitialSpawn()
        {
            StartCoroutine(SpawnCoroutine());
        }
        
        public void StopEnemySpawn()
        {
            StopAllCoroutines();
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
            yield return new WaitForSecondsRealtime(Settings.Data.enemy.spawn.initialSpawnDelay);

            while (_currentEnemies < Settings.Data.game.maxEnemies)
            {
                Spawn();
            }
        }

        private void Spawn()
        {
            _currentEnemies++;
            var enemy = _objectPool.GetObject(_asteroidPrefab);
            enemy.Init(GetSpawnPosition(), Settings.Data.enemy.GetEnemyData(), OnEnemyDestroy(enemy));
            _enemies.Add(enemy);
        }

        private Action OnEnemyDestroy(Asteroid enemy)
        {
            return () =>
            {
                _enemies.Remove(enemy);
                StartCoroutine(SpawnCoroutine(GetSpawnDelay()));
                _currentEnemies--;
                _enemiesToKill--;
            };
        }

        private IEnumerator SpawnCoroutine(WaitForSeconds waitForSeconds)
        {
            yield return waitForSeconds;
            
            Spawn();
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

        private WaitForSeconds GetSpawnDelay()
        {
            var spawnDelay = Settings.Data.enemy.spawn.spawnDelay - (Settings.Data.game.enemiesToKill - _enemiesToKill) *
                Settings.Data.enemy.spawn.spawnDelta;
            
            return new WaitForSeconds(spawnDelay);
        }
    }
}
