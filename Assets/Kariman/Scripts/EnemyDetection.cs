using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
    public List<Transform> enemiesInRange = new List<Transform>();
    public Transform player;
    private Transform closestEnemy;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Logging.Log("enterd");

            Transform newEnemy = other.transform;
            enemiesInRange.Add(newEnemy);

            // Check if the new enemy is closer than the current closest enemy
            if (closestEnemy == null || Vector3.Distance(player.position, newEnemy.position) < Vector3.Distance(player.position, closestEnemy.position))
            {
                closestEnemy = newEnemy;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Transform exitingEnemy = other.transform;
            enemiesInRange.Remove(exitingEnemy);

            // If the exiting enemy was the closest, find the new closest enemy
            if (closestEnemy == exitingEnemy)
            {
                FindClosestEnemy();
            }
        }
    }

    void FindClosestEnemy()
    {
        closestEnemy = null;
        float closestDistance = Mathf.Infinity;

        foreach (Transform enemy in enemiesInRange)
        {
            float distanceToPlayer = Vector3.Distance(player.position, enemy.position);
            if (distanceToPlayer < closestDistance)
            {
                closestDistance = distanceToPlayer;
                closestEnemy = enemy;
            }
        }
    }

    void Update()
    {
        if (closestEnemy != null)
        {
            //float distanceToPlayer = Vector3.Distance(player.position, closestEnemy.position);
            //Debug.Log("Closest enemy distance: " + distanceToPlayer);
            Logging.Log(closestEnemy.gameObject.name);
        }
        FindClosestEnemy();
    }
}
