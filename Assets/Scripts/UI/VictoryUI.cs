using Game;
using Scene;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI
{
    public class VictoryUI : AMenu
    {
        [SerializeField]
        private MenuAnimator _menuAnimator;
        [SerializeField]
        private Button _nextLevelButton;
        [SerializeField]
        private Button _mainMenuButton;
        
        private void Awake()
        {
            _nextLevelButton.onClick.AddListener(OnNextLevelClicked());
            _mainMenuButton.onClick.AddListener(OnMainMenuClicked());
        }

        private UnityAction OnNextLevelClicked()
        {
            void NextLevel()
            {
                _menuAnimator.Hide();
                SceneLoader.StartGame();
            }

            return NextLevel;
        }
        
        private UnityAction OnMainMenuClicked()
        {
            void MainMenu()
            {
                _menuAnimator.Hide();
                SceneLoader.LoadMainMenu();
            }

            return MainMenu;
        }
        
        public override void Show()
        {
            _menuAnimator.Show();
        }

        public override void Hide()
        {
            _menuAnimator.Hide();
        }
    }
}
