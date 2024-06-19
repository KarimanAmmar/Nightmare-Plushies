using UnityEngine;
using System.Collections.Generic;

public class BulletPool : MonoBehaviour
{
	public GameObject bulletPrefab;
	public int poolSize = 5;

	private List<GameObject> bulletPool;
	private int currentIndex = 0;

	void Start()
	{
		bulletPool = new List<GameObject>();
		for (int i = 0; i < poolSize; i++)
		{
			GameObject bullet = Instantiate(bulletPrefab, transform);
			bullet.transform.localPosition = Vector3.zero;
			bullet.transform.localRotation = Quaternion.identity;
			bullet.SetActive(false);
			bulletPool.Add(bullet);
		}
	}

	public GameObject GetBullet()
	{
		GameObject bullet = bulletPool[currentIndex];
		currentIndex = (currentIndex + 1) % poolSize;
		bullet.SetActive(true);
		return bullet;
	}
}
