using System;
using UnityEngine;

namespace Level
{
    [Serializable]
    public class LevelData
    {
        [SerializeField]
        private int _id;
        [SerializeField]
        private GameObject _prefab;

        public int id => _id;
        public GameObject prefab => _prefab;
    }
}
