using LazySquirrelLabs.MinMaxRangeAttribute;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "EnemySettings", menuName = "ScriptableObjects/EnemySettings", order = 2)]
    public class EnemySettings : ScriptableObject
    {
        [Header("Spawn Settings")]
        [SerializeField] private float _spawnDelay;
        [SerializeField] private ushort _maxEnemies;
        
        [Space(5)] [Header("Speeds")]
        
        [MinMaxRange(150, 500)] [SerializeField] private Vector2 _moveSpeed;
        [MinMaxRange(200, 600)] [SerializeField] private Vector2 _bulletSpeed;
        
        [Space(5)] [Header("In seconds")]
        
        [MinMaxRange(0, 100)] [SerializeField] private Vector2 _directionChangeFrequency;
        [MinMaxRange(0, 100)] [SerializeField] private Vector2 _fireDelay;
        
        public float spawnDelay => _spawnDelay;
        public ushort maxEnemies => _maxEnemies;
        
        public Vector2 moveSpeed => _moveSpeed;
        public Vector2 bulletSpeed => _bulletSpeed;
        
        public Vector2 directionChangeFrequency => _directionChangeFrequency;
        public Vector2 fireDelay => _fireDelay;
    }
}
