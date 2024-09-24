using System;
using System.Collections.Generic;
using Game;
using Spawners;
using UnityEngine;

namespace Mob
{
    [RequireComponent(typeof(MobNavigator))] [RequireComponent(typeof(MobAnimator))]
    public class MobBase : MonoBehaviour, IDamageable
    {
        [SerializeField]
        private MobData _mobData;

        private bool _selfDamage;
        private int _hitPoints;
        private Vector3 _destination;
        private MobAnimator _mobAnimator;
        private MobNavigator _mobNavigator;
        private BoxCollider _collider;
        private MobPool _mobPool;
        private readonly Dictionary<MobType, MobType> _winner = new()
        {
            { MobType.Red, MobType.Green },
            { MobType.Green, MobType.Blue },
            { MobType.Blue, MobType.Red }
        };
        
        public int hitPoints => _hitPoints;
        private Vector3 destination => _destination;
        public MobData mobData => _mobData;

        public static Action EnemyKilled;
        
        private void Awake()
        {
            _mobAnimator = GetComponent<MobAnimator>();
            _mobNavigator = GetComponent<MobNavigator>();
            _collider = GetComponent<BoxCollider>();
        }

        private void Start()
        {
            EnemyPosition.EnemyPositionUpdated += OnEnemyPositionUpdated;
        }

        private void OnDestroy()
        {
            EnemyPosition.EnemyPositionUpdated -= OnEnemyPositionUpdated;
        }

        public void Init(MobPool mobPool)
        {
            _mobPool = mobPool;
        }
        
        public void Init(Vector3 destination, int hitPoints = 1)
        {
            _destination = destination;
            _hitPoints = hitPoints;
        }
        
        public void Run()
        {
            _collider.enabled = true;
            _mobNavigator.Run(_destination);
            _mobAnimator.RunAnimation();
        }

        public void Copy(MobBase mobBase, int id)
        {
            transform.position = mobBase.transform.position;
            transform.rotation = mobBase.transform.rotation;
            mobData.Init(id);
            mobData.SetSide(mobBase.mobData.sideType);
            _destination = mobBase.destination;
            _hitPoints = mobBase.hitPoints;
        }

        private void OnTriggerEnter(Collider other)
        {
            var damageable = other.GetComponent<IDamageable>();
            if (damageable == null) return;
            
            if (damageable is MobBase otherMob)
            {
                if (otherMob.mobData.sideType == mobData.sideType) return;

                if (mobData.mobType == otherMob.mobData.mobType)
                {
                    Attack(true);
                }
                else
                {
                    if (IsWinning(mobData.mobType, otherMob.mobData.mobType))
                    {
                        Attack(false);
                    }
                    else
                    {
                        Damage();
                    }
                }
            }
            else if (mobData.sideType != SideType.Enemy)
            {
                Attack(true);
                damageable.Damage();
            }
        }

        private void Attack(bool selfDamage)
        {
            _selfDamage = selfDamage;
            _mobNavigator.Stop();
            _mobAnimator.HitAnimation();
        }
        
        public void Damage()
        {
            _hitPoints--;
            if (_hitPoints > 0) return;
            
            _collider.enabled = false;
            if (_mobData.sideType == SideType.Enemy) EnemyKilled?.Invoke();
            _mobNavigator.Stop();
            _mobAnimator.DieAnimation();
        }

        public void OnHitCompleted()
        {
            if (!_selfDamage)
            {
                _selfDamage = true;
                return;
            }

            _selfDamage = false;
            _hitPoints--;
            if (_hitPoints > 0) return;
            
            _collider.enabled = false;
            _mobAnimator.DieAnimation();
        }

        public void OnDieCompleted()
        {
            _mobData.Clear();
            _mobPool.Put(this);
        }
        
        private bool IsWinning(MobType it, MobType other)
        {
            return _winner[it] == other;
        }
        
        private void OnEnemyPositionUpdated()
        {
            if (!isActiveAndEnabled || _mobData.sideType == SideType.Enemy) return;
            
            Init(EnemyPosition.Instance.Position(), _hitPoints);
            Run();
        }
    }
}
