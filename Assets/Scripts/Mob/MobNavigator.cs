using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Mob
{
    public class MobNavigator : MonoBehaviour
    {
        private const float DestinationOffset = 1F;
        
        private Vector3 _destination;
        private NavMeshAgent _navMeshAgent;
        private Coroutine _active;

        private void Awake()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
        }

        private void OnDestroy()
        {
            StopCoroutine();
        }

        public void Run(Vector3 destination)
        {
            _destination = destination;
            _navMeshAgent.enabled = true;
            _navMeshAgent.SetDestination(_destination);
            _active = StartCoroutine(RunCoroutine());
        }

        public void Stop()
        {
            StopCoroutine();
            _navMeshAgent.enabled = false;
        }
        
        private IEnumerator RunCoroutine()
        {
            while (true)
            {
                yield return null;

                if (IsDestinationReached())
                {
                    
                }
            }
        }
        
        private bool IsDestinationReached()
        {
            return Vector3.SqrMagnitude(transform.position - _destination) <= DestinationOffset;
        }

        private void StopCoroutine()
        {
            if (_active != null)
            {
                StopCoroutine(_active);
                _active = null;
            }
        }
    }
}
