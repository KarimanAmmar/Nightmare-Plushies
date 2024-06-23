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
    [SerializeField] int_Event Value_Event;
    [SerializeField] private GameEvent ClearCountEvent;
    [SerializeField] private AudioClip collectsfx;
   // [SerializeField] float collectingRange;
    [SerializeField] int Upgrade_Value;//the required nuber of the score to activate the upgrade
    [SerializeField] int Available_Upgrades;
    private int Upgrade_Count = 0;
    private float AmountUi;//for a progress UI to show the player how much left before leveling up(or the next upgrade)
   // private Collider[] nearbycoins;
    private float Coins_Count;
   // Vector3 pullPos;
    
    private void OnEnable()
    {
        Coin_event.GameAction+=Collect_Coin;
        ClearCountEvent.GameAction += ClearCount;
        Value_Event.RegisterListener(UpdateValue);

    }
    private void OnDisable()
    {
        Coin_event.GameAction-=Collect_Coin;
        ClearCountEvent.GameAction-= ClearCount;
        Value_Event.UnregisterListener(UpdateValue);
    }
    private void Collect_Coin()
    {
        if (Upgrade_Count != Available_Upgrades)
        {         
            
            Coins_Count++;
            
            AmountUi = Coins_Count / (Upgrade_Value/Available_Upgrades);
            AudioManager.Instance.PlySfx(collectsfx);
            UiProgressBarEvent.Raise(AmountUi);
            if (Coins_Count==(Upgrade_Value/Available_Upgrades))
            {
                Upgrade_Count++;
                
                UpgradeEvent.GameAction.Invoke();
                Coins_Count = 0;
                AmountUi = Coins_Count / (Upgrade_Value / Available_Upgrades);
                UiProgressBarEvent.Raise(AmountUi);
            }

        }
        

        
    }
    private void ClearCount()
    {
       Coins_Count = 0;
       AmountUi = Coins_Count / Upgrade_Value;
       UiProgressBarEvent.Raise(AmountUi);
    }
    private void UpdateValue(int value)
    {
        //Logging.Log($"previous value: {Upgrade_Value}");
        Upgrade_Value = value;
        //Logging.Log($"current value: {Upgrade_Value}");
    }
}
