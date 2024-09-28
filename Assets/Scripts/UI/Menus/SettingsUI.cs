using Scene;
using UI.Menus.Abstract;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI.Menus
{
    public class SettingsUI : ABaseMenu
    {
        [SerializeField] private Button _closeButton;
        [SerializeField] private Button _backButton;
        [SerializeField] private Button _continueButton;
        [SerializeField] private Button _mainMenuButton;
        
        protected override void Awake()
        {
            _closeButton.onClick.AddListener(OnCloseClicked());
            _backButton.onClick.AddListener(OnCloseClicked());
            _continueButton.onClick.AddListener(OnCloseClicked());
            _mainMenuButton.onClick.AddListener(OnMainMenuClicked());
            
            base.Awake();
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
    }
}
