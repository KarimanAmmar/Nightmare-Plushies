using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemyWaveManager : MonoBehaviour
{
	#region Serialized Fields

	[SerializeField] private List<EnemyPool> enemyPools;
	[SerializeField] private List<WaveData> wavesData;
	[SerializeField] private int currentWaveIndex = 0;
	[SerializeField] private Transform playerTransform;

	#endregion

	#region Private Fields

	private int totalEnemiesInWave = 0;
	private bool spawningInProgress = false;
	private static List<SpawnPoint> spawnPoints = new List<SpawnPoint>();

	#endregion

	#region MonoBehaviour Callbacks

	private void Start()
	{
		StartWave();
	}

	private void Update()
	{
		// Randomly disable an enemy pool when 'C' is pressed
		if (Input.GetKeyDown(KeyCode.C))
		{
			int randomEnemyPoolIndex = Random.Range(0, enemyPools.Count);
			enemyPools[randomEnemyPoolIndex].DisableRandomEnemy();
		}
	}

	#endregion

	#region Public Methods

	/// <summary>
	/// Registers a spawn point.
	/// </summary>
	public static void RegisterSpawnPoint(SpawnPoint point)
	{
		if (!spawnPoints.Contains(point))
		{
			spawnPoints.Add(point);
		}
	}

	/// <summary>
	/// Unregisters a spawn point.
	/// </summary>
	public static void UnregisterSpawnPoint(SpawnPoint point)
	{
		if (spawnPoints.Contains(point))
		{
			spawnPoints.Remove(point);
		}
	}

	#endregion

	#region Wave Management

	/// <summary>
	/// Starts the current wave.
	/// </summary>
	private void StartWave()
	{
		if (currentWaveIndex >= wavesData.Count)
		{
			Debug.LogWarning("No more waves to start.");
			return;
		}

		SetPlayerTransformForEnemies(playerTransform);
		totalEnemiesInWave = wavesData[currentWaveIndex].CalculateTotalEnemies();
		StartCoroutine(SpawnWave(currentWaveIndex));
	}

	/// <summary>
	/// Spawns enemies for the current wave.
	/// </summary>
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
					enemyPools[(int)enemyData.enemyType].ActivateEnemy(spawnPosition.position);
					MarkSpawnPointUnavailable(spawnPosition);
				}
				else
				{
					Debug.LogWarning("No available spawn points.");
				}

				// Add delay between each spawn
				yield return new WaitForSeconds(enemyData.delayBetweenSpawns);
			}
		}

		spawningInProgress = false;
	}


	/// <summary>
	/// Gets a random available spawn point.
	/// </summary>
	private Transform GetRandomAvailableSpawnPoint(List<SpawnPoint> availablePoints)
	{
		if (availablePoints.Count > 0)
		{
			int randomIndex = Random.Range(0, availablePoints.Count);
			return availablePoints[randomIndex].transform;
		}
		return null;
	}

	private Transform GetAvailableSpawnPoint()
	{
		List<SpawnPoint> availablePoints = spawnPoints.FindAll(point => point.IsAvailable);
		if (availablePoints.Count > 0)
		{
			int randomIndex = Random.Range(0, availablePoints.Count);
			return availablePoints[randomIndex].transform;
		}
		return null;
	}

	#endregion

	#region Helper Methods

	/// <summary>
	/// Sets the player transform for all enemy pools.
	/// </summary>
	private void SetPlayerTransformForEnemies(Transform player)
	{
		foreach (EnemyPool pool in enemyPools)
		{
			pool.SetPlayerTransform(player);
		}
	}

	/// <summary>
	/// Marks a spawn point as unavailable.
	/// </summary>
	private void MarkSpawnPointUnavailable(Transform point)
	{
		SpawnPoint spawnPoint = point.GetComponent<SpawnPoint>();
		if (spawnPoint != null)
		{
			spawnPoint.SetAvailability(false);
		}
	}

	#endregion
}
