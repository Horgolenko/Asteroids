using UnityEngine;

namespace UI
{
    public class SwitchUI : MonoBehaviour
    {
        [SerializeField]
        private SettingsUI _settingsUI;
        [SerializeField]
        private VictoryUI _victoryUI;
        [SerializeField]
        private DefeatUI _defeatUI;
        
        public SettingsUI settingsUI => _settingsUI;
        public VictoryUI victoryUI => _victoryUI;
        public DefeatUI defeatUI => _defeatUI;
    }
}
