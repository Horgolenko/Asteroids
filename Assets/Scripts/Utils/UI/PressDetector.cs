using System;
using Enums;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Utils.UI
{
    public class PressDetector : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private GamepadDirection _direction;

        public static Action<GamepadDirection, bool> ButtonPressed;

        public void OnPointerDown(PointerEventData eventData)
        {
            ButtonPressed?.Invoke(_direction, true);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            ButtonPressed?.Invoke(_direction, false);
        }
    }
}
