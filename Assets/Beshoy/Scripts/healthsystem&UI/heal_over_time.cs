using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class heal_over_time : MonoBehaviour
{
    [SerializeField] float heal_amount;
    [SerializeField] float heal_time;
    [SerializeField] private Float_event heal_event;
    private delegate void HealEventDelegate();
    private bool enabled_healing;

    private void Start()
    {
        HealEventDelegate healdel= heallIng_time;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == GameConstant.PlayerTag)
        {
            enableHealing();
            
        }
    }
    private void OnTriggerExit(Collider other)
    {

        if (other.gameObject.tag == GameConstant.PlayerTag)
        {
           disableHealing();

        }

    }

    private void enableHealing()
    {
       enabled_healing = true;
       Invoke("heallIng_time", heal_time);
    }

    private void disableHealing()
    {
        enabled_healing=false;
        CancelInvoke();
    }

    private void heallIng_time()
    {
        if (enabled_healing)
        {

            heal_event.Raise(heal_amount);
            Invoke("heallIng_time", heal_time);
        }

    }
}
