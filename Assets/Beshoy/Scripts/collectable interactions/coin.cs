using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coin : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] int coinValue;
    [SerializeField] Float_event coin_event;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
           // Logging.Log(other.gameObject.name);
            coin_event.Raise(coinValue);
            Destroy(gameObject);
        }
    }
}
