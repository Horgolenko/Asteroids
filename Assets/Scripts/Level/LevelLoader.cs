using System.Collections.Generic;
using Game;
using UnityEngine;
using Zenject;

namespace Level
{
    public class LevelLoader : MonoBehaviour
    {
        private const string Levels = "Levels";
    
        private static readonly Dictionary<int, LevelData> _levels = new();
        
        [SerializeField]
        private Transform _levelPosition;

        public static int MaxLevels => _levels.Count - 1;
        
        [Inject]
        private void Construct(GameProvider gameProvider, DiContainer diContainer)
        {
            LoadLevels();
        }

        private void LoadLevels()
        {
            var levels = Resources.Load(Levels) as Levels;
            for (int i = 0; i < levels.levelDatas.Length; i++)
            {
                var levelDatas = levels.levelDatas[i];
                _levels[levelDatas.id] = levelDatas;
            }
        }

        private static LevelData GetEnemyData(int id)
        {
            return _levels.TryGetValue(id, out var value) ? value : new LevelData();
        }
    }
}
