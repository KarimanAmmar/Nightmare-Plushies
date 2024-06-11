using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
	[SerializeField] private GameObject enemyPrefab;
	[SerializeField] private int poolSize = 5;
	[SerializeField] private int maxPoolSize = 20;
	[SerializeField] private int minPoolSize = 5;
	//[SerializeField] private ParticleSystem particleSystem;
	private List<GameObject> pooledEnemies = new List<GameObject>();
	private Transform playerTransform;

	private void Start()
	{
		CreatePool();
	}

	private void CreatePool()
	{
		ClearPool();

		for (int i = 0; i < minPoolSize; i++)
		{
			GameObject enemy = InstantiateEnemy();
			pooledEnemies.Add(enemy);
			enemy.SetActive(false);
		}
	}

	private void ClearPool()
	{
		foreach (GameObject enemy in pooledEnemies)
		{
			Destroy(enemy);
		}
		pooledEnemies.Clear();
	}

	private GameObject InstantiateEnemy()
	{
		GameObject enemy = Instantiate(enemyPrefab);
		enemy.SetActive(false);
		return enemy;
	}

	public void DisableEnemy(GameObject enemy)
	{
		enemy.SetActive(false);
	}

	public GameObject ActivateEnemy(Vector3 targetPosition)
	{
		GameObject inactiveEnemy = pooledEnemies.Find(enemy => !enemy.activeSelf);

		if (inactiveEnemy != null)
		{
			inactiveEnemy.transform.position = targetPosition;
			inactiveEnemy.transform.SetParent(transform);
			inactiveEnemy.SetActive(true);
			//particleSystem.Play();
			// Set player transform here if needed
			return inactiveEnemy;
		}
		else
		{
			if (pooledEnemies.Count < maxPoolSize)
			{
				GameObject enemy = InstantiateEnemy();
				enemy.transform.position = targetPosition;
				enemy.transform.SetParent(transform);
				enemy.SetActive(true);
				pooledEnemies.Add(enemy);
				// Set player transform here if needed
				return enemy;
			}
			else
			{
				Debug.LogWarning("Max pool size reached, cannot activate more enemies.");
				return null;
			}
		}
	}

	public void SetPlayerTransform(Transform player)
	{
		playerTransform = player;
	}

	public bool AreAllEnemiesInactive()
	{
		return pooledEnemies.TrueForAll(enemy => !enemy.activeSelf);
	}
}
