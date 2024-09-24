using System;
using System.Collections;
using Game;
using Mob;
using TMPro;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Spawners
{
    public class EnemySpawner : MonoBehaviour, IDamageable
    {
        private const float MobSpawnDelay = 0.1f;
        private const float SpawnDelay = 5f;
        private const int MinAmount = 25;
        private const int MaxAmount = 45;

        [SerializeField]
        private int _hitPoints;
        [SerializeField]
        private TextMeshProUGUI _text;
        [SerializeField]
        private Transform _spawnPosition;
        [SerializeField]
        private GameObject _portal;
        [SerializeField] 
        private ParticleSystem _particleSystem;

        private int _id;
        private MobType _mobType;
        private Coroutine _spawnCoroutine;
        private Array _mobTypeValues;
        private MobPool _mobPool;
        private BoxCollider _collider;
        private readonly Vector3 _rotation = new(0, 180, 0);
        private readonly WaitForSeconds _waitMobForSpawn = new(MobSpawnDelay);
        private readonly WaitForSeconds _waitForSpawn = new(SpawnDelay);
        private readonly System.Random _random = new();

        public static Action<int> OnDestroyPortal;
        
        [Inject]
        private void Construct(MobPool mobPool)
        {
            _mobPool = mobPool;
        }
        
        private void Awake()
        {
            _text.text = $"{_hitPoints}";
            _mobTypeValues = Enum.GetValues(typeof(MobType));
            _spawnCoroutine = StartCoroutine(SpawnCoroutine());
            _collider = GetComponent<BoxCollider>();
        }

        private void OnDestroy()
        {
            StopCoroutine();
        }

        public void Init(int id)
        {
            _id = id;
        }
        
        private void StopCoroutine()
        {
            if (_spawnCoroutine != null)
            {
                StopCoroutine(_spawnCoroutine);
                _spawnCoroutine = null;
            }
        }
        
        private IEnumerator SpawnCoroutine()
        {
            while (true)
            {
                yield return _waitForSpawn;
                _mobType = GetMobType();
                var spawnAmount = Random.Range(MinAmount, MaxAmount);
                for (int i = 0; i < spawnAmount; i++)
                {
                    Spawn();
                    yield return _waitMobForSpawn;
                }
            }
        }

        private MobType GetMobType()
        {
            return (MobType)_mobTypeValues.GetValue(_random.Next(_mobTypeValues.Length));
        }

        private void Spawn()
        {
            var mob = _mobPool.Get(_mobType);
            mob.transform.position = new Vector3(_spawnPosition.position.x + Random.Range(-0.5f, 0.5f),
                                                _spawnPosition.position.y,
                                                _spawnPosition.position.z);
            mob.transform.rotation = Quaternion.Euler(_rotation);
            mob.mobData.SetSide(SideType.Enemy);
            mob.Init(PlayerPosition.Instance.Position());
            mob.Run();
        }

        public void Damage()
        {
            _hitPoints--;
            if (_hitPoints > 0)
            {
                _text.text = $"{_hitPoints}";
                return;
            }

            OnDestroyPortal?.Invoke(_id);
            _collider.enabled = false;
            StopCoroutine();
            Destroy(_portal);
            _particleSystem.Play();
        }
    }
}
