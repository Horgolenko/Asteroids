using System.Collections;
using UnityEngine;

namespace Utils
{
    public class CoroutineLauncher : MonoBehaviour
    {
        private static bool _isActive = true;
        private static CoroutineLauncher _instance;

        private static CoroutineLauncher I
        {
            get
            {
                if (!_isActive) return null;
                if (_instance == null)
                {
                    _instance = FindObjectOfType<CoroutineLauncher>();

                    if (_instance == null)
                    {
                        GameObject g = new GameObject("__CREATED__CoroutineLauncher");
                        _instance = g.AddComponent<CoroutineLauncher>();

                        g.transform.parent = null;
                        DontDestroyOnLoad(g);
                    }
                }

                return _instance;
            }
        }

        private void Awake()
        {
            if (!_instance)
            {
                _instance = this;
                _isActive = true;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void OnEnable()
        {
            if (_instance == this)
            {
                _isActive = true;
            }
        }

        private void OnDisable()
        {
            if (_instance == this)
            {
                _isActive = false;
            }
        }

        private void OnDestroy()
        {
            if(_instance != this) return;

            _instance = null;
            _isActive = false;

            StopAllCoroutines();
        }

        public static Coroutine Play(IEnumerator routine)
        {
            if (I)
            {
                return I.StartCoroutine(routine);
            }

            return null;
        }

        public static void Stop(IEnumerator routine)
        {
            I.StopCoroutine(routine);
        }
    }
}