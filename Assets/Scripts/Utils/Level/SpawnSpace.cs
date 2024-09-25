using UnityEngine;

namespace Utils.Level
{
    public class SpawnSpace : MonoBehaviour
    {
        [SerializeField] private SpawnSpaceLimit[] _spawnSpaceLimits;

        public Vector3 GetPosition()
        {
            int index = Random.Range(0, _spawnSpaceLimits.Length);
            return _spawnSpaceLimits[index].GetPosition();
        }
    }
}
