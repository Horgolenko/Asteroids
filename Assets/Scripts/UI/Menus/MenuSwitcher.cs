using System.Collections;
using System.Collections.Generic;
using Data.Loaders;
using Enums;
using UI.Menus.Abstract;
using UnityEngine;

namespace UI.Menus
{
    public class MenuSwitcher : MonoBehaviour
    {
        private readonly Dictionary<MenuType, ABaseMenu> _menus = new();

        private void Awake()
        {
            StartCoroutine(LoadCoroutine());
        }

        private IEnumerator LoadCoroutine()
        {
            yield return null;
            
            var menus = MenuLoader.GetMenuDatas();
            for (int i = 0; i < menus.Length; i++)
            {
                var menu = Instantiate(menus[i].menu, transform);
                if (menu is ABaseMenu baseMenu)
                {
                    _menus[menus[i].menuType] = baseMenu;
                }
            }
        }

        public void Show(MenuType menuType)
        {
            if (_menus.TryGetValue(menuType, out var menu))
            {
                menu.Show();
            }
        }

        public void Hide(MenuType menuType)
        {
            if (_menus.TryGetValue(menuType, out var menu))
            {
                menu.Hide();
            }
        }
    }
}
