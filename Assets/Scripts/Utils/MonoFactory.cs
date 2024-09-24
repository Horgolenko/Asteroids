using UnityEngine;

namespace Utils
{
    public class MonoFactory<T> where T : MonoBehaviour
    {
        public static T Create(T prefab)
        {
            var fruit = Object.Instantiate(prefab);
            return fruit;
        }
    }
}