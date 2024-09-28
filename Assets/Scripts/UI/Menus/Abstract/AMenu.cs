using UnityEngine;

namespace UI.Menus.Abstract
{
    public abstract class AMenu : MonoBehaviour
    {
        protected MenuSwitcher _menuSwitcher;
        
        public void Init(MenuSwitcher menuSwitcher)
        {
            _menuSwitcher = menuSwitcher;
        }
    }
}
