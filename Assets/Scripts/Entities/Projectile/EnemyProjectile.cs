using System;
using Interfaces;
using UnityEngine;

namespace Entities.Projectile
{
    [RequireComponent(typeof(ProjectileMover))]
    public class EnemyProjectile : MonoBehaviour, IPoolable
    {
        [SerializeField] private Transform _transform;
        
        private ProjectileMover _projectileMover;
        private ProjectileCollider _projectileCollider;
        
        public GameObject GameObject => gameObject;
        public event Action<IPoolable> Destroyed;

        private void Awake()
        {
            _projectileMover = GetComponent<ProjectileMover>();
            
            _projectileCollider = GetComponentInChildren<ProjectileCollider>();
            _projectileCollider.HitWall += OnHitWall;
        }

        private void OnDestroy()
        {
            _projectileCollider.HitWall -= OnHitWall;
        }

        public void Init(float bulletSpeed, Vector3 direction)
        {
            _transform.localPosition = Vector3.zero;
            _projectileMover.Init(bulletSpeed, direction);
            _projectileMover.StartMoving();
        }
        
        public void Reset()
        {
            
        }
        
        private void OnHitWall()
        {
            _projectileMover.Stop();
            Destroyed?.Invoke(this);
        }
    }
}
