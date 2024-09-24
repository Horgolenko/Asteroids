using Audio;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Utils.UI
{
    [RequireComponent(typeof(Button), typeof(ButtonAudio))]
    public class ButtonScale : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        private const float Duration = 0.2f;
        
        [SerializeField]
        private Vector3 _pressed = new(0.9f, 0.9f, 0.9f);

        private Tweener _tweener;
        
        public void OnPointerDown(PointerEventData eventData)
        {
            _tweener?.Kill();
            _tweener = transform.DOScale(_pressed, Duration);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _tweener?.Kill();
            transform.localScale = Vector3.one;
        }
    }
}
