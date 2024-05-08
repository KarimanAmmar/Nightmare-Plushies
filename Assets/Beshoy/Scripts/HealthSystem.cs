using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private GameEvent DamageEvent;
    [SerializeField] private GameEvent HealEvent;
    [SerializeField] private int Max_health;
    private int Current_health;
    void Start()
    {
        Current_health = Max_health;
        
    }
    private void OnEnable()
    {
        DamageEvent.GameAction+= take_Damage;
        HealEvent.GameAction += Gain_health;
    }
    private void OnDisable()
    {
        DamageEvent.GameAction+= take_Damage;
        HealEvent.GameAction += Gain_health;
    }

    // Update is called once per frame

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            DamageEvent.GameAction.Invoke();
        }
        else if(Input.GetKeyDown(KeyCode.J)) 
        {
            HealEvent.GameAction.Invoke();
        }
        
    }
    public void take_Damage()
    {
        Current_health -= 10;
        Current_health = Mathf.Clamp(Current_health, 0, Max_health);
        Debug.Log($"current healt: {Current_health}");
    }

    public void Gain_health()
    {
        Current_health += 10;
        Current_health = Mathf.Clamp(Current_health,0, Max_health);
        Debug.Log($"current healt: {Current_health}");
    }
}
