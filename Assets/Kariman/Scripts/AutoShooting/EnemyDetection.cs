using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] private List<Transform> enemiesInRange = new List<Transform>();
    private Transform closestEnemy;
    public List<Transform> EnemiesInRange => enemiesInRange;
    public Transform ClosestEnemy => closestEnemy;
    [SerializeField] GameEvent EnemyDetected;
    [SerializeField] GameEvent EnemiesCleared;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Transform newEnemy = other.transform;
            EnemiesInRange.Add(newEnemy);
            SortEnemiesByDistance();
            // Check if the new enemy is closer than the current closest enemy
            if (closestEnemy == null || Vector3.Distance(player.position, newEnemy.position) < Vector3.Distance(player.position, closestEnemy.position))
            {
                closestEnemy = newEnemy;
                EnemyDetected.GameAction?.Invoke();
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Transform exitingEnemy = other.transform;
            RemoveEnemy(exitingEnemy);
            SortEnemiesByDistance();
        }
    }
    void FindClosestEnemy()
    {
        closestEnemy = null;
        float closestDistance = Mathf.Infinity;

        foreach (Transform enemy in EnemiesInRange)
        {
            float distanceToPlayer = Vector3.Distance(player.position, enemy.position);
            if (distanceToPlayer < closestDistance)
            {
                closestDistance = distanceToPlayer;
                closestEnemy = enemy;
            }
        }
    }
    void RemoveEnemy(Transform enemyToRemove)
    {
        EnemiesInRange.Remove(enemyToRemove);
        if (closestEnemy == enemyToRemove)
        {
            FindClosestEnemy();
        }
        if (enemiesInRange.Count == 0)
        {
            EnemiesCleared.GameAction?.Invoke();
        }
    }
    void SortEnemiesByDistance()
    {
        enemiesInRange.Sort((a, b) => Vector3.Distance(player.position, a.position).CompareTo(Vector3.Distance(player.position, b.position)));
    }
    void Update()
    {
        if (closestEnemy != null && !closestEnemy.gameObject.activeSelf)
        {
            RemoveEnemy(closestEnemy);
            SortEnemiesByDistance();
        }
    }
}
