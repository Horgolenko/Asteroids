using LazySquirrelLabs.MinMaxRangeAttribute;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "GameSettings", menuName = "ScriptableObjects/GameSettings", order = 2)]
    public class GameSettings : ScriptableObject
    {
        [SerializeField] private int _maxEnemies;
        [SerializeField] private int _enemiesToKill;
        [SerializeField] private int _maxLifeAmount;
        
        public int maxEnemies => _maxEnemies;
        public int enemiesToKill => _enemiesToKill;
        public int maxLifeAmount => _maxLifeAmount;
    }
}
