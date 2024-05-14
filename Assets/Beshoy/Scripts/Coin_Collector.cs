using System.Collections;
using System.Collections.Generic;
using Unity.Android.Gradle;
using UnityEngine;

public class Coin_Collector : MonoBehaviour
{
    /// <summary>
    /// to do :
    /// coin count 
    /// event listenner
    /// method used to increase number of coins when listen to event
    /// method and event to display the current number of coins in UI 
    /// </summary>    
    [SerializeField] int_Event UI_event;
    [SerializeField] GameEvent coin_event;
    [SerializeField] float collectingRange;
    private Collider[] nearbycoins;
    private int Coins_Count;
    Vector3 pullPos;
    

    private void OnEnable()
    {
        coin_event.GameAction += Collect_Coin;
        
    }
    private void OnDisable()
    {
        coin_event.GameAction -=Collect_Coin;
    }
    private void Collect_Coin()
    {
        Coins_Count++;
        UI_event.Raise(Coins_Count);
    }
    private void OnTriggerEnter(Collider other)
    {       //Logging.Log($"{other.gameObject.name}");
            PullObject(other);
    }
    private void OnTriggerExit(Collider other)
    {
        other.attachedRigidbody.velocity=Vector3.zero;
    }
    private void PullObject(Collider other)
    {
        pullPos = transform.position;
        Vector3 dir = (pullPos - other.transform.position).normalized;
        other.gameObject.TryGetComponent<Collider>(out Collider component);
        component.attachedRigidbody.AddForce(dir * 10f,ForceMode.Impulse);
    }
    // Update is called once per frame
}
