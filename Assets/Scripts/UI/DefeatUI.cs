using Game;
using Scene;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI
{
    public class DefeatUI : AMenu
    {
        [SerializeField]
        private MenuAnimator _menuAnimator;
        [SerializeField]
        private Button _restartButton;
        [SerializeField]
        private Button _mainMenuButton;
        
        private void Awake()
        {
            _restartButton.onClick.AddListener(OnRestartClicked());
            _mainMenuButton.onClick.AddListener(OnMainMenuClicked());
        }

        private UnityAction OnRestartClicked()
        {
            void Restart()
            {
                _menuAnimator.Hide();
                SceneLoader.StartGame();
            }

            return Restart;
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
