using Game;
using Mob;
using States;
using UI;
using UnityEngine;
using Utils.CoroutineUtils;
using Zenject;

namespace Infrastructure
{
    public class BootstrapInstaller : MonoInstaller
    {
        [SerializeField]
        private MobPool _mobPool;
        [SerializeField]
        private SwitchUI _switchUI;
        [SerializeField]
        private GameProvider _gameProvider;
        
        public override void InstallBindings()
        {
            Container.Bind<MobPool>().FromInstance(_mobPool).AsSingle();
            
            var coroutineLauncher = new CoroutineLauncher();
            Container.Bind<CoroutineLauncher>().FromInstance(coroutineLauncher).AsSingle();
            
            var stateMachine = new StateMachine(coroutineLauncher);
            Container.Bind<StateMachine>().FromInstance(stateMachine).AsSingle();
            Container.Bind<ITickable>().To<CoroutineLauncher>().FromResolve();

            _gameProvider.Load();
            
            Container.Bind<GameProvider>().FromInstance(_gameProvider).AsSingle();
            
            InitStateMachine(stateMachine);
        }
        
        private void InitStateMachine(StateMachine gameStateMachine)
        {
            gameStateMachine.AddState(new GameplayState(gameStateMachine));
            gameStateMachine.AddState(new WinState(gameStateMachine, _switchUI));
            gameStateMachine.AddState(new LoseState(gameStateMachine, _switchUI));
            
            gameStateMachine.SetState<GameplayState>();
        }
    }
}