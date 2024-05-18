using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eventTest : MonoBehaviour
{
    [SerializeField] private GameEvent DamageEvent;
    [SerializeField] private GameEvent HealEvent;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            DamageEvent.GameAction.Invoke();
            
        }
        else if (Input.GetKeyDown(KeyCode.K))
        {
            HealEvent.GameAction.Invoke();
            
        }

    }
}
