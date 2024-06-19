using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldSystem : MonoBehaviour
{
    private bool OneHitActive;  // indecates if the premenant sheild is active
    private bool TenSecActive;  // indecates if the temp shield is active
    [SerializeField] private GameEvent ShieldDamageEvent;
    [SerializeField] private GameEvent healthDamageEvent;
    private void OnEnable()
    {
        ShieldDamageEvent.GameAction += DamageShield;
    }
    private void OnDisable()
    {
        ShieldDamageEvent.GameAction -= DamageShield;
    }
    private void Start()
    {
        OneHitActive= true;
        TenSecActive= false;
        StartCoroutine(pulseshield());
    }
    IEnumerator pulseshield() 
    {
        TenSecActive = true;
        //Logging.Log($"is pulse shield active?{TenSecActive}");
        yield return new WaitForSeconds(10);
        StartCoroutine(pulseshield());
    }

    private void DamageShield()
    {
        if (TenSecActive)
        {
            TenSecActive= false;
            //Logging.Log("pulse shield Down");
        }
        else if(OneHitActive)
        {
            OneHitActive= false;
            //Logging.Log("permenant shield Down");
        }
        else
        {
            healthDamageEvent.GameAction.Invoke();
        }
    }

}
