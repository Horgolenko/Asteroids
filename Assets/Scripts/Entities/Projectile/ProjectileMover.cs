using Entities.Abstract;
using UnityEngine;

namespace Entities.Projectile
{
    public class ProjectileMover : AMover
    {
        [SerializeField] private Transform _transform;
        
        public void StartMoving()
        {
            _active = true;
        }

        public void Stop()
        {
            _active = false;
            _rigidbody.velocity = Vector3.zero;
            _transform.localPosition = Vector3.zero;
        }
    }
}
