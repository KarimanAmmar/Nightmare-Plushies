using System;
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
    [SerializeField]private Float_event enemy_damage_event;
    [SerializeField]private float enemy_max_heath;
    [SerializeField]private bool damgeable;
    [SerializeField]private Enemy_UI_handler UI_Handler;
    [SerializeField]private float enemy_current_heath;
    [SerializeField]private GameObject CoinDrop;
    private float fillamountUI;

    private void OnEnable()
    {
        enemy_damage_event.RegisterListener(Take_damage);
       
        
    }

    

    private void OnDisable()
    {
        enemy_damage_event.UnregisterListener(Take_damage);
        enemy_current_heath = enemy_max_heath;
        Update_UI();
    }
    private void Start()
    {
        enemy_current_heath = enemy_max_heath;
        Update_UI();
    }
   
    public void Take_damage(float damage)
    {
        
            enemy_current_heath -= damage;
            enemy_current_heath = Mathf.Clamp(enemy_current_heath,0, enemy_max_heath);
            Update_UI();
            damgeable = false;
            if (enemy_current_heath == 0)
            {
                DropCoin();
                this.gameObject.SetActive(false);
            }
               
    }
    private void Update_UI()
    {
        fillamountUI = enemy_current_heath / enemy_max_heath;
        UI_Handler.Update_UI(fillamountUI);
    }
    private void DropCoin()
    {
        if (CoinDrop != null) // Check if a prefab is assigned
        {
            Instantiate(CoinDrop, transform.position, Quaternion.AngleAxis(90f,new Vector3(0,0,1)));
        }
    }
}
