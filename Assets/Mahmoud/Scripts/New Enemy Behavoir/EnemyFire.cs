using UnityEngine;
using System.Collections;

public class EnemyFire : MonoBehaviour
{
	[SerializeField] private BulletPool bulletPool;
	[SerializeField] private Transform firePoint;
	[SerializeField] private bool isFire = false;
	[SerializeField] private float bulletSpeed = 20f;

	void Update()
	{
		if (isFire)
		{
			FireBullet();
		}
	}

	void FireBullet()
	{
		isFire = false;
		GameObject bullet = bulletPool.GetBullet();
		Rigidbody rb = bullet.GetComponent<Rigidbody>();

		if (rb != null)
		{
			rb.velocity = Vector3.zero;
			rb.angularVelocity = Vector3.zero;
		}

		bullet.transform.localPosition = Vector3.zero;
		bullet.transform.localRotation = Quaternion.identity;

		if (rb != null)
		{
			rb.velocity = firePoint.forward * bulletSpeed;
		}
	}
}
