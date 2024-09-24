using UnityEngine;

namespace Level
{
    [CreateAssetMenu(fileName = "Levels", menuName = "ScriptableObjects/Levels", order = 1)]
    public class Levels : ScriptableObject
    {
        [SerializeField]
        private LevelData[] _levelDatas;
        
        public LevelData[] levelDatas => _levelDatas;
    }
}
