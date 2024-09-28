using UnityEngine;

namespace Settings.Enemy
{
    public readonly struct Shooting
    {
        public readonly Vector2 bulletSpeed;
        public readonly Vector2 fireDelay;

        public Shooting(Vector2 bulletSpeed, Vector2 fireDelay)
        {
            this.bulletSpeed = bulletSpeed;
            this.fireDelay = fireDelay;
        }
    }
}
