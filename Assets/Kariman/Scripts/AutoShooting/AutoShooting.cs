using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AutoShooting : MonoBehaviour
{
    //
    [SerializeField] Transform firePoint;
    [SerializeField] EnemyDetection enemyDetection;
    [SerializeField] GameObject[] projectiles;

    //GameEvents
    [SerializeField] GameEvent IfEnemyDetected;
    [SerializeField] GameEvent IfEnemiesCleared;
    [SerializeField] GameEvent UpgradeProjectilesNum;
    //
	private WaitForSeconds waitTime;
    int numOfProjectiles;
    int maxNumOfProjectiles;
    private Coroutine shootingCoroutine;

    private void Start()
    {
        waitTime = new WaitForSeconds(2.0f);
        projectiles = new GameObject[10];
        numOfProjectiles = 1;
        maxNumOfProjectiles = 3;
    }
    private void OnEnable()
    {
        IfEnemyDetected.GameAction += playerCanShoot;
        IfEnemiesCleared.GameAction += playerCantShoot;
        UpgradeProjectilesNum.GameAction += UpgradeNumOfProjectiles;
    }
    private void OnDisable()
    {
        IfEnemyDetected.GameAction -= playerCanShoot;
        IfEnemiesCleared.GameAction -= playerCantShoot;
        UpgradeProjectilesNum.GameAction -= UpgradeNumOfProjectiles;
    }
    void UpgradeNumOfProjectiles()
    {
        if(numOfProjectiles < maxNumOfProjectiles)
        {
            numOfProjectiles++;
        }
        else
        {
            numOfProjectiles = maxNumOfProjectiles;
        }
    }
    void playerCanShoot()
    {
        if (shootingCoroutine == null)
        {
            shootingCoroutine = StartCoroutine(SetBulletActive());
        }
    }
    void playerCantShoot()
    {
        if (shootingCoroutine != null )
        {
            StopCoroutine(shootingCoroutine);
            shootingCoroutine = null;
        }
    }
    IEnumerator SetBulletActive()
    {
        while (true)
        {
            if (projectiles.Length != 0  && enemyDetection.EnemiesInRange.Count != 0 && enemyDetection != null)
            {
                int MinToShoot = Mathf.Min(numOfProjectiles, enemyDetection.EnemiesInRange.Count);

                for (int i = 0; i < MinToShoot; i++)
                {
                    projectiles[i] = ProjectilesObjectPooling.Instance.GetPooledObject();
                    projectiles[i].transform.position = firePoint.position; 
                    ProjectilesObjectPooling.Instance.ActivatePooledObject(projectiles[i]);
                    ProjectileBehavior projectileBehavior = projectiles[i].GetComponent<ProjectileBehavior>();

                    if (projectileBehavior != null)
                    {
                        projectileBehavior.Initialize(enemyDetection.EnemiesInRange[i], 5);
                    }
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
