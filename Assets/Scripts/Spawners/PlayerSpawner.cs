using Entities.Player;
using UnityEngine;
using Zenject;

namespace Spawners
{
    public class PlayerSpawner : MonoBehaviour
    {
        [SerializeField] private PlayerInstance _playerPrefab;

        private SpawnProvider _spawnProvider;
        private DiContainer _diContainer;
        private GameObject _player;

        public void Init(SpawnProvider spawnProvider, DiContainer diContainer)
        {
            _spawnProvider = spawnProvider;
            _diContainer = diContainer;
        }
        
        public void Spawn()
        {
            _player = _diContainer.InstantiatePrefab(_playerPrefab);
            _player.transform.position = _spawnProvider.GetSpawnPosition();
        }

        public void Respawn()
        {
            _player.transform.position = _spawnProvider.GetRespawnPosition();
            _player.gameObject.SetActive(true);
        }
    }
}
