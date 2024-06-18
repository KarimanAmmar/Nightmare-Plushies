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
    [SerializeField] private GameEvent DeathEvent;
    [SerializeField] private RectTransform Healthbar;
    [SerializeField] private float Max_health;
    private Camera _camera;
    private float damageReduction = 0;
    private float Current_health;
    private float amount_Ui;
    void Start()
    {
        Current_health = Max_health;
        _camera = Camera.main;
        Healthbar.LookAt(_camera.transform.position);

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
    private void LateUpdate()
    {
        if (_camera != null)
        {
            Healthbar.LookAt(Healthbar.position + _camera.transform.rotation * Vector3.forward, _camera.transform.rotation * Vector3.up); ;
        }
    }

    public void take_Damage(float amount)
    {
        float damage = amount - (damageReduction * amount);
        Current_health -= damage;
        Current_health = Mathf.Clamp(Current_health, 0, Max_health);
        Update_UI();
        if (Current_health == 0)
        {
            DeathEvent.GameAction.Invoke();
        }
        Logging.Log($"dmage taken:{damage}");
        Logging.Log($"current healt: {Current_health}");
    }

    public void Gain_health(float amount)
    {
        Current_health += amount;
        Current_health = Mathf.Clamp(Current_health,0, Max_health);
        Update_UI();
        Logging.Log($"current healt: {Current_health}");
    }
    private void Update_UI()
    {
        amount_Ui = Current_health/Max_health;
        health_UI.Raise(amount_Ui);
    }
    public void UpgradeHealth(float value)
    {
        amount_Ui = Current_health / Max_health;
        if(amount_Ui < 1)
        {
            Max_health += ((value / 100f) * Max_health);
            Current_health +=(amount_Ui * Max_health)-Current_health;
            Logging.Log("yes");
        }
        else
        {
            Max_health += ((value / 100f) * Max_health);
            Current_health = Max_health;
            Logging.Log("no");
        }        
        Update_UI();

    }
    public void Upgrade_DamageReduction(float amount)
    {
        damageReduction += amount/100f;
    }

}
