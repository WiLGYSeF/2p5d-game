using UnityEngine;

namespace EntityNS {
    public class Equipment : MonoBehaviour {
        public Entity Owner;

        public virtual void Init(Entity owner) {
            Owner = owner;
        }
    }
}
