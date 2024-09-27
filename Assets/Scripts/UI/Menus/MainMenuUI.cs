using Scene;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI.Menus
{
    public class MainMenuUI : MonoBehaviour
    {
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _quitButton;

        private void Awake()
        {
            _playButton.onClick.AddListener(OnPlayClicked());
            _quitButton.onClick.AddListener(OnQuitClicked());
        }

        private UnityAction OnPlayClicked()
        {
            void Play()
            {
                SceneLoader.StartGame();
            }

            return Play;
        }

        private UnityAction OnQuitClicked()
        {
            void Quit()
            {
                SceneLoader.Quit();
            }

            return Quit;
        }
    }
}
