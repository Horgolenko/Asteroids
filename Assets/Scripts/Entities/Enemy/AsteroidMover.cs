using Entities.Abstract;
using UnityEngine;
using Utils.CoroutineUtils;
using Utils.Level;
using Zenject;

namespace Entities.Enemy
{
    public class AsteroidMover : AMover
    {
        private long _directionChangeFrequency;
        private UpdateLine _directionChangeLine;
        private CoroutineLauncher _coroutineLauncher;

        public Vector3 direction => _direction;

        [Inject]
        private void Construct(CoroutineLauncher coroutineLauncher)
        {
            _coroutineLauncher = coroutineLauncher;
        }

        public void Init(float moveSpeed, float directionChangeFrequency)
        {
            _directionChangeFrequency = (long)(directionChangeFrequency * 1000L);
            _direction = SpaceUtil.GetDirection();
            Init(moveSpeed, _direction);
        }

        public void StartMoving()
        {
            _directionChangeLine = new UpdateLine(ChangeDirection, _directionChangeFrequency);
            _coroutineLauncher.AddUpdate(_directionChangeLine);
            _active = true;
        }

        public void Ricochet(Vector3 normal)
        {
            _direction = Vector3.Reflect(_direction, normal);
        }
        
        public void Stop()
        {
            _active = false;
            _rigidbody.velocity = Vector3.zero;
            _coroutineLauncher.RemoveUpdate(_directionChangeLine);
            _directionChangeLine = null;
        }

        private void ChangeDirection()
        {
            _direction = SpaceUtil.GetDirection();
        }
    }
}
