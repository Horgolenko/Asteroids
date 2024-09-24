using DG.Tweening;
using UnityEngine;

namespace UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class MenuAnimator : MonoBehaviour
    {
        private const float Duration = 0.5f;

        [SerializeField]
        private bool _active;
        
        private CanvasGroup _canvasGroup;
        private Tweener _tweener;

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();

            if (!_active)
            {
                _canvasGroup.alpha = 0;
                _canvasGroup.blocksRaycasts = false;
            }
        }

        private void OnDestroy()
        {
            _tweener?.Kill();
        }

        public void Show()
        {
            gameObject.SetActive(true);
            _canvasGroup.blocksRaycasts = true;
            Animate(1);
        }

        public void Hide()
        {
            Animate(0);
            _canvasGroup.blocksRaycasts = false;
        }
        
        private void Animate(float endValue)
        {
            _tweener = DOTween.To(() => _canvasGroup.alpha, x => _canvasGroup.alpha = x, endValue, Duration);
        }
    }
}
