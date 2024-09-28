using System;
using Entities.Enemy;
using PlayerController;
using States;
using UnityEngine;
using Zenject;

namespace Game
{
    public class GameProvider : MonoBehaviour
    {
        private int _currentShots;
        private int _currentLife;
        private int _enemiesToKill;
        private StateMachine _stateMachine;

        public static Action<int> ShotAmountChanged;
        public static Action<int> LifeAmountChanged;
        public static Action<int> EnemiesAmountChanged;

        [Inject]
        private void Construct(StateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }
        
        private void Start()
        {
            Init();

            StateMachine.StateChanged += OnStateChanged;
            
            PlayerShooter.ShotFired += OnShotFired;
            PlayerShooter.ShotDestroyed += OnShotDestroyed;

            Asteroid.AsteroidDestroyed += OnEnemyKilled;
        }

        private void OnDestroy()
        {
            StateMachine.StateChanged -= OnStateChanged;
            
            PlayerShooter.ShotFired -= OnShotFired;
            PlayerShooter.ShotDestroyed -= OnShotDestroyed;

            Asteroid.AsteroidDestroyed -= OnEnemyKilled;
        }

        public bool CanFire()
        {
            return _currentShots > 0;
        }

        private void Init()
        {
            _currentShots =  Settings.Data.player.shooting.maxShotAmount;
            _currentLife = Settings.Data.game.maxLifeAmount;
            _enemiesToKill = Settings.Data.game.enemiesToKill;
            
            ShotAmountChanged?.Invoke(_currentShots);
            LifeAmountChanged?.Invoke(_currentLife);
            EnemiesAmountChanged?.Invoke(_enemiesToKill);
        }

        private void ReduceLife()
        {
            _currentLife--;
            LifeAmountChanged?.Invoke(_currentLife);
            if (_currentLife <= 0)
            {
                _stateMachine.SetState<DefeatState>();
            }
        }
        
        private void OnStateChanged(AState state)
        {
            if (state is RestartState)
            {
                ReduceLife();
            }
        }

        private void OnShotFired()
        {
            _currentShots--;
            ShotAmountChanged?.Invoke(_currentShots);
        }
        
        private void OnShotDestroyed()
        {
            _currentShots++;
            ShotAmountChanged?.Invoke(_currentShots);
        }
        
        private void OnEnemyKilled(Vector3 position)
        {
            _enemiesToKill--;
            EnemiesAmountChanged?.Invoke(_enemiesToKill);
            
            if (_enemiesToKill <= 0)
            {
                _stateMachine.SetState<VictoryState>();
            }
        }
    }
}
