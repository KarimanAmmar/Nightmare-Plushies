using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    [SerializeField] private Float_event damage_event;
    [SerializeField] float damageValue;
    [SerializeField] float LifeTime;
    WaitForSeconds Waittime;
    private void OnEnable()
    {
        StartCoroutine(DeactivateObject());

    }
    private void Start()
    {
        Waittime = new WaitForSeconds(LifeTime);

    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == GameConstant.PlayerLayer)
        {
            damage_event.Raise(damageValue);
            this.gameObject.SetActive(false);
        }
        
    }
    IEnumerator DeactivateObject()
    {
        yield return Waittime;

        this.gameObject.SetActive(false);
    }
}
