using System;
using Interfaces;
using UnityEngine;

namespace VFX
{
    public class Effect : MonoBehaviour, IPoolable
    {
        private ParticleSystem[] _systems;

        public GameObject GameObject => gameObject;
        public event Action<IPoolable> Destroyed;

        private void Awake()
        {
            _systems = GetComponentsInChildren<ParticleSystem>();
            var main = _systems[0].main;
            main.stopAction = ParticleSystemStopAction.Callback;
        }

        public void Play()
        {
            for (int i = 0; i < _systems.Length; i++)
            {
                _systems[i].Play();
            }
        }

        public void Reset()
        {
            
        }
        
        private void OnParticleSystemStopped()
        {
            Destroyed?.Invoke(this);
        }
    }
}
