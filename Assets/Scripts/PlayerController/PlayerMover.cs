using System;
using DG.Tweening;
using Enums;
using UnityEngine;
using Utils;
using Utils.CoroutineUtils;
using Zenject;

namespace PlayerController
{
    public class PlayerMover : MonoBehaviour
    {
        private const float RotateDuration = 1;
        private const float SpeedDegrade = 0.9f;
        private const float SpeedDelta = 25f;
        private const float DoubleSpeedDelta = 2 * SpeedDelta;

        private bool _moving;
        private bool _accelerating;
        private float _currentSpeedDelta;
        private float _moveSpeed;
        private MoveDirectionType _moveDirectionType = MoveDirectionType.None;
        private Rigidbody _rigidbody;
        private UpdateLine _changeSpeedLine;
        private CoroutineLauncher _coroutineLauncher;
        private Tweener _tweener;
        
        [Inject]
        private void Construct(CoroutineLauncher coroutineLauncher)
        {
            _coroutineLauncher = coroutineLauncher;
        }
        
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            _changeSpeedLine = new UpdateLine(ChangeSpeed, TimeUtils.SpeedChangeDelta);
        }

        private void FixedUpdate()
        {
            _rigidbody.velocity = transform.forward * (_moveSpeed * Time.fixedDeltaTime);
            _rigidbody.angularVelocity = Vector3.zero;
        }
        
        public void Move(float value)
        {
            _moveDirectionType = value switch
            {
                > 0 => MoveDirectionType.Forward,
                < 0 => MoveDirectionType.Backward,
                _ => MoveDirectionType.None
            };

            if (!_moving && value != 0)
            {
                _moving = true;
                _accelerating = true;
                _coroutineLauncher.AddUpdate(_changeSpeedLine);
            }
        }

        private Vector3 _mousePosition;
        public void Rotate(Vector3 mousePosition)
        {
            mousePosition = new Vector3(mousePosition.x, transform.position.y, mousePosition.z);
            
            if (_mousePosition == mousePosition) return;
            
            _mousePosition = mousePosition;
            _tweener?.Kill();
            _tweener = transform.DOLookAt(_mousePosition, RotateDuration);
        }

        private readonly float _rotationSpeed = 100;
        public void Rotate(float value)
        {
            if (Mathf.Abs(value) < 0.01f) return;

            transform.Rotate(0, Mathf.Sign(value) * _rotationSpeed * Time.deltaTime, 0);
        }
        
        public void Stop()
        {
            _moveSpeed = 0;
            _currentSpeedDelta = 0;
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.angularVelocity = Vector3.zero;
            _coroutineLauncher.RemoveUpdate(_changeSpeedLine);
            _moving = false;
            _accelerating = false;
        }
        
        private void ChangeSpeed()
        {
            CalculateCurrentAcceleration();
            CalculateCurrentSpeed();
        }

        private void CalculateCurrentAcceleration()
        {
            switch (_moveDirectionType)
            {
                case MoveDirectionType.Forward:
                    _accelerating = true;
                    _currentSpeedDelta += _currentSpeedDelta > 0 ? SpeedDelta : DoubleSpeedDelta;
                    _currentSpeedDelta = Math.Min(_currentSpeedDelta, Settings.Data.player.movement.maxAcceleration);
                    break;
                case MoveDirectionType.Backward:
                    _accelerating = true;
                    _currentSpeedDelta -= _currentSpeedDelta < 0 ? SpeedDelta : DoubleSpeedDelta;
                    _currentSpeedDelta = Math.Sign(_currentSpeedDelta) * Math.Min(Math.Abs(_currentSpeedDelta), Settings.Data.player.movement.maxAcceleration);
                    break;
                case MoveDirectionType.None:
                {
                    if (_accelerating && _moving)
                    {
                        _accelerating = false;
                        _currentSpeedDelta = -_currentSpeedDelta;
                    }
                    else if (_moving && !_accelerating)
                    {
                        _currentSpeedDelta *= SpeedDegrade;
                        _currentSpeedDelta = Math.Sign(_currentSpeedDelta) * Math.Max(Math.Abs(_currentSpeedDelta), SpeedDelta);
                    }
                    
                    break;
                }
            }
        }
        
        private void CalculateCurrentSpeed()
        {
            _moveSpeed += _currentSpeedDelta;
            if (Math.Abs(_moveSpeed) > Settings.Data.player.movement.maxSpeed)
            {
                _moveSpeed = Math.Sign(_moveSpeed) * Settings.Data.player.movement.maxSpeed;
            }
            
            if (Math.Abs(_moveSpeed) - Math.Abs(_currentSpeedDelta) < 0)
            {
                Stop();
            }
        }
    }
}
