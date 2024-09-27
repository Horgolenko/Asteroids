using Entities.Player;
using States;
using UnityEngine;
using Zenject;

namespace PlayerController
{
    public class PlayerCamera : MonoBehaviour
    {
        public Vector3 Offset;
        
        [SerializeField] private float _movementSpeed;
        [SerializeField] private bool _followPositionX;
        [SerializeField] private bool _followPositionY;
        [SerializeField] private bool _followPositionZ;

        private Transform _mainTransform;
        private PlayerInstance _player;

        [Inject]
        private void Construct(PlayerInstance player)
        {
            _player = player;
            _mainTransform = transform;
        }

        private void Start()
        {
            StateMachine.StateChanged += OnStateChanged;
        }

        private void OnDestroy()
        {
            StateMachine.StateChanged -= OnStateChanged;
        }

        private void FixedUpdate()
        {
            if (_player != null)
            {
                UpdateCameraPosition(_player.transform.position, _movementSpeed * Time.fixedDeltaTime);
            }
        }

        private void Reposition()
        {
            UpdateCameraPosition(_player.transform.position, float.MaxValue);
        }
        
        private void UpdateCameraPosition(Vector3 position, float maxDelta)
        {
            //Updates position depending on axes to be followed
            var positionX = _followPositionX ? position.x : _mainTransform.position.x;
            var positionY = _followPositionY ? position.y : _mainTransform.position.y;
            var positionZ = _followPositionZ ? position.z : _mainTransform.position.z;
            var targetPosition = new Vector3(positionX, positionY, positionZ + Offset.z);
            
            _mainTransform.position = Vector3.MoveTowards(_mainTransform.position, targetPosition, maxDelta);
        }

        private void OnStateChanged(AState state)
        {
            if (state is GameplayState)
            {
                Reposition();
            }
        }
    }
}