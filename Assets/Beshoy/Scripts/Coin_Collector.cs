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
    private int Coins_Count;

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
    // Update is called once per frame
}
