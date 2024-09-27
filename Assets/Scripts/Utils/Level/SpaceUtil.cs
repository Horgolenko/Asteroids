using UnityEngine;

namespace Utils.Level
{
    public static class SpaceUtil
    {
        private const float PlayerToEnemySafeDistance = 3.8f;
        private const float EnemyToEnemySafeDistance = 3.2f;
        
        public static bool IsEnemyToEnemyOverlap(Vector3 selfPosition, Vector3 otherPosition)
        {
            return Vector3.Distance(selfPosition, otherPosition) < EnemyToEnemySafeDistance;
        }
        
        public static bool IsPlayerToEnemyOverlap(Vector3 selfPosition, Vector3 otherPosition)
        {
            return Vector3.Distance(selfPosition, otherPosition) < PlayerToEnemySafeDistance;
        }

        public static Vector3 GetDirection()
        {
            var x = Random.Range(-1f, 1f);
            var z = Random.Range(-1f, 1f);
            return new Vector3(x, 0, z).normalized;
        }
    }
}
