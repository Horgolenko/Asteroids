using Data.Loaders;
using States;
using UnityEngine;

namespace Game
{
    public class GameProvider : MonoBehaviour
    {
        private void Start()
        {
            StateMachine.StateChanged += OnStateChanged;
        }

        private void OnDestroy()
        {
            StateMachine.StateChanged -= OnStateChanged;
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
            DataLoader.Load();
        }

        private void Save()
        {

        }
    }
}
