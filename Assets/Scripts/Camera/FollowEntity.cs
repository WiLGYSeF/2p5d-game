using UnityEngine;

using EntityNS;

namespace CameraNS {
    public class FollowEntity : MonoBehaviour {
        public Entity EntityToFollow;
        public float CameraSpeed = 5f;

        [Range(0, 1)]
        public float LeftMarginPercentage;
        [Range(0, 1)]
        public float RightMarginPercentage;
        [Range(0, 1)]
        public float TopMarginPercentage;
        [Range(0, 1)]
        public float BottomMarginPercentage;

        private Camera _camera;
        private Vector3 _target;

        private void Awake() {
            _camera = GetComponent<Camera>();

            _target = transform.position;
        }

        private void Update() {
            Vector3 point = _camera.WorldToScreenPoint(EntityToFollow.transform.position);
            if (float.IsInfinity(point.z)) {
                return;
            }

            float leftPc = point.x / Screen.width;
            float rightPc = 1 - leftPc;
            float downPc = point.y / Screen.height;
            float upPc = 1 - downPc;

            _target = transform.position;
            if (leftPc < LeftMarginPercentage) {
                _target.z -= (LeftMarginPercentage - leftPc) * Screen.width / point.z;
            } else
            if (rightPc < RightMarginPercentage) {
                _target.z += (RightMarginPercentage - rightPc) * Screen.width / point.z;
            } else
            if (upPc < TopMarginPercentage) {
                _target.x -= (TopMarginPercentage - upPc) * Screen.height / point.z;
            } else
            if (downPc < BottomMarginPercentage) {
                _target.x += (BottomMarginPercentage - downPc) * Screen.height / point.z;
            }

            transform.position = Vector3.Lerp(transform.position, _target, Time.deltaTime * CameraSpeed);
        }
    }
}