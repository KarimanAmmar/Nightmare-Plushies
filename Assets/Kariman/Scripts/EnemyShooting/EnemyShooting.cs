using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder;

public class EnemyShooting : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private string attackAnimation = "Attack";
    [SerializeField] int fireRate = 1;
    [SerializeField] Transform firePoint;

    //Coroutine
    WaitForSeconds waitTime;
    private Coroutine shootingCoroutine;

    //Pooling
    [SerializeField] GameObject prefab;
    [SerializeField] GameObject parent;
    List<GameObject> pooledObjects;
    GameObject projectile;
    [SerializeField] int poolSize;
    [SerializeField] int projectileSpeed = 5;

    private void Awake()
    {
        pooledObjects = new List<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(prefab, parent.transform);
            ProjectileBehavior projectileBehavior = obj.GetComponent<ProjectileBehavior>();
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }
    }
    private void Start() => waitTime = new WaitForSeconds(fireRate);
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(GameConstant.PlayerTag))
        {
            CanShoot(other.gameObject.transform);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(GameConstant.PlayerTag))
        {
            CantShoot();
        }
    }
    void CanShoot(Transform PlayerPos)
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
    IEnumerator Shoot(Transform PlayerPos)
    {
        while (true)
        {
            projectile = GetPooledObject();
            projectile.transform.position = firePoint.position;
            projectile.SetActive(true);
            ProjectileBehavior projectileBehavior = projectile.GetComponent<ProjectileBehavior>();

            if (projectileBehavior != null && PlayerPos.transform != null)
            {
                projectileBehavior.Initialize(PlayerPos.transform, projectileSpeed);
                Logging.Log(PlayerPos.transform.position);
            }
            Logging.Log("ienum");
            yield return waitTime;
        }
    }
    public GameObject GetPooledObject()
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        return null;
    }
}
