using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Utils.UI;

namespace Audio
{
    [RequireComponent(typeof(Button), typeof(ButtonScale))]
    public class ButtonAudio : MonoBehaviour
    {
        [SerializeField]
        private SoundType _soundType = SoundType.Button;
        
        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(OnClicked());
        }
        
        private UnityAction OnClicked()
        {
            void Click()
            {
                AudioPlayer.Play(_soundType);

            }

            return Click;
        }
    }
}
