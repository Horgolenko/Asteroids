using System;
using System.Collections.Generic;
using Utils;
using Utils.CoroutineUtils;

namespace States
{
    public class StateMachine
    {
        private readonly Dictionary<Type, AState> _states = new();
        private AState _currentState;
        private UpdateLine _update;
        private CoroutineLauncher _coroutineLauncher;

        public static Action<AState> OnStateChanged;
        
        public StateMachine(CoroutineLauncher coroutineLauncher)
        {
            _coroutineLauncher = coroutineLauncher;
            _update = new UpdateLine(Update, TimeUtils.FrameDelta);
            
            _coroutineLauncher.AddUpdate(_update);
        }

        ~StateMachine()
        {
            _coroutineLauncher.RemoveUpdate(_update);
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

            OnStateChanged?.Invoke(_currentState);
        }

        private void Update()
        {
            _currentState?.OnUpdate();
        }
    }
}