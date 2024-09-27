using Game;
using TMPro;
using UI.Menus.Abstract;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Menus
{
    public class TopMainMenu : AMenu
    {
        [SerializeField] private Image[] _shots;
        [SerializeField] private Image[] _lifes;
        [SerializeField] private TextMeshProUGUI _enemiesAmountLabel;

        private void Awake()
        {
            GameProvider.LifeAmountChanged += OnLifeAmountChanged;
            GameProvider.ShotAmountChanged += OnShotAmountChanged;
            GameProvider.EnemiesAmountChanged += OnEnemiesAmountChanged;
        }

        private void OnDestroy()
        {
            GameProvider.LifeAmountChanged -= OnLifeAmountChanged;
            GameProvider.ShotAmountChanged -= OnShotAmountChanged;
            GameProvider.EnemiesAmountChanged -= OnEnemiesAmountChanged;
        }

        private void OnLifeAmountChanged(int amount)
        {
            _lifes[amount].enabled = false;
        }

        private void OnShotAmountChanged(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                _shots[i].enabled = true;
            }

            for (int i = amount; i < _shots.Length; i++)
            {
                _shots[i].enabled = false;
            }
        }
        
        private void OnEnemiesAmountChanged(int amount)
        {
            _enemiesAmountLabel.text = $"{amount}";
        }
    }
}
