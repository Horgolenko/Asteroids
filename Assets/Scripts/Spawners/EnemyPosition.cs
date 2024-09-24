using System;
using System.Collections.Generic;
using System.Linq;
using States;
using UnityEngine;
using Zenject;

namespace Spawners
{
    public class EnemyPosition : MonoBehaviour
    {
        public static EnemyPosition Instance;

        [SerializeField]
        private EnemySpawner[] _spawns;

        private readonly Dictionary<int, EnemySpawner> _enemySpawners = new();
        private StateMachine _stateMachine;

        public static Action EnemyPositionUpdated;
        
        [Inject]
        private void Construct(StateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }
        
        private void Start()
        {
            Instance = this;
            EnemySpawner.OnDestroyPortal += OnDestroyPortal;

            InitDictionary();
        }

        private void InitDictionary()
        {
            for (int i = 0; i < _spawns.Length; i++)
            {
                _enemySpawners[i] = _spawns[i];
                _spawns[i].Init(i);
            }
        }

        private void OnDestroy()
        {
            EnemySpawner.OnDestroyPortal -= OnDestroyPortal;
        }

        public Vector3 Position()
        {
            return _enemySpawners.Count == 0 ? Vector3.zero : _enemySpawners.First().Value.transform.position;
        }
        
        private void OnDestroyPortal(int id)
        {
            _enemySpawners.Remove(id);
            if (_enemySpawners.Count == 0)
            {
                _stateMachine.SetState<WinState>();
            }
            else
            {
                EnemyPositionUpdated?.Invoke();
            }
        }
    }
}
