using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eventTest : MonoBehaviour
{
    [SerializeField] private GameEvent DamageEvent;
    [SerializeField] private GameEvent HealEvent;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            DamageEvent.GameAction.Invoke();
            Debug.Log("damage");
        }
        else if (Input.GetKeyDown(KeyCode.J))
        {
            HealEvent.GameAction.Invoke();
            Debug.Log("heal");
        }

    }
}
