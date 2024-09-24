using Game;
using Scene;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI
{
    public class SettingsUI : AMenu
    {
        [SerializeField]
        private MenuAnimator _menuAnimator;
        [SerializeField]
        private Button _closeButton;
        [SerializeField]
        private Button _backButton;
        [SerializeField]
        private Button _continueButton;
        [SerializeField]
        private Button _mainMenuButton;
        
        private void Awake()
        {
            _closeButton.onClick.AddListener(OnCloseClicked());
            _backButton.onClick.AddListener(OnCloseClicked());
            _continueButton.onClick.AddListener(OnCloseClicked());
            _mainMenuButton.onClick.AddListener(OnMainMenuClicked());
        }

        private UnityAction OnCloseClicked()
        {
            void Close()
            {
                _menuAnimator.Hide();
            }

            return Close;
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
