using System;
using Interfaces;
using UnityEngine;

namespace Entities.Projectile
{
    public class ProjectileCollider : MonoBehaviour
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
