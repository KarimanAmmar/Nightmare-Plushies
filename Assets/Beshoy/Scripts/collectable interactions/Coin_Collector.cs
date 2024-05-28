using System;
using UnityEngine;

public class Coin_Collector : MonoBehaviour
{
    /// <summary>
    /// to do :
    /// coin count 
    /// event listenner
    /// method used to increase number of coins when listen to event
    /// method and event to display the current number of coins in UI 
    /// new things added:
    /// switching the coin event from game event to int event
    /// adding the upgrade system event so that
    /// when the player collect enough points it will activate the upgrade system
    /// </summary>    
    [SerializeField] Float_event UiProgressBarEvent;
    [SerializeField] GameEvent Coin_event;
    [SerializeField] GameEvent UpgradeEvent;
    [SerializeField] private GameEvent ClearCountEvent;
    [SerializeField] float collectingRange;
    [SerializeField] float Upgrade_Value;//the required nuber of the score to activate the upgrade
    private float AmountUi;//for a progress UI to show the player how much left before leveling up(or the next upgrade)
    private Collider[] nearbycoins;
    private float Coins_Count;
    Vector3 pullPos;
    

    private void OnEnable()
    {
        Coin_event.GameAction+=Collect_Coin;
        ClearCountEvent.GameAction += ClearCount;

    }
    private void OnDisable()
    {
        Coin_event.GameAction-=Collect_Coin;
        ClearCountEvent.GameAction-= ClearCount;
    }
    private void Collect_Coin()
    {
        Coins_Count++;
        AmountUi = Coins_Count/Upgrade_Value;
        Logging.Log(Coins_Count);
        Logging.Log(Upgrade_Value);
        Logging.Log(AmountUi);
        UiProgressBarEvent.Raise(AmountUi);
        if(Coins_Count==Upgrade_Value)
        {
            UpgradeEvent.GameAction.Invoke();
        }
    }
    private void OnTriggerEnter(Collider other)
    {       //Logging.Log($"{other.gameObject.name}");
        if(other.gameObject.layer == GameConstant.MagneticLayer)
        {
            PullObject(other);

        }
            
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent<Rigidbody>(out Rigidbody rigidbody))
        {
            rigidbody.velocity = Vector3.zero;
            if (other.gameObject.layer == GameConstant.MagneticLayer)
            {
                PullObject(other);

            }

        }
        
    }
    private void PullObject(Collider other)
    {
        pullPos = transform.position;
        Vector3 dir = (pullPos - other.transform.position).normalized;
        other.gameObject.TryGetComponent<Collider>(out Collider component);
        component.attachedRigidbody.AddForce(dir * 10f,ForceMode.Impulse);

    }
    private void ClearCount()
    {
       Coins_Count = 0;
       Logging.Log("coin count returned to 0");
    }
}
