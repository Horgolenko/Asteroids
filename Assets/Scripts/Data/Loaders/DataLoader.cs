using Enemy = Settings.Enemy;
using Player = Settings.Player;
using UnityEngine;

namespace Data.Loaders
{
    public static class DataLoader
    {
        private const string PlayerDataPath = "Data/PlayerSettings";
        private const string EnemySettingsPath = "Data/EnemySettings";
        private const string GameSettingsPath = "Data/GameSettings";

        public static Settings.Game GetGameData()
        {
            var gameSettings = Get<GameSettings>(GameSettingsPath);
            return new Settings.Game(gameSettings.maxEnemies, gameSettings.enemiesToKill, gameSettings.maxLifeAmount);
        }

        public static Enemy.Base GetEnemyData()
        {
            var enemySettings = Get<EnemySettings>(EnemySettingsPath);
            
            var spawn = new Enemy.Spawn(enemySettings.initialSpawnDelay, enemySettings.spawnDelay, enemySettings.spawnDelta);
            var movement = new Enemy.Movement(enemySettings.moveSpeed, enemySettings.directionChangeFrequency);
            var shooting = new Enemy.Shooting(enemySettings.bulletSpeed, enemySettings.fireDelay);
            
            return new Enemy.Base(spawn, movement, shooting);
        }
        
        public static Player.Base GetPlayerData()
        {
            var playerSettings = Get<PlayerSettings>(PlayerDataPath);
            
            var movement = new Player.Movement(playerSettings.maxAcceleration, playerSettings.maxSpeed);

            var shooting = new Player.Shooting(playerSettings.bulletSpeed, playerSettings.maxShotAmount);
            
            return new Player.Base(playerSettings.respawnDelay, movement, shooting);
        }

        private static T Get<T>(string path) where T : ScriptableObject
        {
            return Resources.Load(path) as T;
        }
    }
}
