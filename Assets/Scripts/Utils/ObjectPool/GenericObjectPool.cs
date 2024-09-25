using System.Collections.Generic;
using Interfaces;
using UnityEngine;

namespace Utils.ObjectPool
{
    public class GenericObjectPool<T> where T : Component, IPoolable
    {
        private readonly List<IPoolable> _freeObjects;
        private readonly Transform _container;
        private readonly T _prefab;

        public GenericObjectPool(T prefab)
        {
            _freeObjects = new List<IPoolable>();
            _container = new GameObject().transform;
            _container.name = prefab.GameObject.name;
            _prefab = prefab;
        }

        public IPoolable GetFreeObject()
        {
            IPoolable poolable;
            if (_freeObjects.Count > 0)
            {
                poolable = _freeObjects[0] as T;
                _freeObjects.RemoveAt(0);
            }
            else
            {
                poolable = Object.Instantiate(_prefab, _container);
            }
            poolable.GameObject.SetActive(true);
            poolable.Destroyed += ReturnToPool;
            return poolable;
        }

        private void ReturnToPool(IPoolable poolable)
        {
            _freeObjects.Add(poolable);
            poolable.Destroyed -= ReturnToPool;
            poolable.GameObject.SetActive(false);
            poolable.GameObject.transform.SetParent(_container);
        }
    }
}
