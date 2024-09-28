using Data;
using UnityEngine;

namespace Settings.Enemy
{
    public readonly struct Base
    {
        public readonly Spawn spawn;
        private readonly Movement movement;
        private readonly Shooting shooting;

        public Base(Spawn spawn, Movement movement, Shooting shooting)
        {
            this.spawn = spawn;
            this.movement = movement;
            this.shooting = shooting;
        }
        
        public EnemyData GetEnemyData()
        {
            var moveSpeed = Random.Range(movement.moveSpeed.x, movement.moveSpeed.y);
            var directionChangeFrequency = Random.Range(movement.directionChangeFrequency.x, movement.directionChangeFrequency.y);
            var bulletSpeed = Random.Range(shooting.bulletSpeed.x, shooting.bulletSpeed.y);
            var fireDelay = Random.Range(shooting.fireDelay.x, shooting.fireDelay.y);
            return new EnemyData(moveSpeed, directionChangeFrequency, bulletSpeed, fireDelay);
        }
    }
}
