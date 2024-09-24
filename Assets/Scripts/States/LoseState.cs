using UI;

namespace States
{
    public class LoseState : AState
    {
        private readonly SwitchUI _switchUI;
        
        public LoseState(StateMachine stateMachine, SwitchUI switchUI) : base(stateMachine)
        {
            _switchUI = switchUI;
        }

        public override void Enter()
        {
            _switchUI.defeatUI.Show();
        }

        public override void OnUpdate()
        {
            
        }

        public override void Exit()
        {
            
        }
    }
}
