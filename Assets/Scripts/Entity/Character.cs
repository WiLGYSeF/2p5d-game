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

        protected int _midAirJumps = 0;
        protected bool _jumping = false;

        private bool _isGrounded = false;

        private int _maskGround;

        protected override void Awake() {
            base.Awake();

			_maskGround = LayerMask.NameToLayer("Ground");
            _inventory = GetComponent<EquipmentInventory>();
            if (_inventory && _inventory.ActiveWeapon) {
                _inventory.ActiveWeapon.Owner = this;
            }
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
        }

        protected virtual void OnCollisionExit(Collision collision) {
            if (collision.collider.gameObject.layer == _maskGround) {
                _isGrounded = false;
            }
        }

        private void Ground() {
            _midAirJumps = 0;
            _isGrounded = true;
		}
    }
}