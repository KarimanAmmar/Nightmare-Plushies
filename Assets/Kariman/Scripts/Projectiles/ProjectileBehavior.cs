using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehavior : MonoBehaviour
{
    /// <summary>
    /// note to kariman :
    /// i will have 2 float event scriptables for damage one for player-> enemy 
    /// and the other is for enemy->player
    /// all i added is floatevent ,damage value in float 
    /// and invoke the event if the projectile collide with the enemy
    /// </summary>

    Transform targetPoint;
    [SerializeField] float speed;
    [SerializeField] private Float_event enemy_damage_event;
    [SerializeField] private float damage;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemy_damage_event.Raise(damage);
            this.gameObject.SetActive(false);
           // other.gameObject.SetActive(false);
        }
    }
    public void Initialize(Transform target, float moveSpeed)
    {
        targetPoint = target;
        speed = moveSpeed;
    }

    private void Update()
    {
        Move();
    }
    void Move()
    {
        if (targetPoint != null)
        {
            Vector3 direction = (targetPoint.position - transform.position).normalized;
            transform.Translate(direction * speed * Time.deltaTime, Space.World);
        }
    }
}
