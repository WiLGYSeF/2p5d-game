using UnityEngine;
using UnityEngine.InputSystem;

namespace Entity.Player {
	public class PlayerController : MonoBehaviour {
		public float MovementSpeed = 3f;
		
		private Input.InputControls _controls;

		private Rigidbody _rb;

		private Vector3 _playerMoveInput;

		private void Awake() {
			_rb = GetComponent<Rigidbody>();

			_controls = new Input.InputControls();
			_controls.Player.Move.started += OnMove;
			_controls.Player.Move.performed += OnMove;
			_controls.Player.Move.canceled += OnMove;
		}

		private void Update() {
			_rb.velocity = _playerMoveInput * MovementSpeed;
		}

		void OnMove(InputAction.CallbackContext context) {
			Vector2 input = context.action.ReadValue<Vector2>();
			_playerMoveInput = new Vector3(-input.y, 0, input.x);
		}

		private void OnEnable() {
			_controls.Enable();
		}

		private void OnDisable() {
			_controls.Disable();
		}
	}
}
