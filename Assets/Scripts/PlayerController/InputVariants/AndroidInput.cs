using Enums;
using UnityEngine;
using Utils.UI;

namespace PlayerController.InputVariants
{
    public class AndroidInput : AInput
    {
        private bool _pressed;
        private GamepadDirection _direction;
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
            
            if (_direction == GamepadDirection.Right)
            {
                return 1;
            }
            
            if (_direction == GamepadDirection.Left)
            {
                return -1;
            }

            return 0;
        }
        
        public override float Vertical()
        {
            if (!_pressed) return 0;
            
            if (_direction == GamepadDirection.Up)
            {
                return 1;
            }

            if (_direction == GamepadDirection.Down)
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
            _direction = direction;
            _pressed = pressed;
        }
    }
}
