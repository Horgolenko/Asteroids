using Game;
using PlayerController.InputVariants;
using States;
using UnityEngine;
using Zenject;

namespace PlayerController
{
    public class InputController : MonoBehaviour
    {
        private Camera _camera;
        private PlayerMover _playerMover;
        private PlayerShooter _playerShooter;
        private GameProvider _gameProvider;
        private StateMachine _stateMachine;
        private PlayerInput _playerInput;
        private Vector3 _lastMousePosition;

        [Inject]
        private void Construct(GameProvider gameProvider, StateMachine stateMachine, PlayerInput playerInput)
        {
            _gameProvider = gameProvider;
            _stateMachine = stateMachine;
            _playerInput = playerInput;
        }
        
        private void Awake()
        {
            _camera = Camera.main;
            _playerMover = GetComponent<PlayerMover>();
            _playerShooter = GetComponent<PlayerShooter>();
        }

#if UNITY_EDITOR
        private void Start()
        {
            _lastMousePosition = _playerInput.MousePosition();
        }
#endif

        private void Update()
        {
            if (_stateMachine.currentState is not GameplayState) return;

            Move();
            Rotate();
            Shoot();
        }

        private void Move()
        {
            _playerMover.Move(_playerInput.Vertical());
        }

        private void Rotate()
        {
#if UNITY_EDITOR
            if (_playerInput.MousePosition() != _lastMousePosition)
            {
                _lastMousePosition = _playerInput.MousePosition();
                RotateByMouse();
            }
            else
            {
                RotateByButtons();
            } 
#else
            RotateByButtons();
#endif
        }
        
#if UNITY_EDITOR
        private void RotateByMouse()
        {
            var mousePosition = _camera.ScreenToWorldPoint(_playerInput.MousePosition());
            _playerMover.Rotate(mousePosition);
        }
#endif
        
        private void RotateByButtons()
        {
            _playerMover.Rotate(_playerInput.Horizontal());
        }

        private void Shoot()
        {
            if (_playerInput.Shoot())
            {
                if (_gameProvider.CanFire())
                {
                    _playerShooter.Fire();
                }
            }
        }
    }
}
