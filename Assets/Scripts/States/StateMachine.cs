using System;
using System.Collections;
using System.Collections.Generic;
using Utils;

namespace States
{
    public class StateMachine
    {
        private readonly Dictionary<Type, AState> _states = new();
        private AState _currentState;
        
        public StateMachine()
        {
            CoroutineLauncher.Play(Update());
        }

        ~StateMachine()
        {
            CoroutineLauncher.Stop(Update());
        }
        
        public void AddState<T>(T state) where T : AState
        {
            var type = typeof(T);

            if (_states.ContainsKey(type))
            {
                return;
            }
            
            _states.Add(type, state);
        }

        public void SetState<T>() where T : AState
        {
            _currentState?.Exit();
            _currentState = _states[typeof(T)];
            _currentState.Enter();
        }

        private IEnumerator Update()
        {
            while (true)
            {
                _currentState?.OnUpdate();
                yield return null;
            }
        }
    }
}
