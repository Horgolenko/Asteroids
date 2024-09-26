using System;
using Data;
using Interfaces;
using UnityEngine;

namespace Entities.Enemy
{
    [RequireComponent(typeof(AsteroidMover), typeof(AsteroidShooter))]
    public class Asteroid : MonoBehaviour, IDamageable, IPoolable
    {
        private AsteroidMover _asteroidMover;
        private AsteroidShooter _asteroidShooter;
        private AsteroidCollider _asteroidCollider;
        
        public GameObject GameObject => gameObject;
        public event Action<IPoolable> Destroyed;

        private void Awake()
        {
            _asteroidMover = GetComponent<AsteroidMover>();
            _asteroidShooter = GetComponent<AsteroidShooter>();
            
            _asteroidCollider = GetComponentInChildren<AsteroidCollider>();
            _asteroidCollider.ChangeDirection += OnChangeDirection;
        }

        private void OnDestroy()
        {
            _asteroidCollider.ChangeDirection -= OnChangeDirection;
        }

        public void Init(Vector3 position, EnemyData enemyData)
        {
            transform.position = position;
            
            _asteroidMover.Init(enemyData.moveSpeed, enemyData.directionChangeFrequency);
            _asteroidMover.StartMoving();

            _asteroidShooter.Init(enemyData.bulletSpeed, enemyData.fireDelay);
            _asteroidShooter.StartShooting();
        }
        
        public void Damage()
        {
            _asteroidMover.Stop();
            _asteroidShooter.Stop();
            Destroyed?.Invoke(this);
        }
        
        public void Reset()
        {
            transform.rotation = Quaternion.identity;
        }
        
        private void OnChangeDirection(Vector3 normal)
        {
            _asteroidMover.Ricochet(normal);
        }
    }
}
