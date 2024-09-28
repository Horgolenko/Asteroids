namespace Settings.Player
{
    public readonly struct Movement
    {
        public readonly float maxAcceleration;
        public readonly float maxSpeed;

        public Movement(float maxAcceleration, float maxSpeed)
        {
            this.maxAcceleration = maxAcceleration;
            this.maxSpeed = maxSpeed;
        }
    }
}
