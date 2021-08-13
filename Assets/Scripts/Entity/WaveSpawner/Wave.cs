using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EntityNS {
    [System.Serializable]
    public class Wave {
        public EntitySpawn[] Entities;
        public SpawnCondition Condition;
    }

    [System.Serializable]
    public struct EntitySpawn {
        public Entity entity;
        public Transform transform;
    }

    [System.Serializable]
    public struct SpawnCondition {
        public GameObject trigger;
        public bool onWaveDone;
    }
}
