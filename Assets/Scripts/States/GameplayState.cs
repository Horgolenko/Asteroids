using Spawners;

namespace States
{
    public class GameplayState : AState
    {
        private readonly EnemySpawner _enemySpawner;
        
        public GameplayState(StateMachine stateMachine, EnemySpawner enemySpawner) : base(stateMachine)
        {
            _enemySpawner = enemySpawner;
        }

        public override void Enter()
        {
            _enemySpawner.InitialSpawn();
        }

        public override void OnUpdate()
        {
        
        }

        public override void Exit()
        {
        
        }
    }
}
