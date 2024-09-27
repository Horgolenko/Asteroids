using Entities.Player;
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
        [SerializeField] private SpawnProvider _spawnProvider;
        [SerializeField] private SwitchUI _switchUI;
        [SerializeField] private GameProvider _gameProvider;
        
        public override void InstallBindings()
        {
            _gameProvider.Load();
            _spawnProvider.Init(Container);
            
            var objectPool = new ObjectPool(Container);
            Container.Bind<ObjectPool>().FromInstance(objectPool).AsSingle();
            
            var coroutineLauncher = new CoroutineLauncher();
            Container.Bind<CoroutineLauncher>().FromInstance(coroutineLauncher).AsSingle();
            
            var stateMachine = new StateMachine(coroutineLauncher);
            Container.Bind<StateMachine>().FromInstance(stateMachine).AsSingle();
            Container.Bind<ITickable>().To<CoroutineLauncher>().FromResolve();

            Container.Bind<GameProvider>().FromInstance(_gameProvider).AsSingle();
            Container.Bind<PlayerInstance>().FromComponentInHierarchy().AsSingle().Lazy();
            
            InitStateMachine(stateMachine);
        }
        
        private void InitStateMachine(StateMachine gameStateMachine)
        {
            gameStateMachine.AddState(new GamePrepareState(gameStateMachine, _spawnProvider));
            gameStateMachine.AddState(new GameplayState(gameStateMachine));
            gameStateMachine.AddState(new RestartState(gameStateMachine, _spawnProvider));
            gameStateMachine.AddState(new WinState(gameStateMachine, _switchUI));
            gameStateMachine.AddState(new LoseState(gameStateMachine, _switchUI));
            
            gameStateMachine.SetState<GamePrepareState>();
        }
    }
}