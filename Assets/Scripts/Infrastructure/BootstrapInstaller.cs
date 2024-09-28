using Entities.Player;
using Game;
using PlayerController.InputVariants;
using Spawners;
using States;
using UI.Menus;
using UnityEngine;
using Utils.CoroutineUtils;
using Utils.ObjectPool;
using Zenject;

namespace Infrastructure
{
    public class BootstrapInstaller : MonoInstaller
    {
        [SerializeField] private PlayerInstance _player;
        [SerializeField] private PlayerInput _playerInput;
        [SerializeField] private SpawnProvider _spawnProvider;
        [SerializeField] private MenuSwitcher _menuSwitcher;
        [SerializeField] private GameProvider _gameProvider;
        
        public override void InstallBindings()
        {
            _spawnProvider.Init();
            Container.Bind<SpawnProvider>().FromInstance(_spawnProvider).AsSingle();
            Container.Bind<PlayerInstance>().FromComponentInNewPrefab(_player).AsSingle();
            Container.Bind<PlayerInput>().FromComponentInNewPrefab(_playerInput).AsSingle();
            
            Container.Bind<GameProvider>().FromInstance(_gameProvider).AsSingle();
            
            var objectPool = new ObjectPool(Container);
            Container.Bind<ObjectPool>().FromInstance(objectPool).AsSingle();
            
            var coroutineLauncher = new CoroutineLauncher();
            Container.Bind<CoroutineLauncher>().FromInstance(coroutineLauncher).AsSingle();
            Container.Bind<ITickable>().To<CoroutineLauncher>().FromResolve();

            var stateMachine = new StateMachine(coroutineLauncher);
            Container.Bind<StateMachine>().FromInstance(stateMachine).AsSingle();
            var menuSwitcher = Instantiate(_menuSwitcher);
            InitStateMachine(stateMachine, menuSwitcher, objectPool);
        }
        
        private void InitStateMachine(StateMachine gameStateMachine, MenuSwitcher menuSwitcher, ObjectPool objectPool)
        {
            gameStateMachine.AddState(new GamePrepareState(gameStateMachine, _spawnProvider));
            gameStateMachine.AddState(new GameplayState(gameStateMachine));
            gameStateMachine.AddState(new RestartState(gameStateMachine));
            gameStateMachine.AddState(new VictoryState(gameStateMachine, menuSwitcher, objectPool, _spawnProvider));
            gameStateMachine.AddState(new DefeatState(gameStateMachine, menuSwitcher, objectPool, _spawnProvider));
            
            gameStateMachine.SetState<GamePrepareState>();
        }
    }
}