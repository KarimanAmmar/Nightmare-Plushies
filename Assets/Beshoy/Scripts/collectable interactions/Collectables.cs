using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectables : MonoBehaviour
{
    [SerializeField] GameEvent Collectable_event;
    [SerializeField] Collider colliderTobeactive;

    private void OnTriggerEnter(Collider Other)
    {
        if (Other.gameObject.tag == GameConstant.PlayerTag)
        {
            Debug.Log(Other.gameObject.name);
            Collectable_event.GameAction.Invoke();
            Destroy(gameObject);

            if(colliderTobeactive != null)
            {
                colliderTobeactive.enabled = true;
            }
        }
    }
}
