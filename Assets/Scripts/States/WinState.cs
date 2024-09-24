using Game;
using UI;

namespace States
{
    public class WinState : AState
    {
        private readonly SwitchUI _switchUI;
        
        public WinState(StateMachine stateMachine, SwitchUI switchUI) : base(stateMachine)
        {
            _switchUI = switchUI;
        }

        public override void Enter()
        {
            _switchUI.victoryUI.Show();
        }

        public override void OnUpdate()
        {
        
        }

        public override void Exit()
        {
        
        }
    }
}
