using Interfaces;
using PlayerController;
using States;
using UnityEngine;
using Zenject;

namespace Entities.Player
{
    public class PlayerInstance : MonoBehaviour, IDamageable
    {
        private PlayerMover _playerMover;
        private StateMachine _stateMachine;
        
        [Inject]
        private void Construct(StateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }
        
        private void Awake()
        {
            _playerMover = GetComponent<PlayerMover>();
        }
        
        public void Damage()
        {
            _playerMover.Stop();
            _stateMachine.SetState<RestartState>();
            gameObject.SetActive(false);
        }
    }
}
