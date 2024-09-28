using UnityEngine;

namespace Settings.Enemy
{
    public readonly struct Movement
    {
        public readonly Vector2 moveSpeed;
        public readonly Vector2 directionChangeFrequency;

        public Movement(Vector2 moveSpeed, Vector2 directionChangeFrequency)
        {
            this.moveSpeed = moveSpeed;
            this.directionChangeFrequency = directionChangeFrequency;
        }
    }
}
