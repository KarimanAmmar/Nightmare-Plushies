using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
	[SerializeField] private List<Wave> waves;
	[SerializeField] private Transform playerTransform;
	[SerializeField] private List<EnemyPool> enemyPools;
	[SerializeField] private GameEvent enemyDefeatedEvent;
	[SerializeField] private GameEvent wavesCompletedEvent;
	[SerializeField] private GameEvent startNextWave;
	[SerializeField] private int_Event waveCompletedEvent;

	private int currentWaveIndex = -1;
	[SerializeField] private int totalEnemies;
	[SerializeField] private int activeEnemies = 0;
	private bool spawningInProgress = false;
	private bool waitingForNextWaveTrigger = false;
	private bool allWavesCompleted = false;

	private void Awake()
	{
		foreach (Wave wave in waves)
		{
			wave.waveData.isWaveCompleted = false;
		}
		SetPlayerTransformForEnemies(playerTransform);

		// Subscribe to the enemyDefeatedEvent and startNextWave GameActions
		enemyDefeatedEvent.GameAction += DecrementActiveEnemies;
		startNextWave.GameAction += TriggerNextWave;
	}

	private void Start()
	{
		if (waves.Count > 0)
		{
			NextWave();
		}
	}

	private void Update()
	{
		if (activeEnemies == 0 && !allWavesCompleted && !waitingForNextWaveTrigger)
		{
			NextWave();
		}
	}

	private void SpawnEnemies(Wave wave)
	{
		totalEnemies = wave.waveData.CalculateTotalEnemies();
		activeEnemies = totalEnemies;

		StartCoroutine(SpawnEnemyCoroutine(wave));
	}

	private IEnumerator SpawnEnemyCoroutine(Wave wave)
	{
		bool isFirstEnemyType = true;

		foreach (NumberOfEnemies enemyData in wave.waveData.TypeOfEnemies)
		{
			int numberOfEnemiesToSpawn = enemyData.numberOfEnemy;

			if (isFirstEnemyType && currentWaveIndex == 0)
			{
				numberOfEnemiesToSpawn += 1;
				isFirstEnemyType = false;
				Debug.Log("Spawning first type of enemy with one additional count: " + numberOfEnemiesToSpawn);
			}

			for (int i = 0; i < numberOfEnemiesToSpawn; i++)
			{
				Transform spawnPoint = wave.SpawnPoints[Random.Range(0, wave.SpawnPoints.Count)];
				GameObject newEnemy = enemyPools[(int)enemyData.enemyType].ActivateEnemy(spawnPoint.position);
				yield return new WaitForSeconds(enemyData.delayBetweenSpawns);
			}
		}

		spawningInProgress = false;
	}

	private void SetPlayerTransformForEnemies(Transform player)
	{
		foreach (EnemyPool pool in enemyPools)
		{
			pool.SetPlayerTransform(player);
		}
	}

	private void NextWave()
	{
		currentWaveIndex++;
		if (currentWaveIndex < waves.Count)
		{
			Wave currentWave = waves[currentWaveIndex];
			if (currentWave.ByTrigger)
			{
				waitingForNextWaveTrigger = true;
			}
			else
			{
				SpawnEnemies(currentWave);
			}
		}
		else
		{
			if (!allWavesCompleted)
			{
				wavesCompletedEvent.GameAction?.Invoke();
				allWavesCompleted = true;
			}
		}
	}

	private void DecrementActiveEnemies()
	{
		activeEnemies--;
		if (activeEnemies == 0)
		{
			Debug.Log("Wave " + currentWaveIndex + " completed");
			waveCompletedEvent.Raise(currentWaveIndex + 1);
		}
	}

	private void TriggerNextWave()
	{
		if (waitingForNextWaveTrigger)
		{
			waitingForNextWaveTrigger = false;
			SpawnEnemies(waves[currentWaveIndex]);
		}
	}

	private void OnDestroy()
	{
		// Unsubscribe from events to prevent memory leaks
		enemyDefeatedEvent.GameAction -= DecrementActiveEnemies;
		startNextWave.GameAction -= TriggerNextWave;
	}
}
