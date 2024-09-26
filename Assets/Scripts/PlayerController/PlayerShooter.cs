using Data.Loaders;
using Entities.Player;
using UnityEngine;
using Utils.ObjectPool;
using Zenject;

namespace PlayerController
{
    public class PlayerShooter : MonoBehaviour
    {
        [SerializeField] private PlayerProjectile _projectilePrefab;
        [SerializeField] private Transform _shotPosition;
        
        private byte _shootsCount;
        private ObjectPool _objectPool;
        private Rigidbody _rigidbody;

        [Inject]
        private void Construct(DiContainer diContainer)
        {
            _objectPool = new ObjectPool(diContainer);
        }

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        public bool CanFire()
        {
            return _shootsCount < DataLoader.GetPlayerData().maxShotAmount;
        }

        public void Fire()
        {
            _shootsCount++;
            var projectile = _objectPool.GetObject(_projectilePrefab);
            projectile.Init(_rigidbody.velocity, _shotPosition.position, transform.forward, () => _shootsCount--);
        }
    }
}
