using Data.Loaders;
using Entities.Player;
using Utils;

namespace States
{
    public class RestartState : AState
    {
        private long _timestamp;
        
        public RestartState(StateMachine stateMachine) : base(stateMachine)
        {
            
        }

        public override void Enter()
        {
            _timestamp = TimeUtils.GetTimestamp();
        }

        public override void OnUpdate()
        {
            if (TimeUtils.GetTimestamp() - _timestamp > DataLoader.GetPlayerData().respawnDelay)
            {
                PlayerInstance.Instance.Respawn();
                _stateMachine.SetState<GameplayState>();
            }
        }

        public override void Exit()
        {
            
        }
    }
}
