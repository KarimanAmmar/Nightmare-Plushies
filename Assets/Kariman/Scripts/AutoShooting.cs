using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoShooting : MonoBehaviour
{
    [SerializeField] Transform firePoint;
    [SerializeField] EnemyDetection enemyDetection;
    [SerializeField] GameEvent IfEnemyDetected;
    [SerializeField] GameEvent IfEnemiesCleared;
    private WaitForSeconds waitTime;

    private void Start()
    {
        waitTime = new WaitForSeconds(2.0f);
    }
    private void OnEnable()
    {
        IfEnemyDetected.GameAction += playerCanShoot;
        IfEnemiesCleared.GameAction += playerCantShoot;
    }
    private void OnDisable()
    {
        IfEnemyDetected.GameAction -= playerCanShoot;
        IfEnemiesCleared.GameAction -= playerCantShoot;
    }
    public void playerCanShoot()
    {
        StartCoroutine(SetBulletActive());
        Logging.Log("started");
    }
    public void playerCantShoot()
    {
        StopCoroutine(SetBulletActive());
        Logging.Log("stoped");
    }
    IEnumerator SetBulletActive()
    {
        while(true)
        {
            Logging.Log("in");

            GameObject bullet = ObjectPooling.Instance.GetPooledObject();
            if (bullet != null && enemyDetection != null)
            {
                if (enemyDetection.EnemiesInRange.Count != 0)
                {
                    // Calculate direction towards current target
                    Vector3 direction = (enemyDetection.ClosestEnemy.position - firePoint.position).normalized;

                    // Set bullet's position to firePoint
                    bullet.transform.position = firePoint.position;

                    // Calculate rotation to look towards target
                    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                    bullet.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

                    // Activate bullet
                    ObjectPooling.Instance.ActivatePooledObject(bullet);
                }
            }
            yield return waitTime;
        }
    }
}
