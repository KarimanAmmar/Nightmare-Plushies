using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AutoShooting : MonoBehaviour
{
    [SerializeField] Transform firePoint;
    [SerializeField] EnemyDetection enemyDetection;
    [SerializeField] GameEvent IfEnemyDetected;
    [SerializeField] GameEvent IfEnemiesCleared;
    [SerializeField] GameObject[] projectiles;

    private WaitForSeconds waitTime;
    int numOfProjectiles;
    private Coroutine shootingCoroutine;

    private void Start()
    {
        waitTime = new WaitForSeconds(1.0f);
        numOfProjectiles = 2;
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
    void playerCanShoot()
    {
        if (shootingCoroutine == null)
        {
            shootingCoroutine = StartCoroutine(SetBulletActive());
            Logging.Log("started");
        }
    }

    void playerCantShoot()
    {
        if (shootingCoroutine != null )
        {
            StopCoroutine(shootingCoroutine);
            shootingCoroutine = null;
            Logging.Log("stopped");
        }
    }

    IEnumerator SetBulletActive()
    {
        GameObject[] projectiles = new GameObject[numOfProjectiles];

        while (true)
        {
            for (int i = 0; i < numOfProjectiles; i++)
            {
                projectiles[i] = ProjectilesObjectPooling.Instance.GetPooledObject();
            }

            if (projectiles.Length != 0 && enemyDetection != null && enemyDetection.EnemiesInRange.Count != 0)
            {
                int x = Mathf.Min(projectiles.Length, enemyDetection.EnemiesInRange.Count);

                //for (int i = 0; i < 2 ; i++)
                //{
                    projectiles[0].transform.position = firePoint.position;
                    ProjectilesObjectPooling.Instance.ActivatePooledObject(projectiles[0]);
                    ProjectileBehavior projectileBehavior = projectiles[0].GetComponent<ProjectileBehavior>();

                    if (projectileBehavior != null)
                    {
                        projectileBehavior.Initialize(enemyDetection.EnemiesInRange[0], 5);
                        Logging.Log(0);
                    }
                
                yield return waitTime;
            }
            else
            {
                yield return null;
            }
        }
    }
}
