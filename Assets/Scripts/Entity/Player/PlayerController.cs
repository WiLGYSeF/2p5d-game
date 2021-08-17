using UnityEngine;
using UnityEngine.InputSystem;

namespace EntityNS {
    public class PlayerController : Character {
        private Input.InputControls _controls;

        protected override void Awake() {
            base.Awake();

            Team = 0;

            _controls = new Input.InputControls();
            _controls.Player.Move.started += OnMove;
            _controls.Player.Move.performed += OnMove;
            _controls.Player.Move.canceled += OnMove;

            _controls.Player.Jump.started += OnJump;

            _controls.Player.Fire.started += OnFire;
        }

        void OnMove(InputAction.CallbackContext context) {
            Vector2 input = context.action.ReadValue<Vector2>();
            _inputMove = new Vector3(-input.y, 0, input.x);

            if (_inputMove.z > 0) {
                transform.rotation = Quaternion.AngleAxis(0f, Vector3.up);
            } else
            if (_inputMove.z < 0) {
                transform.rotation = Quaternion.AngleAxis(180f, Vector3.up);
            }
        }

        void OnJump(InputAction.CallbackContext context) {
            if (!IsGrounded && _midAirJumps >= MidAirJumps) {
                return;
            }

            _inputJump = Vector3.up * JumpSpeed;
            _jumping = true;
            if (!IsGrounded) {
                _midAirJumps++;
            }
        }

        void OnFire(InputAction.CallbackContext context) {
            Inventory.ActiveWeapon.Use();
        }

        private void OnEnable() {
            _controls.Enable();
        }

        private void OnDisable() {
            _controls.Disable();
        }
    }
}
