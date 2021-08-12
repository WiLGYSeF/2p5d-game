using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using EventSystemNS;

namespace UINS {
    public class DamageNumbers : MonoBehaviour {
        private void OnEntityDamage(System.EventArgs args) {
            EntityDamageEvent evt = args as EntityDamageEvent;
            Debug.Log($"{evt.Entity} {evt.Damage}");
        }

        private void OnEnable() {
            EventManager.AddListener(EntityDamageEvent.NAME, OnEntityDamage);
        }

        private void OnDisable() {
            EventManager.RemoveListener(EntityDamageEvent.NAME, OnEntityDamage);
        }
    }
}
