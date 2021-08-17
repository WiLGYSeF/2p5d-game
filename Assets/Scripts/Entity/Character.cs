using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EntityNS {
    public class Character : Entity {
        public float MovementSpeed = 3f;
        public float JumpSpeed = 10f;
        public int MidAirJumps = 0;
        public float Gravity = -30f;

        public EquipmentInventory Inventory {
            get => _inventory;
        }
        private EquipmentInventory _inventory;

        public bool IsGrounded {
            get => _isGrounded;
        }

        public bool OnEnvironment {
            get => _onEnvironment;
        }

        protected Vector3 _inputMove;
        protected Vector3 _inputJump;

        protected int _midAirJumps = 0;
        protected bool _jumping = false;

        private Vector3 _moveTo;
        private bool _doMove;

        private bool _isGrounded = false;

        private bool _onEnvironment = false;

        private int _maskGround;
        private int _maskEnvironment;

        protected override void Awake() {
            base.Awake();

            _inventory = GetComponent<EquipmentInventory>();
            if (_inventory && _inventory.ActiveWeapon) {
                _inventory.ActiveWeapon.Owner = this;
            }

            _maskGround = LayerMask.NameToLayer("Ground");
            _maskEnvironment = LayerMask.NameToLayer("Environment");
        }

        protected virtual void Update() {
            if (_doMove) {
                Vector3 diff = (_moveTo - transform.position).normalized;
                if (diff != Vector3.zero) {
                    _inputMove.x = diff.x;
                    _inputMove.z = diff.z;
                } else {
                    _doMove = false;
                }
            }

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

        public void MoveTo(Vector3 pos) {
            _moveTo = pos;
            _doMove = true;
        }

        protected virtual void OnCollisionEnter(Collision collision) {
            if (collision.collider.gameObject.layer == _maskGround) {
                Ground();
                _jumping = false;
            }

            if (collision.collider.gameObject.CompareTag("Enemy")) {
                TakeDamage(20);
            }
        }

        protected virtual void OnCollisionStay(Collision collision) {
            if (collision.collider.gameObject.layer == _maskGround) {
                Ground();
            }
            if (collision.collider.gameObject.layer == _maskEnvironment) {
                _onEnvironment = true;
            }
        }

        protected virtual void OnCollisionExit(Collision collision) {
            if (collision.collider.gameObject.layer == _maskGround) {
                _isGrounded = false;
            }
            if (collision.collider.gameObject.layer == _maskEnvironment) {
                _onEnvironment = false;
            }
        }

        private void Ground() {
            _midAirJumps = 0;
            _isGrounded = true;
        }
    }
}