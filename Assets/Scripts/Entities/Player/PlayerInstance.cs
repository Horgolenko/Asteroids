using Animation;
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
        private StateMachine _stateMachine;
        private SpawnProvider _spawnProvider;
        private PlayerMover _playerMover;
        private PlayerDissolve _playerDissolve;
        private MeshCollider _meshCollider;
        
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
            _playerDissolve = GetComponent<PlayerDissolve>();
            _meshCollider = GetComponent<MeshCollider>();
            Instance = this;
        }
        
        public void Damage()
        {
            _meshCollider.enabled = false;
            _playerMover.Stop();
            _stateMachine.SetState<RestartState>();
            _playerDissolve.Animate(() => gameObject.SetActive(false));
        }
        
        public void Respawn()
        {
            transform.position = _spawnProvider.GetRespawnPosition();
            _playerDissolve.ResetAnimation();
            _meshCollider.enabled = true;
            gameObject.SetActive(true);
        }
    }
}
