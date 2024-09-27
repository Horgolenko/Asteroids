using System;
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
        
        private ObjectPool _objectPool;
        private Rigidbody _rigidbody;

        public static Action ShotFired;
        public static Action ShotDestroyed;
        
        [Inject]
        private void Construct(ObjectPool objectPool)
        {
            _objectPool = objectPool;
        }

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        public void Fire()
        {
            ShotFired?.Invoke();
            var projectile = _objectPool.GetObject(_projectilePrefab);
            projectile.Init(_rigidbody.velocity, _shotPosition.position, transform.forward, () => ShotDestroyed?.Invoke());
        }
    }
}
