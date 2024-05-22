using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class enemy_health_system : MonoBehaviour
{
    /// <summary>
    /// note :
    /// i didnt use the float event scriptables for the enemy health bar
    /// beacause i cant use one float event for all enemies 
    /// so i will find another way to use to change the fill amount of the bar when the enemy is hit 
    /// </summary>
    [SerializeField] private Float_event enemy_damage_event;
    [SerializeField] private float enemy_max_heath;
    [SerializeField]private bool damgeable;
    private Enemy_UI_handler UI_Handler;
    private float enemy_current_heath;
    private float fillamountUI;

    private void OnEnable()
    {
        enemy_damage_event.RegisterListener(take_damage);
        Update_UI();
    }
    private void OnDisable()
    {
        enemy_damage_event.UnregisterListener(take_damage);
        enemy_current_heath = enemy_max_heath;
    }
    private void Start()
    {
        enemy_current_heath = enemy_max_heath;
        UI_Handler = GetComponent<Enemy_UI_handler>();
        Update_UI();
        

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "projectile")
        {
            damgeable = true;
        }
    }
    private void take_damage(float damage)
    {
        if (damgeable)
        {
            enemy_current_heath -= damage;
            enemy_current_heath = Mathf.Clamp(enemy_current_heath, 0, enemy_max_heath);
            Update_UI();
            if (enemy_current_heath == 0)
            {
                this.gameObject.SetActive(false);
            }
            
        }
        damgeable = false;
    }
    private void Update_UI()
    {
        fillamountUI = enemy_current_heath / enemy_max_heath;
        UI_Handler.Update_UI(fillamountUI);
    }
}
