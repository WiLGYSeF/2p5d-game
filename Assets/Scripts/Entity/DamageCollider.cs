using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EntityNS {
    public class DamageCollider : MonoBehaviour {
        public float Lifetime;

        public float Damage {
            get => _weapon.Damage;
        }

        private Weapon _weapon;
        private float _awoken;

        private void Awake() {
            _awoken = Time.realtimeSinceStartup;
            StartCoroutine(KillCoroutine());
        }

        public void Init(Weapon weapon) {
            _weapon = weapon;
        }

        IEnumerator KillCoroutine() {
            while (Time.realtimeSinceStartup - _awoken < Lifetime) {
                yield return null;
            }
            Destroy(gameObject);
        }
    }
}