using System;
using Interfaces;
using UnityEngine;

namespace Entities.Projectile
{
    public class EnemyProjectileCollider : MonoBehaviour
    {
        public Action HitWall;
        
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent(typeof(IUndestroyable), out var undestroyable))
            {
                HitWall?.Invoke();
            }
        }
    }
}
