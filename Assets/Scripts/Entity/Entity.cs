using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EntityNS {
    public class Entity : MonoBehaviour {
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

        // Entity health pool variables
        public int maxHealh = 100;
        public int currentHealth;
        public HealthBar healthBar;
        public bool debug = true;

		protected virtual void Awake() {
			_maskGround = LayerMask.NameToLayer("Ground");
            _inventory = GetComponent<EquipmentInventory>();
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

        private void Start() {
            currentHealth = maxHealh;
            healthBar.SetMaxHealth(maxHealh);
        }

        private void TakeDamage( int damage ) {
            currentHealth -= damage;
            healthBar.SetHealth(currentHealth);
        }
    }
}
