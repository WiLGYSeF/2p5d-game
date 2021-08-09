using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using EntityNS;

namespace CameraNS {
    public class FollowEntity : MonoBehaviour {
        public Entity EntityToFollow;
        public float CameraSpeed = 5f;

        [Range(0, 1)]
        public float LeftPercentage;
        [Range(0, 1)]
        public float RightPercentage;
        [Range(0, 1)]
        public float UpPercentage;
        [Range(0, 1)]
        public float DownPercentage;

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
            if (leftPc < LeftPercentage) {
                _target.z -= (LeftPercentage - leftPc) * Screen.width / point.z;
            } else
            if (rightPc < RightPercentage) {
                _target.z += (RightPercentage - rightPc) * Screen.width / point.z;
            } else
            if (upPc < UpPercentage) {
                _target.x -= (UpPercentage - upPc) * Screen.height / point.z;
            } else
            if (downPc < DownPercentage) {
                _target.x += (DownPercentage - downPc) * Screen.height / point.z;
            }

            transform.position = Vector3.Lerp(transform.position, _target, Time.deltaTime * CameraSpeed);
        }
    }
}