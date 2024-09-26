using UnityEngine;

namespace PlayerController
{
    public class PlayerInput : MonoBehaviour
    {
        private Camera _camera;
        private PlayerMover _playerMover;
        private PlayerShooter _playerShooter;

        private void Awake()
        {
            _camera = Camera.main;
            _playerMover = GetComponent<PlayerMover>();
            _playerShooter = GetComponent<PlayerShooter>();
        }

        private void Update()
        {
            Move();
            Rotate();
            Shoot();
        }

        private void Move()
        {
            _playerMover.Move(Input.GetAxis("Vertical"));
        }

        private void Rotate()
        {
            var mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
            _playerMover.Rotate(mousePosition);
        }

        private void Shoot()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (_playerShooter.CanFire())
                {
                    _playerShooter.Fire();
                }
            }
        }
    }
}
