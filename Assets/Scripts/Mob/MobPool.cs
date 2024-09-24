using System.Collections.Generic;
using Game;
using UnityEngine;

namespace Mob
{
    public class MobPool : MonoBehaviour
    {
        private const int DefaultAmount = 100;
        
        [SerializeField] private MobBase[] _mobPrefabs;

        private readonly Dictionary<MobType, List<MobBase>> _mobs = new();

        private void Start()
        {
            var mobTypes = new List<MobType> { MobType.Red, MobType.Green, MobType.Blue };
            for (int i = 0; i < DefaultAmount; i++)
            {
                foreach (var mobType in mobTypes)
                {
                    var mob = Create(mobType);
                    Put(mob);
                }
            }
        }

        public MobBase Get(MobType mobType)
        {
            if (_mobs.TryGetValue(mobType, out List<MobBase> mobs))
            {
                if (mobs.Count != 0)
                {
                    var mob = mobs[0];
                    mobs.RemoveAt(0);
                    mob.gameObject.SetActive(true);
                    return mob;
                }

                return Create(mobType);
            }

            return Create(mobType);
        }

        public void Put(MobBase mob)
        {
            mob.gameObject.SetActive(false);
            mob.transform.position = transform.position;
            if (_mobs.TryGetValue(mob.mobData.mobType, out List<MobBase> mobs))
            {
                mobs.Add(mob);
            }
            else
            {
                var mobList = new List<MobBase> { mob };
                _mobs[mob.mobData.mobType] = mobList;
            }
        }

        private MobBase Create(MobType mobType)
        {
            var mob = Instantiate(GetPrefab(mobType), transform);
            mob.Init(this);
            mob.transform.parent = transform;
            return mob;
        }

        private MobBase GetPrefab(MobType mobType)
        {
            for (int i = 0; i < _mobPrefabs.Length; i++)
            {
                if (_mobPrefabs[i].mobData.mobType == mobType)
                {
                    return _mobPrefabs[i];
                }
            }

            return _mobPrefabs[^1];
        }
    }
}
