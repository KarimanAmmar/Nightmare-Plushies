using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageableObject : MonoBehaviour
{
    [SerializeField] private Float_event enemy_damage_event;
    [SerializeField] private float damage;
    [SerializeField] bool deactive = true;
    private void OnTriggerEnter(Collider other)
    {
        if (deactive)
        {
            this.gameObject.SetActive(false);
        }
        if (other.CompareTag(GameConstant.EnemyTag))
        {
            if (other.gameObject.GetComponent<enemy_health_system>() != null)
            {
                other.gameObject.GetComponent<enemy_health_system>().Take_damage(damage);
            }
        }
    }
}
