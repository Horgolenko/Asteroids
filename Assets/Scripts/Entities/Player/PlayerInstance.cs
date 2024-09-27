using Interfaces;
using PlayerController;
using Spawners;
using States;
using UnityEngine;
using Zenject;

namespace Entities.Player
{
    public class PlayerInstance : MonoBehaviour, IDamageable
    {
        private SpawnProvider _spawnProvider;
        private PlayerMover _playerMover;
        private StateMachine _stateMachine;
        
        public static PlayerInstance Instance;
        
        [Inject]
        private void Construct(StateMachine stateMachine, SpawnProvider spawnProvider)
        {
            _stateMachine = stateMachine;
            _spawnProvider = spawnProvider;
            transform.position = _spawnProvider.GetSpawnPosition();
        }
        
        private void Awake()
        {
            _playerMover = GetComponent<PlayerMover>();
            Instance = this;
        }
        
        public void Damage()
        {
            _playerMover.Stop();
            _stateMachine.SetState<RestartState>();
            gameObject.SetActive(false);
        }
        
        public void Respawn()
        {
            transform.position = _spawnProvider.GetRespawnPosition();
            gameObject.SetActive(true);
        }
    }
}
