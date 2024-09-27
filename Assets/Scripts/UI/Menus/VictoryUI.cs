using Scene;
using UI.Menus.Abstract;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI.Menus
{
    public class VictoryUI : ABaseMenu
    {
        [SerializeField] private Button _mainMenuButton;
        
        protected override void Awake()
        {
            _mainMenuButton.onClick.AddListener(OnMainMenuClicked());
            
            base.Awake();
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
