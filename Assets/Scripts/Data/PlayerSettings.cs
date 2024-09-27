using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "PlayerSettings", menuName = "ScriptableObjects/PlayerSettings", order = 1)]
    public class PlayerSettings : ScriptableObject
    {
        [Header("Player Speed Settings")]
        [SerializeField] private float _maxAcceleration;
        [SerializeField] private float _maxSpeed;
        [SerializeField] private float _bulletSpeed;
        
        [Space]
        [Header("Player Settings")]
        [SerializeField] private int _maxShotAmount;
        [SerializeField] private int _maxLifeAmount;
        [SerializeField] private int _enemiesToKill;
        [SerializeField] private float _respawnDelay;
        
        public float maxAcceleration => _maxAcceleration;
        public float maxSpeed => _maxSpeed;
        public float bulletSpeed => _bulletSpeed;
        public int maxShotAmount => _maxShotAmount;
        public int maxLifeAmount => _maxLifeAmount;
        public int enemiesToKill => _enemiesToKill;
        public float respawnDelay => _respawnDelay;
    }
}
