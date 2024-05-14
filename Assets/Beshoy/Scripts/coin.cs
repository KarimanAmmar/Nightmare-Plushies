using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coin : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameEvent coin_event;
    void Start()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
           // Logging.Log(other.gameObject.name);
            coin_event.GameAction.Invoke();
        }

    }

}
