using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
	[SerializeField] private GameObject enemyPrefab;
	[SerializeField] private EnemyType enemyType;
	[SerializeField] private int poolSize = 5;
	[SerializeField] private int maxPoolSize = 20;
	[SerializeField] private int minPoolSize = 5;
	private List<GameObject> pooledEnemies = new List<GameObject>();
	private int activeEnemyCount = 0;
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

	public void ActivateEnemy(Vector3 targetPosition)
	{
		GameObject inactiveEnemy = pooledEnemies.Find(enemy => !enemy.activeSelf);

		if (inactiveEnemy != null)
		{
			inactiveEnemy.transform.position = targetPosition;
			inactiveEnemy.transform.SetParent(transform); 
			inactiveEnemy.SetActive(true);
			inactiveEnemy.GetComponent<EnemyController>().SetPlayerTransform(playerTransform);
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
				enemy.GetComponent<EnemyController>().SetPlayerTransform(playerTransform);
			}
			else
			{
				Debug.LogWarning("Max pool size reached, cannot activate more enemies.");
			}
		}
	}

	public void DisableRandomEnemy()
	{
		GameObject activeEnemy = pooledEnemies.Find(enemy => enemy.activeSelf);
		if (activeEnemy != null)
		{
			DisableEnemy(activeEnemy);
		}
		else
		{
			Debug.LogWarning("No active enemies to disable.");
		}
	}

	public void SetPlayerTransform(Transform player)
	{
		playerTransform = player;
	}
}
