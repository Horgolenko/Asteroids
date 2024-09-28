using System;
using System.Collections;
using UnityEngine;
using UnityEngine.VFX;

namespace Animation
{
    public class PlayerDissolve : MonoBehaviour
    {
        private const float DissolveRate = 0.05f;
        private static readonly int DissolveAmount = Shader.PropertyToID("_DissolveAmount");

        private Material _material;
        private Action _onComplete;
        private MeshRenderer _meshRenderer;
        private VisualEffect _visualEffect;
        private Coroutine _dissolveCoroutine;
        private readonly WaitForSeconds _waitForRefresh = new(0.04f);

        private void Awake()
        {
            _meshRenderer = GetComponent<MeshRenderer>();
            _material = _meshRenderer.material;
            _visualEffect = GetComponentInChildren<VisualEffect>();
        }

        public void Animate(Action onComplete)
        {
            _onComplete = onComplete;
            _visualEffect.Play();
            _dissolveCoroutine = StartCoroutine(DissolveCoroutine());
        }

        private IEnumerator DissolveCoroutine()
        {
            float value = 0;
            while (value < 1)
            {
                value += DissolveRate;
                _material.SetFloat(DissolveAmount, value);
                yield return _waitForRefresh;
            }

            _onComplete?.Invoke();
        }

        public void ResetAnimation()
        {
            if (_dissolveCoroutine != null)
            {
                StopCoroutine(_dissolveCoroutine);
                _dissolveCoroutine = null;
            }
            _visualEffect.Stop();
            _material.SetFloat(DissolveAmount, 0);
        }
    }
}
