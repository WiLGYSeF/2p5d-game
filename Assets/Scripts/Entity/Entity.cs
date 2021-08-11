using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EntityNS {
    public class Entity : MonoBehaviour {
        public int Team = 1;

         // Entity health pool variables
        public float MaxHealh = 100;
        public float CurrentHealth;
        public HealthBar HealthBarObject;

        protected virtual void Awake() {
            CurrentHealth = MaxHealh;
            if (HealthBarObject) {
                HealthBarObject.SetMaxHealth(MaxHealh);
            }
        }

        protected virtual void OnTriggerEnter(Collider collider) {
            DamageCollider dmgCollider = collider.gameObject.GetComponentInParent<DamageCollider>();
            if (dmgCollider != null && dmgCollider.Team != Team) {
                TakeDamage(dmgCollider.Damage);
            }
        }

        public void TakeDamage(float damage) {
            CurrentHealth -= damage;
            if (HealthBarObject) {
                HealthBarObject.SetHealth(CurrentHealth);
            }
        }
    }
}
