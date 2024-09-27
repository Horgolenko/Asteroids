using System;
using Enums;
using UI.Menus.Abstract;
using UnityEngine;

namespace Data.UI
{
    [Serializable]
    public class MenuData
    {
        [SerializeField] private MenuType _menuType;
        [SerializeField] private AMenu _menu;
        
        public MenuType menuType => _menuType;
        public AMenu menu => _menu;
    }
}
