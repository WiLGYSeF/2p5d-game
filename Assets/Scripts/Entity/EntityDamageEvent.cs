using EntityNS;

namespace EventSystemNS {
    public class EntityDamageEvent : System.EventArgs {
        public const string NAME = "EntityDamageEvent";

        public Entity Entity {get; set;}
        public float Damage {get; set;}

        public EntityDamageEvent(Entity entity, float damage) {
            Entity = entity;
            Damage = damage;
        }
    }
}
