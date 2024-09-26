using UnityEngine;

namespace Entities.Abstract
{
    public abstract class AMover : MonoBehaviour
    {
        private float _moveSpeed;
        protected Rigidbody _rigidbody;

        protected bool _active;
        protected Vector3 _direction;

        private void Awake()
        {
            _rigidbody = GetComponentInChildren<Rigidbody>();
        }

        private void FixedUpdate()
        {
            if (!_active) return;
            
            _rigidbody.velocity = _direction * (_moveSpeed * Time.fixedDeltaTime);
            Debug.Log($"_rigidbody.velocity = {_rigidbody.velocity}");
        }

        public void Init(float moveSpeed, Vector3 direction = default)
        {
            _moveSpeed = moveSpeed;
            _direction = direction;
        }
    }
}
