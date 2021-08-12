using UnityEngine;

namespace Utility {
    public abstract class Singleton<T> : MonoBehaviour where T: MonoBehaviour
    {
        public static T Instance {
            get {
                if (_instance == null) {
                    Singleton<T> singleton = FindObjectOfType<Singleton<T>>();
                    _instance = singleton.GetComponent<T>();

                    if (_instance == null) {
                        throw new System.InvalidOperationException();
                    }

                    singleton._initialized = true;
                    singleton.SingletonInit();
                }
                return _instance;
            }
        }
        protected static T _instance;

        public bool Initialized => _initialized;
        private bool _initialized;

        private void Awake() {
            T component = this.GetComponent<T>();
            if (_instance != null && _instance != component) {
                Destroy(this.gameObject);
                throw new System.InvalidOperationException();
            }

            if (_instance == null) {
                _instance = component;
                _initialized = true;
                SingletonInit();
            }
        }

        public abstract void SingletonInit();
        public abstract void SingletonDestroy();

        private void OnDestroy() {
            if (_instance != this.GetComponent<T>()) {
                SingletonDestroy();
                _instance = null;
                _initialized = false;
            }
        }
    }
}
