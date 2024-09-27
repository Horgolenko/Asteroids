using UnityEngine;

namespace UI.Menus.Abstract
{
    [RequireComponent(typeof(MenuAnimator))]
    public abstract class ABaseMenu : AMenu
    {
        protected MenuAnimator _menuAnimator;

        protected virtual void Awake()
        {
            _menuAnimator = GetComponent<MenuAnimator>();
        }

        public virtual void Show()
        {
            _menuAnimator.Show();
        }

        public virtual void Hide()
        {
            _menuAnimator.Hide();
        }
    }
}
