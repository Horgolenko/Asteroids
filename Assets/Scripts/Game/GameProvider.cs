using Mob;
using States;
using UnityEngine;

namespace Game
{
    public class GameProvider : MonoBehaviour
    {
        private void Start()
        {
            StateMachine.OnStateChanged += OnStateChanged;
            MobBase.EnemyKilled += EnemyKilled;
        }

        private void OnDestroy()
        {
            StateMachine.OnStateChanged -= OnStateChanged;
            MobBase.EnemyKilled -= EnemyKilled;
        }

        private void OnStateChanged(AState state)
        {
            switch (state)
            {
                case WinState:
                    LevelPassed();
                    Save();
                    break;
                case LoseState:
                    Save();
                    break;
            }
        }

        private void LevelPassed()
        {

        }

        private void EnemyKilled()
        {

        }
        
        public void Load()
        {

        }

        private void Save()
        {

        }
    }
}
