using TMPro;
using UnityEngine;

namespace Mob
{
    public class Obstacle : MonoBehaviour, IDamageable
    {
        [SerializeField]
        private int _hitPoints;
        [SerializeField]
        private TextMeshProUGUI _text;
        [SerializeField]
        private GameObject _obstacle;
        [SerializeField]
        private ParticleSystem _particleSystem;

        private BoxCollider _collider;

        private void Awake()
        {
            _collider = GetComponent<BoxCollider>();
        }

        private void Start()
        {
            _text.text = $"{_hitPoints}";
        }

        public void Damage()
        {
            _hitPoints--;
            if (_hitPoints <= 0)
            {
                _collider.enabled = false;
                _particleSystem.Play();
                Destroy(_obstacle);
            }
            else
            {
                _text.text = $"{_hitPoints}";
            }
        }
    }
}
