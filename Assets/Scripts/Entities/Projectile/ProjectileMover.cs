using Entities.Abstract;
using UnityEngine;

namespace Entities.Projectile
{
    public class ProjectileMover : AMover
    {
        public void StartMoving()
        {
            _active = true;
        }

        public void Stop()
        {
            _active = false;
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.transform.position = Vector3.zero;
        }
    }
}
