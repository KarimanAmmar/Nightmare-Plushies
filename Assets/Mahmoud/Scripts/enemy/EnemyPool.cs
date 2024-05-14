using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyPool", menuName = "ScriptableObjects/EnemyPool", order = 1)]
public class EnemyPool : ScriptableObject
{
	[SerializeField] private GameObject enemyPrefab;
	[SerializeField] private EnemyType enemyType;
	[SerializeField] private int poolSize = 5;
	[SerializeField] private int maxPoolSize = 20;
	[SerializeField] private int minPoolSize = 5;
	public List<GameObject> pooledEnemies = new List<GameObject>();
	private int activeEnemyCount = 0;

	private void OnEnable()
	{
		CreatePool();
	}

	private void CreatePool()
	{
		// Clear the pooledEnemies list and destroy any objects inside it
		foreach (GameObject enemy in pooledEnemies)
		{
			DestroyImmediate(enemy);
		}
		pooledEnemies.Clear();

		for (int i = 0; i < minPoolSize; i++)
		{
			GameObject enemy = InstantiateEnemy();
			pooledEnemies.Add(enemy);
			enemy.SetActive(false);
		}
	}


	private GameObject InstantiateEnemy()
	{
		GameObject enemy = Instantiate(enemyPrefab);
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
			inactiveEnemy.SetActive(true);
		}
		else
		{
			if (pooledEnemies.Count < maxPoolSize)
			{
				GameObject enemy = InstantiateEnemy();
				enemy.transform.position = targetPosition;
				enemy.SetActive(true);
				pooledEnemies.Add(enemy);
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
}
