using System;
using Interfaces;
using UnityEngine;

namespace Entities.Enemy
{
    public class AsteroidCollider : MonoBehaviour
    {
        public Action<Vector3> ChangeDirection;

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent(typeof(IUndestroyable), out var undestroyable))
            {
                ChangeDirection?.Invoke(collision.contacts[0].normal);
            }
            else if (collision.gameObject.TryGetComponent(typeof(AsteroidCollider), out var asteroidCollider))
            {
                ChangeDirection?.Invoke(collision.contacts[0].normal);
            }
            else if (collision.gameObject.TryGetComponent(typeof(IDamageable), out var damageable))
            {
                var player = (IDamageable)damageable;
                player.Damage();
            }
        }
    }
}
