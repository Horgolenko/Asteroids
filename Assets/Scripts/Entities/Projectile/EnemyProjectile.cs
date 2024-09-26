using System;
using Interfaces;
using UnityEngine;

namespace Entities.Projectile
{
    [RequireComponent(typeof(ProjectileMover))]
    public class EnemyProjectile : MonoBehaviour, IPoolable
    {
        private ProjectileMover _projectileMover;
        private EnemyProjectileCollider _enemyProjectileCollider;
        
        public GameObject GameObject => gameObject;
        public event Action<IPoolable> Destroyed;

        private void Awake()
        {
            _projectileMover = GetComponent<ProjectileMover>();
            
            _enemyProjectileCollider = GetComponentInChildren<EnemyProjectileCollider>();
        }

        private void OnEnable()
        {
            _enemyProjectileCollider.HitWall += OnHitWall;
        }

        private void OnDisable()
        {
            _enemyProjectileCollider.HitWall -= OnHitWall;
        }

        public void Init(float bulletSpeed, Vector3 direction)
        {
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
