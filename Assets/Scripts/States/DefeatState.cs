using Enums;
using Spawners;
using UI.Menus;
using Utils.ObjectPool;

namespace States
{
    public class DefeatState : AState
    {
        private readonly MenuSwitcher _menuSwitcher;
        private readonly ObjectPool _objectPool;
        private readonly SpawnProvider _spawnProvider;
        
        public DefeatState(StateMachine stateMachine, MenuSwitcher menuSwitcher, ObjectPool objectPool, SpawnProvider spawnProvider) : base(stateMachine)
        {
            _menuSwitcher = menuSwitcher;
            _objectPool = objectPool;
            _spawnProvider = spawnProvider;
        }

        public override void Enter()
        {
            _menuSwitcher.Show(MenuType.Defeat);
            _spawnProvider.StopEnemySpawn();
            _objectPool.Dispose();
        }

        public override void OnUpdate()
        {
            
        }

        public override void Exit()
        {
            
        }
    }
}
