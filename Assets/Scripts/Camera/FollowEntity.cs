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

        private Camera _camera;
        private Vector3 _target;

        private void Awake() {
            _camera = GetComponent<Camera>();

            _target = transform.position;
        }

        private void Update() {
            Vector3 point = _camera.WorldToScreenPoint(EntityToFollow.transform.position);
            float leftPc = point.x / Screen.width;
            float rightPc = 1 - leftPc;

            if (leftPc < LeftPercentage) {
                _target = transform.position;
                _target.z -= (LeftPercentage - leftPc) * Screen.width / point.z;
            } else
            if (rightPc < RightPercentage) {
                _target = transform.position;
                _target.z += (RightPercentage - rightPc) * Screen.width / point.z;
            }

            transform.position = Vector3.Lerp(transform.position, _target, Time.deltaTime * CameraSpeed);
            
            Debug.Log($"{_target} {transform.position}");
        }
    }
}