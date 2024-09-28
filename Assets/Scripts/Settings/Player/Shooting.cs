namespace Settings.Player
{
    public readonly struct Shooting
    {
        public readonly float bulletSpeed;
        public readonly int maxShotAmount;

        public Shooting(float bulletSpeed, int maxShotAmount)
        {
            this.bulletSpeed = bulletSpeed;
            this.maxShotAmount = maxShotAmount;
        }
    }
}
