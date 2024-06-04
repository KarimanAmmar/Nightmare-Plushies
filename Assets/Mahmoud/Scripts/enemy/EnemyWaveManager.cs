using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyWaveManager : MonoBehaviour
{
	[SerializeField] private List<EnemyPool> enemyPools;
	[SerializeField] private List<Wave> wavesData;
	[SerializeField] private int currentWaveIndex = 0;
	[SerializeField] private Transform playerTransform;
	[SerializeField] private GameEvent WavesCompelete;
	private int totalEnemiesInWave = 0;
	private int activeEnemies = 0;
	private bool spawningInProgress = false;

	private void Start()
	{
		// Check if required components are assigned
		if (enemyPools == null || enemyPools.Count == 0)
		{
			Logging.Error("Enemy pools are not assigned.");
			return;
		}

		if (wavesData == null || wavesData.Count == 0)
		{
			Logging.Error("Waves data are not assigned.");
			return;
		}

		if (playerTransform == null)
		{
			Logging.Error("Player transform is not assigned.");
			return;
		}

		if (WavesCompelete == null)
		{
			Logging.Error("WavesCompelete GameEvent is not assigned.");
			return;
		}

		foreach (var wave in wavesData)
		{
			if (wave.StartWavesByTrigger)
			{
				if (wave.StartTrigger == null)
				{
					Logging.Error("StartTrigger is not assigned in one of the waves.");
					continue;
				}

				var waveStartTrigger = wave.StartTrigger.GetComponent<WaveStartTrigger>();
				if (waveStartTrigger == null)
				{
					Logging.Error($"WaveStartTrigger component is missing on {wave.StartTrigger.gameObject.name}.");
					continue;
				}

				wave.StartTrigger.gameObject.SetActive(true);
				waveStartTrigger.OnPlayerEnter += OnPlayerEnterWaveTrigger;
			}
		}

		// Start the first wave if it's not set to start by trigger
		if (!wavesData[currentWaveIndex].StartWavesByTrigger)
		{
			StartWave();
		}
	}

	private void OnPlayerEnterWaveTrigger(Collider trigger)
	{
		for (int i = 0; i < wavesData.Count; i++)
		{
			if (wavesData[i].StartTrigger == trigger)
			{
				currentWaveIndex = i;
				StartWave();
				break;
			}
		}
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
		Logging.Log($"Enemy defeated. Active enemies remaining: {activeEnemies}");

		if (activeEnemies <= 0 && !spawningInProgress)
		{
			Logging.Log($"Wave {currentWaveIndex} completed.");
			wavesData[currentWaveIndex].waveData.isWaveCompleted = true;
			currentWaveIndex++;

			if (currentWaveIndex < wavesData.Count)
			{
				if (wavesData[currentWaveIndex].StartWavesByTrigger)
				{
					wavesData[currentWaveIndex].StartTrigger.gameObject.SetActive(true);
				}
				else
				{
					StartCoroutine(StartNextWaveWithDelay());
				}
			}
			else
			{
				Logging.Log("All waves completed. Invoking WavesCompelete event.");
				WavesCompelete.GameAction?.Invoke();
			}
		}
	}

	private IEnumerator StartNextWaveWithDelay()
	{
		if (currentWaveIndex <= 0)
		{
			yield break;
		}

		var delay = wavesData[currentWaveIndex - 1].waveData.DelayBeforeWaveStarts;
		Logging.Log($"Waiting for {delay} seconds before starting next wave.");
		yield return new WaitForSeconds(delay);
		StartWave();
	}

	private void StartWave()
	{
		if (currentWaveIndex >= wavesData.Count)
		{
			Logging.Log("No more waves to start. Invoking WavesCompelete event.");
			WavesCompelete.GameAction?.Invoke();
			return;
		}

		Logging.Log($"Starting wave {currentWaveIndex}.");
		SetPlayerTransformForEnemies(playerTransform);
		totalEnemiesInWave = wavesData[currentWaveIndex].waveData.CalculateTotalEnemies();
		StartCoroutine(SpawnWave(currentWaveIndex));
	}

	private IEnumerator SpawnWave(int waveIndex)
	{
		spawningInProgress = true;

		WaveData waveData = wavesData[waveIndex].waveData;
		yield return new WaitForSeconds(waveData.DelayBeforeWaveStarts);

		List<NumberOfEnemies> shuffledEnemies = waveData.TypeOfEnemies.OrderBy(x => Random.value).ToList();

		foreach (NumberOfEnemies enemyData in shuffledEnemies)
		{
			for (int i = 0; i < enemyData.numberOfEnemy; i++)
			{
				Transform spawnPosition = GetRandomSpawnPoint(wavesData[waveIndex].SpawnPoints);
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
								Logging.Log($"Enemy spawned. Active enemies: {activeEnemies}");
							}
						}
					}
				}

				yield return new WaitForSeconds(enemyData.delayBetweenSpawns);
			}
		}

		spawningInProgress = false;

		// Check if all waves are completed
		if (currentWaveIndex >= wavesData.Count - 1)
		{
			Logging.Log("All waves completed. Invoking WavesCompelete event.");
			WavesCompelete.GameAction?.Invoke();
		}
		else if (!wavesData[waveIndex].StartWavesByTrigger && currentWaveIndex < wavesData.Count - 1)
		{
			StartCoroutine(StartNextWaveWithDelay());
		}
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
