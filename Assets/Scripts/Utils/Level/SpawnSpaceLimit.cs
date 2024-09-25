using UnityEngine;

namespace Utils.Level
{
    public class SpawnSpaceLimit : MonoBehaviour
    {
        [SerializeField] private Transform _lowLeftPoint;
        [SerializeField] private Transform _topRightPoint;

        private Vector3 lowLeftPoint => _lowLeftPoint.position;
        private Vector3 topRightPoint => _topRightPoint.position;
        
        public Vector3 GetPosition()
        {
            var x = Random.Range(lowLeftPoint.x, topRightPoint.x);
            var z = Random.Range(lowLeftPoint.z, topRightPoint.z);
            return new Vector3(x, 1.5f, z);
        }
    }
}
