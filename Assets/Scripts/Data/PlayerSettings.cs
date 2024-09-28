using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "PlayerSettings", menuName = "ScriptableObjects/PlayerSettings", order = 1)]
    public class PlayerSettings : ScriptableObject
    {
        [Header("Speed Settings")]
        [SerializeField] private float _maxAcceleration;
        [SerializeField] private float _maxSpeed;

        [Space]
        [Header("Shooting Settings")]
        [SerializeField] private float _bulletSpeed;
        [SerializeField] private int _maxShotAmount;
        
        [Space]
        [Header("General Settings")]
        [SerializeField] private float _respawnDelay;
        
        public float maxAcceleration => _maxAcceleration;
        public float maxSpeed => _maxSpeed;
        
        public float bulletSpeed => _bulletSpeed;
        public int maxShotAmount => _maxShotAmount;
        
        public float respawnDelay => _respawnDelay;
    }
}
