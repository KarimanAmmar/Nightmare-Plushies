using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// thsi script is for changing the number of the upgrade value in the collector when the level changes
/// </summary>
public class LevelNumber : MonoBehaviour
{
    [SerializeField] private int[] numbers;
    private int currentValue;
    [SerializeField] private GameEvent LevelEvent;//when to change the value
    [SerializeField] private int_Event value_event;//how to broadcast the value
    private void OnEnable()
    {
        LevelEvent.GameAction+=UPdateUpgradevalue;
        
    }
    private void OnDisable()
    {
        LevelEvent.GameAction-=UPdateUpgradevalue;
        
    }
    private void UPdateUpgradevalue()
    {
        currentValue++;
        if(currentValue == numbers.Length)
        {
            currentValue = 0;
        }
        value_event.Raise(numbers[currentValue]);
    }
    private void Start()
    {
        currentValue = 0;
        value_event.Raise(numbers[currentValue]);
    }

}
