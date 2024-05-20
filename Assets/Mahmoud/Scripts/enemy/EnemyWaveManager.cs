using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyWaveManager : MonoBehaviour
{
	[SerializeField] private List<EnemyPool> enemyPools;
	[SerializeField] private List<WaveData> wavesData;
	[SerializeField] private int currentWaveIndex = 0;
	[SerializeField] private Transform playerTransform;

	private int totalEnemiesInWave = 0;
	private int activeEnemies = 0;
	private bool spawningInProgress = false;
	private static List<SpawnPoint> spawnPoints = new List<SpawnPoint>();

	private void Start()
	{
		RegisterSpawnPoints();
		StartWave();
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.C))
		{
			int randomEnemyPoolIndex = Random.Range(0, enemyPools.Count);
			enemyPools[randomEnemyPoolIndex].DisableRandomEnemy();
		}
	}

	private void RegisterSpawnPoints()
	{
		if (spawnPoints.Count == 0)
		{
			SpawnPoint[] allSpawnPoints = FindObjectsOfType<SpawnPoint>();
			spawnPoints.AddRange(allSpawnPoints);
		}
	}

	public static void RegisterSpawnPoint(SpawnPoint point)
	{
		if (!spawnPoints.Contains(point))
		{
			spawnPoints.Add(point);
		}
	}

	public static void UnregisterSpawnPoint(SpawnPoint point)
	{
		if (spawnPoints.Contains(point))
		{
			spawnPoints.Remove(point);
		}
	}

	public void OnEnemyDefeated()
	{
		activeEnemies--;
		if (activeEnemies <= 0 && !spawningInProgress)
		{
			currentWaveIndex++;
			StartWave();
		}
	}

	private void StartWave()
	{
		if (currentWaveIndex >= wavesData.Count)
		{
			return;
		}

		foreach (SpawnPoint spawnPoint in spawnPoints)
		{
			spawnPoint.ResetAvailability();
		}

		SetPlayerTransformForEnemies(playerTransform);
		totalEnemiesInWave = wavesData[currentWaveIndex].CalculateTotalEnemies();
		StartCoroutine(SpawnWave(currentWaveIndex));
	}

	private IEnumerator SpawnWave(int waveIndex)
	{
		spawningInProgress = true;

		WaveData waveData = wavesData[waveIndex];
		yield return new WaitForSeconds(waveData.DelayBeforeWaveStarts);

		List<NumberOfEnemies> shuffledEnemies = waveData.TypeOfEnemies.OrderBy(x => Random.value).ToList();
		List<SpawnPoint> availablePoints = spawnPoints.FindAll(point => point.IsAvailable);

		foreach (NumberOfEnemies enemyData in shuffledEnemies)
		{
			for (int i = 0; i < enemyData.numberOfEnemy; i++)
			{
				Transform spawnPosition = GetRandomAvailableSpawnPoint(availablePoints);
				if (spawnPosition != null)
				{
					EnemyPool enemyPool = enemyPools[(int)enemyData.enemyType];
					if (enemyPool != null)
					{
						GameObject enemy = enemyPool.ActivateEnemy(spawnPosition.position);
						if (enemy != null)
						{
							EnemyController enemyController = enemy.GetComponent<EnemyController>();
							if (enemyController != null)
							{
								enemyController.OnDefeated += OnEnemyDefeated;
								activeEnemies++;
								MarkSpawnPointUnavailable(spawnPosition);
							}
						}
					}
				}

				yield return new WaitForSeconds(enemyData.delayBetweenSpawns);
			}
		}

		spawningInProgress = false;
	}

	private Transform GetRandomAvailableSpawnPoint(List<SpawnPoint> availablePoints)
	{
		if (availablePoints.Count > 0)
		{
			int randomIndex = Random.Range(0, availablePoints.Count);
			return availablePoints[randomIndex].transform;
		}
		return null;
	}

	private void SetPlayerTransformForEnemies(Transform player)
	{
		foreach (EnemyPool pool in enemyPools)
		{
			pool.SetPlayerTransform(player);
		}
	}

	private void MarkSpawnPointUnavailable(Transform point)
	{
		SpawnPoint spawnPoint = point.GetComponent<SpawnPoint>();
		if (spawnPoint != null)
		{
			spawnPoint.SetAvailability(false);
		}
	}
}
