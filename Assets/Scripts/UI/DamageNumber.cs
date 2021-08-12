using System.Collections;
using UnityEngine;
using TMPro;

namespace UINS {
    public class DamageNumber : MonoBehaviour {
        public float Speed;
        public float Lifetime;

        public string Text {
            get => TextObject.text;
            set => TextObject.text = value;
        }

        public TextMeshProUGUI TextObject;

        private float _awoken;

        private void Awake() {
            _awoken = Time.realtimeSinceStartup;
            StartCoroutine(KillCoroutine());
        }

        private void Update() {
            Vector3 pos = transform.position;
            pos.y += 1 * Speed * Time.deltaTime;
            transform.position = pos;
        }

        IEnumerator KillCoroutine() {
            while (Time.realtimeSinceStartup - _awoken < Lifetime) {
                yield return null;
            }
            Destroy(gameObject);
        }
    }
}