namespace Data
{
    public struct EnemyData
    {
        public readonly float moveSpeed;
        public readonly float directionChangeFrequency;
        public readonly float bulletSpeed;
        public readonly float fireDelay;

        public EnemyData(float moveSpeed, float directionChangeFrequency, float bulletSpeed, float fireDelay)
        {
            this.moveSpeed = moveSpeed;
            this.directionChangeFrequency = directionChangeFrequency;
            this.bulletSpeed = bulletSpeed;
            this.fireDelay = fireDelay;
        }
    }
}
