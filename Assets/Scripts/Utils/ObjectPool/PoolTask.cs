using System.Collections.Generic;
using UnityEngine;
using Zenject;
using IPoolable = Interfaces.IPoolable;

namespace Utils.ObjectPool
{
    public class PoolTask
    {
        private readonly List<IPoolable> _freeObjects;
        private readonly List<IPoolable> _objectsInUse;
        private readonly Transform _container;
        private DiContainer _diContainer;

        public PoolTask(DiContainer diContainer, Transform container)
        {
            _diContainer = diContainer;
            _container = container;
            _objectsInUse = new List<IPoolable>();
            _freeObjects = new List<IPoolable>();
        }

        public void CreateFreeObjects<T>(T prefab, int count) where T : Component, IPoolable
        {
            for (var i = 0; i < count; i++)
            {
                var poolable = _diContainer.InstantiatePrefab(prefab, _container).GetComponent<T>();
                _freeObjects.Add(poolable);
            }
        }

        public T GetFreeObject<T>(T prefab) where T : Component, IPoolable
        {
            T poolable;
            if (_freeObjects.Count > 0)
            {
                poolable = _freeObjects[0] as T;
                _freeObjects.RemoveAt(0);
            }
            else
            {
                poolable = _diContainer.InstantiatePrefab(prefab, _container).GetComponent<T>();
            }
            poolable.Destroyed += ReturnToPool;
            poolable.GameObject.SetActive(true);
            _objectsInUse.Add(poolable);
            return poolable;
        }

        public void ReturnAllObjectsToPool()
        {
            foreach (var poolable in _objectsInUse)
            {
                poolable.Reset();
            }
        }

        public void Dispose()
        {
            foreach (var poolable in _objectsInUse)
            {
                Object.Destroy(poolable.GameObject);
            }

            foreach (var poolable in _freeObjects)
            {
                Object.Destroy(poolable.GameObject);
            }
        }

        private void ReturnToPool(IPoolable poolable)
        {
            _objectsInUse.Remove(poolable);
            _freeObjects.Add(poolable);
            poolable.Destroyed -= ReturnToPool;
            poolable.GameObject.SetActive(false);
            poolable.GameObject.transform.SetParent(_container);
        }
    }
}
