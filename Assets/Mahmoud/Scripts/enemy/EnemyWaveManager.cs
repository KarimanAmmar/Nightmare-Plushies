using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
	[SerializeField] private List<Wave> Waves;
	[SerializeField] private Transform playerTransform;
	[SerializeField] private List<EnemyPool> enemyPools;
	[SerializeField] private int currentWaveIndex = 0;
	[SerializeField] private int TotalEnemy;
	private bool spawningInProgress = false;
	[SerializeField] private int activeEnemies = 0;
	[SerializeField] private GameEvent enemyDefeatedEvent;
	[SerializeField] private GameEvent WavesCompeleted;
	private bool allWavesCompleted = false;  

	private void Awake()
	{
		foreach (Wave wave in Waves)
		{
			wave.waveData.isWaveCompleted = false;
		}
		SetPlayerTransformForEnemies(playerTransform);

		// Subscribe to the enemyDefeatedEvent
		enemyDefeatedEvent.GameAction += DecrementActiveEnemies;
	}

	private void Start()
	{
		if (Waves.Count > 0)
		{
			currentWaveIndex = -1;
		}
	}

	private void Update()
	{
		if (activeEnemies == 0 && !allWavesCompleted)  // Check if not all waves are completed
		{
			NextWave();
		}
	}

	private void SpawnEnemies(Wave wave)
	{
		TotalEnemy = wave.waveData.CalculateTotalEnemies();
		activeEnemies = TotalEnemy;
		//spawningInProgress = true;

		StartCoroutine(SpawnEnemyCoroutine(wave));
	}

	private IEnumerator SpawnEnemyCoroutine(Wave wave)
	{
		foreach (NumberOfEnemies enemyData in wave.waveData.TypeOfEnemies)
		{
			for (int i = 0; i < enemyData.numberOfEnemy; i++)
			{
				Transform spawnPoint = wave.SpawnPoints[Random.Range(0, wave.SpawnPoints.Count)];
				GameObject newEnemy = enemyPools[(int)enemyData.enemyType].ActivateEnemy(spawnPoint.position);
				yield return new WaitForSeconds(enemyData.delayBetweenSpawns);
			}
		}
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
		if (currentWaveIndex < Waves.Count)
		{
			SpawnEnemies(Waves[currentWaveIndex]);
		}
		else
		{
			if (!allWavesCompleted)  
			{
				WavesCompeleted.GameAction?.Invoke();
				allWavesCompleted = true;  
			}
		}
	}

	private void DecrementActiveEnemies()
	{
		activeEnemies--;
	}

	private void OnDestroy()
	{
		enemyDefeatedEvent.GameAction -= DecrementActiveEnemies;
	}
}
