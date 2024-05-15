using System.Collections;
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
        waitTime = new WaitForSeconds(2.0f);
        projectiles = new GameObject[10];
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
        while (true)
        {
            for (int i = 0; i < numOfProjectiles; i++)
            {
                projectiles[i] = ProjectilesObjectPooling.Instance.GetPooledObject();
            }

            if (projectiles.Length != 0  && enemyDetection.EnemiesInRange.Count != 0 && enemyDetection != null)
            { 
                int x = Mathf.Min(projectiles.Length , enemyDetection.EnemiesInRange.Count - 1);

                for (int i = 0; i < x; i++)
                {
                    projectiles[i].transform.position = firePoint.position;
                    ProjectilesObjectPooling.Instance.ActivatePooledObject(projectiles[i]);
                    ProjectileBehavior projectileBehavior = projectiles[i].GetComponent<ProjectileBehavior>();

                    if (projectileBehavior != null)
                    {
                        projectileBehavior.Initialize(enemyDetection.EnemiesInRange[i], 5);
                        Logging.Log(i);
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
