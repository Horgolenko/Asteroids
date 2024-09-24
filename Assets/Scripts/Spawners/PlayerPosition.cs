using Game;
using Mob;
using States;
using UnityEngine;
using Zenject;

namespace Spawners
{
    public class PlayerPosition : MonoBehaviour
    {
        public static PlayerPosition Instance;

        private StateMachine _stateMachine;

        [Inject]
        private void Construct(StateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }
        
        private void Start()
        {
            Instance = this;
        }

        public Vector3 Position()
        {
            return transform.position;
        }

        private void OnTriggerEnter(Collider other)
        {
            var mobBase = other.GetComponent<MobBase>();
            if (mobBase == null) return;

            if (mobBase.mobData.sideType == SideType.Enemy)
            {
                _stateMachine.SetState<LoseState>();
            }
        }
    }
}
