using System.Collections.Generic;
using UnityEngine;
using Zenject;
using IPoolable = Interfaces.IPoolable;

namespace Utils.ObjectPool
{
    public class ObjectPool
    {
        private readonly Dictionary<Component, PoolTask> _activePoolTasks;
        private readonly Transform _container;
        private DiContainer _diContainer;

        public ObjectPool(DiContainer diContainer)
        {
            _diContainer = diContainer;
            _activePoolTasks = new Dictionary<Component, PoolTask>();
            _container = new GameObject().transform;
            _container.name = nameof(ObjectPool);
        }

        public void CreateFreeObjects<T>(T prefab, int count) where T : Component, IPoolable
        {
            if (!_activePoolTasks.TryGetValue(prefab, out var poolTask))
            {
                AddTaskToPool(prefab, out poolTask);
            }
            
            poolTask.CreateFreeObjects(prefab, count);
        }

        public T GetObject<T>(T prefab) where T : Component, IPoolable
        {
            if (!_activePoolTasks.TryGetValue(prefab, out var poolTask))
            {
                AddTaskToPool(prefab, out poolTask);
            }
            
            return poolTask.GetFreeObject(prefab);
        }

        public void Dispose()
        {
            foreach (var poolTask in _activePoolTasks.Values)
            {
                poolTask.Dispose();
            }
        }

        private void AddTaskToPool<T>(T prefab, out PoolTask poolTask) where T : Component, IPoolable
        {
            var taskContainer = new GameObject
            {
                name = $"{prefab.name}_pool",
                transform =
                {
                    parent = _container
                }
            };
            poolTask = new PoolTask(_diContainer, taskContainer.transform);
            _activePoolTasks.Add(prefab, poolTask);
        }
    }
}
