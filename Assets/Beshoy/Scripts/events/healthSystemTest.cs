using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthsystemtest : MonoBehaviour
{
    [SerializeField] private Float_event DamageEvent;
    [SerializeField] private Float_event HealEvent;
    [SerializeField] private float damage;
    [SerializeField] private bool is_healing;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag== GameConstant.PlayerTag)
        {
            activateEvent();
        }
    }

    private void activateEvent()
    {
        if (!is_healing)
        {
            DamageEvent.Raise(damage);
        }else
        {
            HealEvent.Raise(damage);
        }
    }
}
