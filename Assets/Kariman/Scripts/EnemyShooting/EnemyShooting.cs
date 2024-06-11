using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    //projection calculation
    [SerializeField] float maxDistance;
    [SerializeField] float launchAngle = 45.0f;

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
            if (PlayerPos.transform != null)
            {

                Vector3 direction = PlayerPos.position - firePoint.position;
                float distance = Vector3.Distance(firePoint.position, PlayerPos.position);

                // Calculate the angle in radians
                float angle = (launchAngle - (launchAngle * (distance / maxDistance))) * Mathf.Deg2Rad;

                // Calculate the horizontal distance
                float horizontalDistance = new Vector2(direction.x, direction.z).magnitude;

                // Calculate the initial velocity required to reach the target
                float g = Mathf.Abs(Physics.gravity.y);
                float initialVelocity = Mathf.Sqrt(horizontalDistance * g / Mathf.Sin(2 * angle));

                // Time to reach the target
                float timeOfFlight = horizontalDistance / (initialVelocity * Mathf.Cos(angle));

                // Vertical component of the initial velocity
                float verticalVelocity = initialVelocity * Mathf.Sin(angle);

                // Correct the forward direction to be horizontal
                direction.y = 0;
                direction.Normalize();
                // Calculate the initial velocity vector
                Vector3 initialVelocityVector = direction * initialVelocity;
                initialVelocityVector.y = verticalVelocity;

                // Instantiate projectile at firePoint with directionPoint's rotation
                //GameObject projectile = Instantiate(prefab, firePoint.position, transform.rotation);
                projectile = GetPooledObject();
                projectile.transform.position = firePoint.position;
                projectile.SetActive(true);
                Rigidbody rigidbody = projectile.GetComponent<Rigidbody>();


                if (rigidbody != null)
                {
                    rigidbody.isKinematic = false;
                    rigidbody.velocity = initialVelocityVector; // Set the velocity
                }
                else
                {
                    Logging.Error("Projectile prefab does not have a Rigidbody component!");
                }
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
