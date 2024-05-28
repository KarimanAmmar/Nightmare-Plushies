using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coin : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameEvent coin_event;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == GameConstant.PlayerTag)
        {
           // Logging.Log(other.gameObject.name);
            coin_event.GameAction.Invoke();
            Destroy(gameObject);
        }
    }
}
