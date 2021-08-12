using UnityEngine;

using EventSystemNS;

namespace UINS {
    public class DamageNumberGenerator : MonoBehaviour {
        public Canvas CanvasObject;
        public DamageNumber DamageNumberPrefab;

        private void OnEntityDamage(System.EventArgs args) {
            EntityDamageEvent evt = args as EntityDamageEvent;
            DamageNumber num = Instantiate(
                DamageNumberPrefab,
                Camera.main.WorldToScreenPoint(evt.Entity.transform.position),
                Quaternion.identity,
                CanvasObject.transform
            );
            num.Text = evt.Damage.ToString();
        }

        private void OnEnable() {
            EventManager.AddListener(EntityDamageEvent.NAME, OnEntityDamage);
        }

        private void OnDisable() {
            EventManager.RemoveListener(EntityDamageEvent.NAME, OnEntityDamage);
        }
    }
}
