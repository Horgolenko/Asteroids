using Spawners;

namespace States
{
    public class GamePrepareState : AState
    {
        private readonly SpawnProvider _spawnProvider;
        
        public GamePrepareState(StateMachine stateMachine, SpawnProvider spawnProvider) : base(stateMachine)
        {
            _spawnProvider = spawnProvider;
        }

        public override void Enter()
        {
            _spawnProvider.InitialSpawn();
            _stateMachine.SetState<GameplayState>();
        }

        public override void OnUpdate()
        {
        
        }

        public override void Exit()
        {
        
        }
    }
}
