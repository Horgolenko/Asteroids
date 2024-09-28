using LazySquirrelLabs.MinMaxRangeAttribute;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "EnemySettings", menuName = "ScriptableObjects/EnemySettings", order = 2)]
    public class EnemySettings : ScriptableObject
    {
        [Header("Spawn Settings")]
        [SerializeField] private float _initialSpawnDelay;
        [SerializeField] private float _spawnDelay;
        [SerializeField] private float _spawnDelta;
        
        [Space(5)] [Header("Movement Settings")]
        
        [MinMaxRange(150, 500)] [SerializeField] private Vector2 _moveSpeed;
        [MinMaxRange(0, 100)] [SerializeField] private Vector2 _directionChangeFrequency;
        
        [Space(5)] [Header("Shooting Settings (in seconds)")]
        
        [MinMaxRange(200, 600)] [SerializeField] private Vector2 _bulletSpeed;
        [MinMaxRange(0, 100)] [SerializeField] private Vector2 _fireDelay;
        
        public float initialSpawnDelay => _initialSpawnDelay;
        public float spawnDelay => _spawnDelay;
        public float spawnDelta => _spawnDelta;
        
        public Vector2 moveSpeed => _moveSpeed;
        public Vector2 directionChangeFrequency => _directionChangeFrequency;
        
        public Vector2 bulletSpeed => _bulletSpeed;
        public Vector2 fireDelay => _fireDelay;
    }
}
