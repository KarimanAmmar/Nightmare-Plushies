using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectables : MonoBehaviour
{
    [SerializeField] GameEvent Collectable_event;
    [SerializeField] Collider colliderTobeactive;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == GameConstant.PlayerTag)
        {
            // Logging.Log(other.gameObject.name);
            Collectable_event.GameAction.Invoke();
            Destroy(gameObject);

            if(colliderTobeactive != null)
            {
                colliderTobeactive.enabled = true;
            }
        }
    }
}
