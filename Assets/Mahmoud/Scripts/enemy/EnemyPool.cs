using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
	[SerializeField] private GameObject enemyPrefab;
	[SerializeField] private int initialPoolSize = 5;
	[SerializeField] private int maxPoolSize = 20;
	private List<GameObject> pooledEnemies = new List<GameObject>();
	private Transform playerTransform;
	// [SerializeField] private ParticleSystem particleSystem;

	private void Start()
	{
		CreatePool(initialPoolSize);
	}

	private void CreatePool(int size)
	{
		ClearPool();

		for (int i = 0; i < size; i++)
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
		enemy.transform.SetParent(transform);
		enemy.SetActive(false);
		return enemy;
	}

	public void DisableEnemy(GameObject enemy)
	{
		enemy.SetActive(false);
	}

	public GameObject ActivateEnemy(Vector3 targetPosition)
	{
		GameObject inactiveEnemy = GetInactiveEnemy();

		if (inactiveEnemy != null)
		{
			SetupEnemy(inactiveEnemy, targetPosition);
			return inactiveEnemy;
		}

		if (pooledEnemies.Count < maxPoolSize)
		{
			GameObject newEnemy = InstantiateEnemy();
			pooledEnemies.Add(newEnemy);
			SetupEnemy(newEnemy, targetPosition);
			return newEnemy;
		}

		Debug.LogWarning("Max pool size reached, cannot activate more enemies.");
		return null;
	}

	private GameObject GetInactiveEnemy()
	{
		return pooledEnemies.Find(enemy => !enemy.activeSelf);
	}

	private void SetupEnemy(GameObject enemy, Vector3 targetPosition)
	{
		enemy.transform.position = targetPosition;
		StartCoroutine(ActivateEnemyWithDelay(enemy));
		enemy.SetActive(true);
		// if (particleSystem != null) particleSystem.Play();
	}

	private IEnumerator ActivateEnemyWithDelay(GameObject enemy)
	{
		EnemyController enemyController = enemy.GetComponent<EnemyController>();
		if (enemyController != null)
		{
			enemyController.enabled = false;
		}

		yield return new WaitForSeconds(1f);

		if (enemy != null && enemyController != null)
		{
			enemyController.enabled = true;
			enemyController.CurrentState = enemyController.WanderingState;
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
