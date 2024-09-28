using System;
using Entities.Projectile;
using Interfaces;
using UnityEngine;

namespace Entities.Player
{
    [RequireComponent(typeof(ProjectileMover))]
    public class PlayerProjectile : MonoBehaviour, IPoolable
    {
        private Action _onDestroy;
        private ProjectileMover _projectileMover;
        private PlayerProjectileCollider _projectileCollider;
        
        public GameObject GameObject => gameObject;
        public event Action<IPoolable> Destroyed;

        private void Awake()
        {
            _projectileMover = GetComponent<ProjectileMover>();
            _projectileCollider = GetComponentInChildren<PlayerProjectileCollider>();
        }

        private void OnEnable()
        {
            _projectileCollider.HitWall += OnHit;
            _projectileCollider.HitEnemy += OnHit;
        }

        private void OnDisable()
        {
            _projectileCollider.HitWall -= OnHit;
            _projectileCollider.HitEnemy -= OnHit;
        }

        public void Reset()
        {
            
        }

        public void Init(Vector3 position, Vector3 direction, Action onDestroy)
        {
            _onDestroy = onDestroy;
            transform.position = position;
            _projectileMover.Init(Settings.Data.player.shooting.bulletSpeed, direction);
            _projectileMover.StartMoving();
        }
        
        private void OnHit()
        {
            _onDestroy?.Invoke();
            _projectileMover.Stop();
            Destroyed?.Invoke(this);
        }
    }
}
