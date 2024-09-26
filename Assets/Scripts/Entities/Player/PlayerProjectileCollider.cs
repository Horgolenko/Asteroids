using System;
using Interfaces;
using UnityEngine;

namespace Entities.Player
{
    public class PlayerProjectileCollider : MonoBehaviour
    {
        public Action HitWall;
        
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent(typeof(IUndestroyable), out var undestroyable))
            {
                HitWall?.Invoke();
            }
            else if (collision.gameObject.TryGetComponent(typeof(IDamageable), out var damageable))
            {
                var enemy = (IDamageable)damageable;
                enemy.Damage();
            }
        }
    }
}
