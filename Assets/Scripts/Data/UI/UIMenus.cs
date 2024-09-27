using UnityEngine;

namespace Data.UI
{
    [CreateAssetMenu(fileName = "UIMenus", menuName = "ScriptableObjects/UIMenus", order = 3)]
    public class UIMenus : ScriptableObject
    {
        [SerializeField] private MenuData[] _menus;
        
        public MenuData[] menus => _menus;
    }
}
