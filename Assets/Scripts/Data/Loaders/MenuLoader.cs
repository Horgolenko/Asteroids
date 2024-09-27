using Data.UI;
using UnityEngine;

namespace Data.Loaders
{
    public static class MenuLoader
    {
        private const string MenusDataPath = "Data/UIMenus";
        
        public static MenuData[] GetMenuDatas()
        {
            var uiMenus = Resources.Load(MenusDataPath) as UIMenus;
            return uiMenus.menus;
        }
    }
}
