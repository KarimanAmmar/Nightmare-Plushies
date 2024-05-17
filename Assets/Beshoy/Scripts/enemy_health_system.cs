using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class enemy_health_system : MonoBehaviour
{
    [SerializeField] private Float_event enemy_damage_event;
    [SerializeField] private float enemy_max_heath;
    private float enemy_current_heath;
    [SerializeField]private bool damgeable;
    private void OnEnable()
    {
        enemy_damage_event.RegisterListener(take_damage);
    }
    private void OnDisable()
    {
        enemy_damage_event.UnregisterListener(take_damage);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "projectile")
        {
            damgeable = true;
        }
        else
        {
            damgeable= false;
        }
    }
    private void take_damage(float damage)
    {
        if (damgeable)
        {
            enemy_current_heath -= damage;
            enemy_current_heath = Mathf.Clamp(enemy_current_heath, 0, enemy_max_heath);
            if (enemy_current_heath == 0)
            {
                this.gameObject.SetActive(false);
            }
        }
    }
}
