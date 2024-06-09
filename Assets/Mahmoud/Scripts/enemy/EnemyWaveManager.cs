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
	[SerializeField] private GameEvent WavesComplete;
	private int activeEnemies = 0;
	private bool spawningInProgress = false;

	private void Awake()
	{
		if (enemyPools == null || enemyPools.Count == 0 || wavesData == null || wavesData.Count == 0 || playerTransform == null || WavesComplete == null)
		{
			Logging.Error("One or more required components are not assigned.");
			return;
		}

		foreach (var wave in wavesData)
		{
			wave.waveData.isWaveCompleted = false; // Ensure isWaveCompleted is initially false
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

	public void OnEnemyDefeated()
	{
		activeEnemies--;

		if (activeEnemies <= 0 && !spawningInProgress)
		{
			wavesData[currentWaveIndex].waveData.MarkWaveCompleted();
			StartNextWave();
		}
	}

	private void StartNextWave()
	{
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
			CheckAllWavesCompleted();
		}
	}

	private IEnumerator StartNextWaveWithDelay()
	{
		if (currentWaveIndex <= 0)
		{
			yield break;
		}

		var delay = wavesData[currentWaveIndex - 1].waveData.DelayBeforeWaveStarts;
		yield return new WaitForSeconds(delay);
		StartWave();
	}

	private void StartWave()
	{
		if (currentWaveIndex >= wavesData.Count)
		{
			CheckAllWavesCompleted();
			return;
		}

		activeEnemies = wavesData[currentWaveIndex].waveData.CalculateTotalEnemies();
		SetPlayerTransformForEnemies(playerTransform);
		StartCoroutine(SpawnWave(currentWaveIndex));
	}

	private IEnumerator SpawnWave(int waveIndex)
	{
		spawningInProgress = true;

		WaveData waveData = wavesData[waveIndex].waveData;
		yield return new WaitForSeconds(waveData.DelayBeforeWaveStarts);

		// List to store all active spawn coroutines
		List<Coroutine> activeSpawns = new List<Coroutine>();

		// Start a coroutine for each type of enemy
		foreach (NumberOfEnemies enemyData in waveData.TypeOfEnemies)
		{
			Coroutine spawnCoroutine = StartCoroutine(SpawnEnemyType(enemyData, waveIndex));
			activeSpawns.Add(spawnCoroutine);
		}

		// Wait for all spawn coroutines to finish
		foreach (Coroutine spawnCoroutine in activeSpawns)
		{
			yield return spawnCoroutine;
		}

		spawningInProgress = false;

		if (currentWaveIndex >= wavesData.Count - 1)
		{
			CheckAllWavesCompleted();
		}
		else if (!wavesData[waveIndex].StartWavesByTrigger && currentWaveIndex < wavesData.Count - 1)
		{
			StartCoroutine(StartNextWaveWithDelay());
		}
	}

	private IEnumerator SpawnEnemyType(NumberOfEnemies enemyData, int waveIndex)
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
						}
					}
				}
			}

			yield return new WaitForSeconds(enemyData.delayBetweenSpawns);
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

	private void CheckAllWavesCompleted()
	{
		bool allWavesCompleted = true;
		foreach (var wave in wavesData)
		{
			if (!wave.waveData.isWaveCompleted)
			{
				allWavesCompleted = false;
				break;
			}
		}

		if (allWavesCompleted)
		{
			WavesComplete.GameAction?.Invoke();
			Logging.Log("All waves completed.");
			Debug.Log("All waves completed.");
		}
	}
}
