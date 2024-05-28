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
	[SerializeField] private List<Transform> spawnPoints;
	[SerializeField] GameEvent WavesCompelete;
	private int totalEnemiesInWave = 0;
	private int activeEnemies = 0;
	private bool spawningInProgress = false;

	private void Start()
	{
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
			WavesCompelete.GameAction?.Invoke();
			return;
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

		foreach (NumberOfEnemies enemyData in shuffledEnemies)
		{
			for (int i = 0; i < enemyData.numberOfEnemy; i++)
			{
				Transform spawnPosition = GetRandomSpawnPoint(GetNearestSpawnPoints(playerTransform.position, 3));
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
							}
						}
					}
				}

				yield return new WaitForSeconds(enemyData.delayBetweenSpawns);
			}
		}

		spawningInProgress = false;
	}

	private List<Transform> GetNearestSpawnPoints(Vector3 position, int count)
	{
		return spawnPoints.OrderBy(sp => Vector3.Distance(position, sp.position)).Take(count).ToList();
	}

	private Transform GetRandomSpawnPoint(List<Transform> points)
	{
		if (points.Count > 0)
		{
			int randomIndex = Random.Range(0, points.Count);
			return points[randomIndex];
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
}
