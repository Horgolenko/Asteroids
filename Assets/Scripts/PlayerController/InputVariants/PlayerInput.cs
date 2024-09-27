using UnityEngine;

namespace PlayerController.InputVariants
{
    public class PlayerInput : MonoBehaviour
    {
        [SerializeField] private GameObject _menuPrefab;

        private AInput _input;
        
        private void Awake()
        {
#if !UNITY_EDITOR
            Instantiate(_menuPrefab);
            _input = new AndroidInput();
#else
            _input = new PCInput();
#endif
        }
        
        public float Horizontal()
        {
            return _input.Horizontal();
        }
        
        public float Vertical()
        {
            return _input.Vertical();
        }
        
#if UNITY_EDITOR
        public Vector3 MousePosition()
        {
            return Input.mousePosition;
        }
#endif
        
        public bool Shoot()
        {
            return _input.Shoot();
        }
    }
}
