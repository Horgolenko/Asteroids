using System.Collections;
using Game;
using Mob;
using UI;
using UnityEngine;
using Zenject;

namespace Spawners
{
    public class MobSpawner : MonoBehaviour
    {
        private const float SpawnDelay = 0.5f;

        [SerializeField]
        private Transform _spawnPosition;
        [SerializeField]
        private ParticleSystem _particleSystem;
        
        private MobType _mobType = MobType.Red;
        private MobPool _mobPool;
        private Coroutine _active;
        private readonly WaitForSeconds _waitForSpawn = new(SpawnDelay);
        
        [Inject]
        private void Construct(MobPool mobPool)
        {
            _mobPool = mobPool;
        }

        private void Start()
        {
            _active = StartCoroutine(SpawnCoroutine());
        }

        private void OnDestroy()
        {
            if (_active != null)
            {
                StopCoroutine(_active);
            }
        }

        private IEnumerator SpawnCoroutine()
        {
            while (true)
            {
                yield return _waitForSpawn;

                var mob = _mobPool.Get(_mobType);
                mob.mobData.SetSide(SideType.Friend);
                _particleSystem.Play();
                mob.transform.position = _spawnPosition.position;
                mob.transform.rotation = Quaternion.identity;
                mob.Init(EnemyPosition.Instance.Position());
                mob.Run();
            }
        }
    }
}
