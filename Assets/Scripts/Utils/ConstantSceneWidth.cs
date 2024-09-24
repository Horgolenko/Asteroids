using UnityEngine;

namespace Utils
{
    public class ConstantSceneWidth : MonoBehaviour
    {
        [SerializeField]
        private float _desiredSceneWidth = 10f;

        private Camera _camera;

        private void Start()
        {
            _camera = GetComponent<Camera>();
            AdjustCameraPosition();
        }

        private void AdjustCameraPosition()
        {
            float currentAspect = (float)Screen.width / Screen.height;

            float newCameraDistance = _desiredSceneWidth / (2f * Mathf.Tan(_camera.fieldOfView * 0.5f * Mathf.Deg2Rad) * currentAspect);

            var position = _camera.transform.position;
            _camera.transform.position = new Vector3(position.x, position.y, -newCameraDistance);
        }
    }
}