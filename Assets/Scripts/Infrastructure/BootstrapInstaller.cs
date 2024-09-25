using Data.Loaders;
using Game;
using Spawners;
using States;
using UI;
using UnityEngine;
using Utils.CoroutineUtils;
using Utils.ObjectPool;
using Zenject;

namespace Infrastructure
{
    public class BootstrapInstaller : MonoInstaller
    {
        [SerializeField] private EnemySpawner _enemySpawner;
        [SerializeField] private SwitchUI _switchUI;
        [SerializeField] private GameProvider _gameProvider;
        
        public override void InstallBindings()
        {
            _gameProvider.Load();

            var objectPool = new ObjectPool(Container);
            Container.Bind<ObjectPool>().FromInstance(objectPool).AsSingle();
            
            var coroutineLauncher = new CoroutineLauncher();
            Container.Bind<CoroutineLauncher>().FromInstance(coroutineLauncher).AsSingle();
            
            var stateMachine = new StateMachine(coroutineLauncher);
            Container.Bind<StateMachine>().FromInstance(stateMachine).AsSingle();
            Container.Bind<ITickable>().To<CoroutineLauncher>().FromResolve();

            Container.Bind<GameProvider>().FromInstance(_gameProvider).AsSingle();
            
            InitStateMachine(stateMachine);
        }
        
        private void InitStateMachine(StateMachine gameStateMachine)
        {
            gameStateMachine.AddState(new GameplayState(gameStateMachine, _enemySpawner));
            gameStateMachine.AddState(new WinState(gameStateMachine, _switchUI));
            gameStateMachine.AddState(new LoseState(gameStateMachine, _switchUI));
            
            gameStateMachine.SetState<GameplayState>();
        }
    }
}