using System;
using Data.Loaders;
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
        private const float SpeedDegrade = 0.75f;
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

        public void Rotate(Vector3 mousePosition)
        {
            mousePosition = new Vector3(mousePosition.x, transform.position.y, mousePosition.z);
            transform.DOLookAt(mousePosition, 2);
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
                    _currentSpeedDelta += _currentSpeedDelta > 0 ? SpeedDelta : DoubleSpeedDelta;
                    _currentSpeedDelta = Math.Max(_currentSpeedDelta, DataLoader.GetPlayerData().maxAcceleration);
                    break;
                case MoveDirectionType.Backward:
                    _currentSpeedDelta -= _currentSpeedDelta < 0 ? SpeedDelta : DoubleSpeedDelta;
                    _currentSpeedDelta = Math.Sign(_currentSpeedDelta) * Math.Min(Math.Abs(_currentSpeedDelta), DataLoader.GetPlayerData().maxAcceleration);
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
            if (Math.Abs(_moveSpeed) > DataLoader.GetPlayerData().maxSpeed)
            {
                _moveSpeed = Math.Sign(_moveSpeed) * DataLoader.GetPlayerData().maxSpeed;
            }
            
            if (Math.Abs(_moveSpeed) - Math.Abs(_currentSpeedDelta) < 0)
            {
                _moveSpeed = 0;
                _currentSpeedDelta = 0;
                _coroutineLauncher.RemoveUpdate(_changeSpeedLine);
                _moving = false;
                _accelerating = false;
            }
        }
    }
}
