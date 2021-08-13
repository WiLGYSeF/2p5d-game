using UnityEngine;

using EntityNS;
using EventSystemNS;

public class PlayerTrigger : MonoBehaviour {
    private void OnTriggerEnter(Collider other) {
        Entity entity = other.GetComponentInParent<PlayerController>();
        if (entity == null || entity.Team != 0) {
            return;
        }

        EventManager.Dispatch(PlayerTriggerEnterEvent.NAME, new PlayerTriggerEnterEvent(entity, this.gameObject));
    }
}
