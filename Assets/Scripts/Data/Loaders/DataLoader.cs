using UnityEngine;
using Random = UnityEngine.Random;

namespace Data.Loaders
{
    public static class DataLoader
    {
        private const string PlayerDataPath = "Data/PlayerData";
        private const string EnemySettingsPath = "Data/EnemySettings";

        private static PlayerData _playerData;
        private static EnemySettings _enemySettings;
        
        public static void Load()
        {
            LoadData();
        }

        private static void LoadData()
        {
            _playerData = Resources.Load(PlayerDataPath) as PlayerData;
            _enemySettings = Resources.Load(EnemySettingsPath) as EnemySettings;
        }

        public static float GetSpawnDelay()
        {
            return _enemySettings.spawnDelay;
        }

        public static ushort GetMaxEnemies()
        {
            return _enemySettings.maxEnemies;
        }
        
        public static EnemyData GetEnemyData()
        {
            var moveSpeed = Random.Range(_enemySettings.moveSpeed.x, _enemySettings.moveSpeed.y);
            var bulletSpeed = Random.Range(_enemySettings.bulletSpeed.x, _enemySettings.bulletSpeed.y);
            var directionChangeFrequency = Random.Range(_enemySettings.directionChangeFrequency.x, _enemySettings.directionChangeFrequency.y);
            var fireDelay = Random.Range(_enemySettings.fireDelay.x, _enemySettings.fireDelay.y);
            return new EnemyData(moveSpeed, bulletSpeed, directionChangeFrequency, fireDelay);
        }
        
        public static PlayerData GetPlayerData()
        {
            return _playerData;
        }
    }
}
