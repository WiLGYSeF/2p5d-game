using UnityEngine;
using UnityEngine.InputSystem;

namespace Entity.Player {
	public class PlayerController : MonoBehaviour {
		public float MovementSpeed = 3f;
		public float JumpSpeed = 10f;
		public int MidAirJumps = 0;
		public float Gravity = -30f;

		public bool IsGrounded {
			get => _isGrounded;
		}
		
		private Input.InputControls _controls;

		private Rigidbody _rb;

		private Vector3 _inputMove;
		private Vector3 _inputJump;
		private bool _isGrounded = false;
		private int _midAirJumps = 0;
		private bool _jumping = false;

		private int _maskGround;

		private void Awake() {
			_rb = GetComponent<Rigidbody>();

			_controls = new Input.InputControls();
			_controls.Player.Move.started += OnMove;
			_controls.Player.Move.performed += OnMove;
			_controls.Player.Move.canceled += OnMove;

			_controls.Player.Jump.started += OnJump;

			_maskGround = LayerMask.NameToLayer("Ground");
		}

		private void Update() {
			Vector3 vel = _rb.velocity;
			Vector3 move = _inputMove * MovementSpeed;

			vel.x = move.x;
			vel.y += Gravity * Time.deltaTime;
			vel.z = move.z;

			if (!_jumping && IsGrounded) {
				vel.y = 0;
			}
			if (_inputJump != Vector3.zero) {
				vel.y = _inputJump.y;
				_inputJump = Vector3.zero;
			}

			_rb.velocity = vel;

			Debug.Log($"{_rb.velocity} {IsGrounded}");
		}

		void OnMove(InputAction.CallbackContext context) {
			Vector2 input = context.action.ReadValue<Vector2>();
			_inputMove = new Vector3(-input.y, 0, input.x);
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

		private void OnCollisionEnter(Collision collision) {
			if (collision.collider.gameObject.layer == _maskGround) {
				_midAirJumps = 0;
				_isGrounded = true;
				_jumping = false;
			}
		}

		private void OnCollisionExit(Collision collision) {
			if (collision.collider.gameObject.layer == _maskGround) {
				_isGrounded = false;
			}
		}

		private void OnEnable() {
			_controls.Enable();
		}

		private void OnDisable() {
			_controls.Disable();
		}
	}
}
