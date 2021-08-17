using UnityEngine;
using UnityEngine.InputSystem;

namespace EntityNS {
    public class PlayerController : Character {
        private Input.InputControls _controls;

        private Vector3 _inputMove;
        private Vector3 _inputJump;

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

        protected override void Update() {
            Vector3 vel = _rb.velocity;
            Vector3 move = _inputMove * MovementSpeed;

            if (IsGrounded || !OnEnvironment) {
                vel.x = move.x;
                vel.z = move.z;
            }
            vel.y += Gravity * Time.deltaTime;

            if (!_jumping && IsGrounded) {
                vel.y = 0;
            }
            if (_inputJump != Vector3.zero) {
                vel.y = _inputJump.y;
                _inputJump = Vector3.zero;
            }

            _rb.velocity = vel;
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
