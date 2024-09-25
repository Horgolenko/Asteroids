namespace Data
{
    public struct EnemyData
    {
        public readonly float moveSpeed;
        public readonly float bulletSpeed;
        public readonly float directionChangeFrequency;
        public readonly float fireDelay;

        public EnemyData(float moveSpeed, float bulletSpeed, float directionChangeFrequency, float fireDelay)
        {
            this.moveSpeed = moveSpeed;
            this.bulletSpeed = bulletSpeed;
            this.directionChangeFrequency = directionChangeFrequency;
            this.fireDelay = fireDelay;
        }
    }
}
