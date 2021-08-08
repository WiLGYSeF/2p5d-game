using UnityEngine;

namespace Entity {
    public class Entity : MonoBehaviour {
        public float MovementSpeed = 3f;
        public float JumpSpeed = 10f;
        public int MidAirJumps = 0;
        public float Gravity = -30f;
    }
}
