using System.Collections.Generic;
using UnityEngine;

namespace Audio
{
    public class AudioPlayer : MonoBehaviour
    {
        [SerializeField]
        private AudioData[] _audioDatas;
        [SerializeField]
        private AudioSource _musicSource;
        
        private static readonly Dictionary<SoundType, AudioClip> _audioClips = new();
        private static AudioSource _soundSource;

        private void Awake()
        {
            _soundSource = GetComponent<AudioSource>();
            
            for (int i = 0; i < _audioDatas.Length; i++)
            {
                _audioClips[_audioDatas[i].fileName] = _audioDatas[i].audioFile;
            }
        }

        public static void Play(SoundType audioName)
        {
            _soundSource.PlayOneShot(_audioClips[audioName]);
        }

        public void SetSoundVolume(float value)
        {
            _soundSource.volume = value;
        }
        
        public void SetMusicVolume(float value)
        {
            _musicSource.volume = value;
        }
    }
}
