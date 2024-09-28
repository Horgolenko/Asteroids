using Entities.Enemy;
using Entities.Projectile;
using UnityEngine;
using Utils.ObjectPool;
using Zenject;

namespace VFX
{
    public class EffectRunner : MonoBehaviour
    {
        [SerializeField] private Effect _asteroidPrefab;
        [SerializeField] private Effect _projectilePrefab;

        private ObjectPool _objectPool;

        [Inject]
        private void Construct(ObjectPool objectPool)
        {
            _objectPool = objectPool;
        }

        private void Start()
        {
            Asteroid.AsteroidDestroyed += OnEnemyKilled;
            EnemyProjectile.ProjectileDestroyed += OnProjectileDestroyed;
        }

        private void OnDestroy()
        {
            Asteroid.AsteroidDestroyed -= OnEnemyKilled;
            EnemyProjectile.ProjectileDestroyed -= OnProjectileDestroyed;
        }

        private void OnEnemyKilled(Vector3 position)
        {
            PlayEffect(position, _asteroidPrefab);
        }
        
        private void OnProjectileDestroyed(Vector3 position)
        {
            PlayEffect(position, _projectilePrefab);
        }

        private void PlayEffect(Vector3 position, Effect prefab)
        {
            var effect = _objectPool.GetObject(prefab);
            effect.transform.position = position;
            effect.Play();
        }
    }
}
