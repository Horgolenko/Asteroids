namespace Settings.Player
{
    public readonly struct Base
    {
        public readonly long respawnDelay;
        public readonly Movement movement;
        public readonly Shooting shooting;

        public Base(float respawnDelay, Movement movement, Shooting shooting)
        {
            this.respawnDelay = (long)(respawnDelay * 1000L);
            this.movement = movement;
            this.shooting = shooting;
        }
    }
}
