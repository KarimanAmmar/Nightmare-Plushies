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

	// Coroutine
	WaitForSeconds waitTime;
	private Coroutine shootingCoroutine;
	private bool isShooting;

	// Pooling
	[SerializeField] GameObject prefab;
	[SerializeField] GameObject parent;
	List<GameObject> pooledObjects;
	GameObject projectile;
	[SerializeField] int poolSize;
	[SerializeField] int projectileSpeed = 5;

	// Projection calculation
	[SerializeField] float maxDistance;
	[SerializeField] float launchAngle = 45.0f;
	//audio
	[SerializeField] AudioClip shootClip;

	public bool IsShooting { get => isShooting; set => isShooting = value; }

	private void Awake()
	{
		pooledObjects = new List<GameObject>();

		for (int i = 0; i < poolSize; i++)
		{
			GameObject obj = Instantiate(prefab, parent.transform);
			obj.SetActive(false);
			pooledObjects.Add(obj);
		}
	}

	private void Start()
	{
		waitTime = new WaitForSeconds(1f / fireRate);
	}

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

	void CanShoot(Transform playerPos)
	{
		if (shootingCoroutine == null)
		{
			shootingCoroutine = StartCoroutine(Shoot(playerPos));
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

	IEnumerator Shoot(Transform playerPos)
	{
		while (true)
		{
			if (playerPos != null && !IsShooting)
			{
				IsShooting = true;

				Vector3 direction = playerPos.position - firePoint.position;
				float distance = Vector3.Distance(firePoint.position, playerPos.position);

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
				projectile = GetPooledObject();
				projectile.transform.position = firePoint.position;
				AudioManager.Instance.PlyENV(shootClip);
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

				//Logging.Log(playerPos.transform.position);
				yield return waitTime;
				IsShooting = false;
			}
			else
			{
                yield return null;
            }
            //Logging.Log("ienum");
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
		GameObject obj = Instantiate(prefab, parent.transform);
		obj.SetActive(false);
		pooledObjects.Add(obj);
		return obj;
	}
}
