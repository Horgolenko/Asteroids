namespace States
{
    public abstract class AState
    {
        protected StateMachine _stateMachine;

        protected AState(StateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }
        
        public abstract void Enter();
        public abstract void OnUpdate();
        public abstract void Exit();
    }
}
