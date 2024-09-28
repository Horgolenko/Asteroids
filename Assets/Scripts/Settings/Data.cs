using Data.Loaders;

namespace Settings
{
    public readonly struct Data
    {
        public static readonly Game game;
        public static readonly Enemy.Base enemy;
        public static readonly Player.Base player;

        static Data()
        {
            game = DataLoader.GetGameData();
            enemy = DataLoader.GetEnemyData();
            player = DataLoader.GetPlayerData();
        }
    }
}
