using UnityEngine;

public class EnemyFire : MonoBehaviour
{
	[SerializeField] private BulletPool bulletPool;
	[SerializeField] private Transform firePoint;
	[SerializeField] private bool isFire = false;
	[SerializeField] private float bulletSpeed = 20f;
	
	public void FireBullet()
	{
		GameObject bullet = bulletPool.GetBullet();
		bullet.transform.parent = null;
		// Ensure bullet is active when fired
		bullet.SetActive(true);

		Rigidbody rb = bullet.GetComponent<Rigidbody>();

		if (rb != null)
		{
			rb.velocity = Vector3.zero;
			rb.angularVelocity = Vector3.zero;
		}

		bullet.transform.position = firePoint.position;
		bullet.transform.rotation = firePoint.rotation;

		if (rb != null)
		{
			rb.velocity = firePoint.forward * bulletSpeed;
		}
	}
}
