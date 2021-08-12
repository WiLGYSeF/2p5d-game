using EntityNS;

namespace EventSystemNS {
    public class EntityDeathEvent : System.EventArgs {
        public const string NAME = "EntityDeathEvent";

        public Entity Entity {get; private set;}
        
        public EntityDeathEvent(Entity entity) {
            Entity = entity;
        }
    }
}
