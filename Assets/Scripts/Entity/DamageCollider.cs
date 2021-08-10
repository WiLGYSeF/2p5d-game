using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EntityNS {
    public class DamageCollider : MonoBehaviour {
        public float Lifetime;

        private float _awoken;

        private void Awake() {
            _awoken = Time.realtimeSinceStartup;
            StartCoroutine(KillCoroutine());
        }

        IEnumerator KillCoroutine() {
            while (Time.realtimeSinceStartup - _awoken < Lifetime) {
                yield return null;
            }
            Destroy(gameObject);
        }
    }
}