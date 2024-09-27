using Data.Loaders;
using Spawners;
using Utils;

namespace States
{
    public class RestartState : AState
    {
        private readonly SpawnProvider _spawnProvider;
        private long _timestamp;
        
        public RestartState(StateMachine stateMachine, SpawnProvider spawnProvider) : base(stateMachine)
        {
            _spawnProvider = spawnProvider;
        }

        public override void Enter()
        {
            _timestamp = TimeUtils.GetTimestamp();
        }

        public override void OnUpdate()
        {
            if (TimeUtils.GetTimestamp() - _timestamp > DataLoader.GetPlayerData().respawnDelay)
            {
                _spawnProvider.RespawnPlayer();
                _stateMachine.SetState<GameplayState>();
            }
        }

        public override void Exit()
        {
            
        }
    }
}
