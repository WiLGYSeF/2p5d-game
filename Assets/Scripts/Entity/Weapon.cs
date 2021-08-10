using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EntityNS {
    [System.Serializable]
    public class Weapon : Equipment {
        public float Damage;
        public float Cooldown;

        public DamageCollider DamageCollider;
        public GameObject DamageColliderSpawner;

        private float _lastUse;

        public bool Use() {
            if (Time.realtimeSinceStartup - _lastUse < Cooldown) {
                return false;
            }

            Instantiate(DamageCollider, DamageColliderSpawner.transform.position, Quaternion.identity);
            _lastUse = Time.realtimeSinceStartup;
            return true;
        }
    }
}