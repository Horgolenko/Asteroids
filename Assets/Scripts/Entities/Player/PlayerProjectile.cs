using System;
using Data.Loaders;
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
            _projectileCollider.HitWall += OnHitWall;
        }

        private void OnDisable()
        {
            _projectileCollider.HitWall -= OnHitWall;
        }

        public void Reset()
        {
            
        }

        public void Init(Vector3 velocity, Vector3 position, Vector3 direction, Action onDestroy)
        {
            _onDestroy = onDestroy;
            transform.position = position;
            Debug.Log($"transform.position = {transform.position}");
            _projectileMover.Init(velocity.magnitude + DataLoader.GetPlayerData().bulletSpeed, direction);
            _projectileMover.StartMoving();
        }
        
        private void OnHitWall()
        {
            _onDestroy?.Invoke();
            _projectileMover.Stop();
            Destroyed?.Invoke(this);
        }
    }
}
