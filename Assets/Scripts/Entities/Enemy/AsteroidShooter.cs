using Entities.Projectile;
using UnityEngine;
using Utils.CoroutineUtils;
using Utils.ObjectPool;
using Zenject;

namespace Entities.Enemy
{
    public class AsteroidShooter : MonoBehaviour
    {
        [SerializeField] private Transform _transform;
        [SerializeField] private EnemyProjectile _projectilePrefab;

        private float _bulletSpeed;
        private long _fireDelay;
        private ObjectPool _objectPool;
        private CoroutineLauncher _coroutineLauncher;
        private UpdateLine _shootingLine;
        private AsteroidMover _asteroidMover;
        
        [Inject]
        private void Construct(ObjectPool objectPool, CoroutineLauncher coroutineLauncher)
        {
            _objectPool = objectPool;
            _coroutineLauncher = coroutineLauncher;
        }

        public void Init(float bulletSpeed, float fireDelay, AsteroidMover asteroidMover)
        {
            _bulletSpeed = bulletSpeed;
            _fireDelay = (long)(fireDelay * 1000L);
            _asteroidMover = asteroidMover;
        }

        public void StartShooting()
        {
            _shootingLine = new UpdateLine(Shoot, _fireDelay);
            _coroutineLauncher.AddUpdate(_shootingLine);
        }

        private void Shoot()
        {
            var bullet = _objectPool.GetObject(_projectilePrefab);
            bullet.transform.position = _transform.position;
            bullet.Init(_bulletSpeed, _asteroidMover.direction);
        }

        public void Stop()
        {
            _coroutineLauncher.RemoveUpdate(_shootingLine);
            _shootingLine = null;
        }
    }
}
