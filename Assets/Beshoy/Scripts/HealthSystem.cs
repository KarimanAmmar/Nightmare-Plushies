using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// todo:
/// change health value to float \
/// use float_event scriptables instead of empty event
/// create a method to send (currenthealth/maxhealth) to the UI manager(if there is any)
/// </summary>
public class HealthSystem : MonoBehaviour
{
    [SerializeField] private Float_event DamageEvent;
    [SerializeField] private Float_event HealEvent;
    [SerializeField] private Float_event health_UI;
    [SerializeField] private float Max_health;
    private float Current_health;
    void Start()
    {
        Current_health = Max_health;
        
    }
    private void OnEnable()
    {
        DamageEvent.RegisterListener(take_Damage);
        HealEvent.RegisterListener(Gain_health);
    }
    private void OnDisable()
    {
        DamageEvent.UnregisterListener(take_Damage);
        HealEvent.UnregisterListener(Gain_health);
    }
   
    public void take_Damage(float amount)
    {
        Current_health -= 10;
        Current_health = Mathf.Clamp(Current_health, 0, Max_health);
        float amount_Ui = Current_health / Max_health;
        health_UI.Raise(amount_Ui);
        Debug.Log($"current healt: {Current_health}");
    }

    public void Gain_health(float amount)
    {
        Current_health += 10;
        Current_health = Mathf.Clamp(Current_health,0, Max_health);
        float amount_Ui = Current_health / Max_health;
        health_UI.Raise(amount_Ui);
        Debug.Log($"current healt: {Current_health}");
    }
}
