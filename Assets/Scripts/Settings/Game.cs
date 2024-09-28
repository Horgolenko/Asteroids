namespace Settings
{
    public readonly struct Game
    {
        public readonly int maxEnemies;
        public readonly int enemiesToKill;
        public readonly int maxLifeAmount;

        public Game(int maxEnemies, int enemiesToKill, int maxLifeAmount)
        {
            this.maxEnemies = maxEnemies;
            this.enemiesToKill = enemiesToKill;
            this.maxLifeAmount = maxLifeAmount;
        }
    }
}
