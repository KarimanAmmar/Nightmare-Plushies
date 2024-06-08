using System.Collections;
using UnityEngine;

public class AimsAttack : MonoBehaviour, IAttackBehavior
{
    [SerializeField] private Animator animator;

    [SerializeField] private string attackAnimation = "Attack";
    [SerializeField] int fireRate;
    [SerializeField] int attackDistance;
    [SerializeField] ObjectPooling thisObjectPooling;
    [SerializeField] Transform firePoint;
    WaitForSeconds waitTime;
    private Coroutine shootingCoroutine;
    GameObject projectile;

    private void Start()=> waitTime = new WaitForSeconds(fireRate);
    //private void Update()
    //{
    //    if (shootingCoroutine == null)
    //    {
    //        animator.SetBool("Attack", true);
    //    }
    //    else
    //    {
    //        animator.SetBool("Attack", false);
    //    }
    //}
    public void Attack(EnemyController enemy, Vector3 playerPosition)
    {
        float distance = Vector3.Distance(enemy.transform.position, playerPosition);

        if (distance <= attackDistance)
        {
            CanShoot(playerPosition);
        }
        else
        {
            CantShoot();
        }
    }
    void CanShoot(Vector3 PlayerPos)
    {
        if (shootingCoroutine == null)
        {
            shootingCoroutine = StartCoroutine(Shoot(PlayerPos));
            animator.SetBool("Attack", true);
        }
    }
    void CantShoot()
    {
        if (shootingCoroutine != null)
        {
            StopCoroutine(shootingCoroutine);
            shootingCoroutine = null;
            animator.SetBool("Attack", false);
        }
    }
    IEnumerator Shoot(Vector3 PlayerPos)
    {
        while (true)
        {
            projectile = ObjectPooling.Instance.GetPooledObject();
            projectile.transform.position = firePoint.position;
            ObjectPooling.Instance.ActivatePooledObject(projectile);
            ProjectileBehavior projectileBehavior = projectile.GetComponent<ProjectileBehavior>();

            if (projectileBehavior != null)
            {
                GameObject tempGameObject = new GameObject("TempObject");
                tempGameObject.transform.position = PlayerPos;

                projectileBehavior.Initialize(tempGameObject.transform, 5);

                Destroy(tempGameObject);
            }
            Logging.Log("ienum");
            yield return waitTime;
        }

    }
}
