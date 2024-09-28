namespace Settings.Enemy
{
    public readonly struct Spawn
    {
        public readonly float initialSpawnDelay;
        public readonly float spawnDelay;
        public readonly float spawnDelta;

        public Spawn(float initialSpawnDelay, float spawnDelay, float spawnDelta)
        {
            this.initialSpawnDelay = initialSpawnDelay;
            this.spawnDelay = spawnDelay;
            this.spawnDelta = spawnDelta;
        }
    }
}
