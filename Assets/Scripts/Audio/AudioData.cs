using System;
using UnityEngine;

namespace Audio
{
    [Serializable]
    public class AudioData
    {
        [SerializeField]
        private SoundType _fileName;
        [SerializeField]
        private AudioClip _audioFile;

        public SoundType fileName => _fileName;
        public AudioClip audioFile => _audioFile;
    }
}
