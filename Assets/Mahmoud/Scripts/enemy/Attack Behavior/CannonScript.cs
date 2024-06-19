using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonScript : MonoBehaviour
{
	public GameObject cannonBallPrefab; // Assign the cannonball prefab in the Inspector
	public Transform firePoint; // The point from where the cannonball will be shot
	public float shootForce = 10f; // Adjust the shooting force in the Inspector
	public float shootDelay = 2f; // Delay between shots
	public int poolSize = 10; // Number of bullets in the pool

	private Queue<GameObject> cannonBallPool;
	private bool canShoot = true;

	void Start()
	{
		// Initialize the cannonball pool
		cannonBallPool = new Queue<GameObject>();

		for (int i = 0; i < poolSize; i++)
		{
			GameObject cannonBall = Instantiate(cannonBallPrefab);
			cannonBall.SetActive(false);
			cannonBallPool.Enqueue(cannonBall);
		}
	}

	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
		{
			if (canShoot)
			{
				StartCoroutine(ShootCannonBall());
			}
		}
	}

	IEnumerator ShootCannonBall()
	{
		canShoot = false;

		// Retrieve a cannonball from the pool
		GameObject cannonBall = cannonBallPool.Dequeue();
		cannonBall.transform.position = firePoint.position;
		cannonBall.transform.rotation = firePoint.rotation;
		cannonBall.SetActive(true);

		// Add downward force to the cannonball
		Rigidbody rb = cannonBall.GetComponent<Rigidbody>();
		if (rb != null)
		{
			rb.velocity = Vector3.zero; // Reset velocity
			rb.angularVelocity = Vector3.zero; // Reset angular velocity
			rb.AddForce(Vector3.down * shootForce, ForceMode.Impulse);
		}

		// Re-enqueue the cannonball after a delay to simulate it falling out of the screen
		yield return new WaitForSeconds(5f); // Adjust the delay as needed
		cannonBall.SetActive(false);
		cannonBallPool.Enqueue(cannonBall);

		// Wait before allowing another shot
		yield return new WaitForSeconds(shootDelay);

		canShoot = true;
	}
}
