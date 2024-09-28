namespace Settings.Player
{
    public readonly struct Base
    {
        public readonly float respawnDelay;
        public readonly Movement movement;
        public readonly Shooting shooting;

        public Base(float respawnDelay, Movement movement, Shooting shooting)
        {
            this.respawnDelay = respawnDelay;
            this.movement = movement;
            this.shooting = shooting;
        }
    }
}
