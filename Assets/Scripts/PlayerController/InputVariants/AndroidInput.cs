using Enums;
using UnityEngine;
using Utils.UI;

namespace PlayerController.InputVariants
{
    public class AndroidInput : AInput
    {
        private bool _pressed;
        private GamepadDirection _movementDirection;
        private GamepadDirection _rotationDirection;
        private Touch _touch;
        
        public AndroidInput()
        {
            PressDetector.ButtonPressed += OnButtonPressed;
        }

        ~AndroidInput()
        {
            PressDetector.ButtonPressed -= OnButtonPressed;
        }

        public override float Horizontal()
        {
            if (!_pressed) return 0;
            
            if (_rotationDirection == GamepadDirection.Right)
            {
                return 1;
            }
            
            if (_rotationDirection == GamepadDirection.Left)
            {
                return -1;
            }

            return 0;
        }
        
        public override float Vertical()
        {
            if (!_pressed) return 0;
            
            if (_movementDirection == GamepadDirection.Up)
            {
                return 1;
            }

            if (_movementDirection == GamepadDirection.Down)
            {
                return -1;
            }

            return 0;
        }

        public override bool Shoot()
        {
            if (Input.touchCount >= 1)
            {
                _touch = Input.GetTouch(0);
                return _touch.phase == TouchPhase.Began && Input.touchCount == 2;
            }

            return false;
        }
        
        private void OnButtonPressed(GamepadDirection direction, bool pressed)
        {
            if (pressed)
            {
                switch (direction)
                {
                    case GamepadDirection.Up or GamepadDirection.Down:
                        _movementDirection = direction;
                        break;
                    case GamepadDirection.Left or GamepadDirection.Right:
                        _rotationDirection = direction;
                        break;
                }
            }
            else
            {
                switch (direction)
                {
                    case GamepadDirection.Up or GamepadDirection.Down:
                        _movementDirection = GamepadDirection.None;
                        break;
                    case GamepadDirection.Left or GamepadDirection.Right:
                        _rotationDirection = GamepadDirection.None;
                        break;
                }
            }
            
            _pressed = pressed;
        }
    }
}
