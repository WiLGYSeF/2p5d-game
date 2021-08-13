using UnityEngine;

using EntityNS;

namespace EventSystemNS {
    public class PlayerTriggerEnterEvent : System.EventArgs {
        public const string NAME = "PlayerTriggerEnterEvent";

        public Entity Entity {get; private set;}
        public GameObject Trigger {get; private set;}
        
        public PlayerTriggerEnterEvent(Entity entity, GameObject trigger) {
            Entity = entity;
            Trigger = trigger;
        }
    }
}
