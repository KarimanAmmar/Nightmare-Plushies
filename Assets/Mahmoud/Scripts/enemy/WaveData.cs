using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WaveData
{
	[SerializeField]
	private int waveID;
	public int WaveID
	{
		get { return waveID; }
		set { waveID = value; }
	}
	private string waveName;
	public string WaveName
	{
		get { return waveName; }
		set { waveName = value; }
	}
	private List<NumberOfEnemies> typeOfEnemies;
	public List<NumberOfEnemies> TypeOfEnemies
	{
		get { return typeOfEnemies; }
		set { typeOfEnemies = value; }
	}

	[SerializeField]
	private List<Transform> spawnPoints;
	public List<Transform> SpawnPoints
	{
		get { return spawnPoints; }
		set { spawnPoints = value; }
	}

	[SerializeField]
	private float delayBeforeWaveStarts;
	public float DelayBeforeWaveStarts
	{
		get { return delayBeforeWaveStarts; }
		set { delayBeforeWaveStarts = value; }
	}

	[SerializeField]
	private bool isWaveCompleted;
	public bool IsWaveCompleted
	{
		get { return isWaveCompleted; }
		set { isWaveCompleted = value; }
	}

	public int CalculateTotalEnemies()
	{
		int totalEnemies = 0;
		foreach (NumberOfEnemies enemyData in TypeOfEnemies)
		{
			totalEnemies += enemyData.NumberOfEnemy;
		}
		return totalEnemies;
	}
}

[System.Serializable]
public struct NumberOfEnemies
{
	[SerializeField] 
	private EnemyType enemyType;
	[SerializeField] 
	private int numberOfEnemy;
	[SerializeField] 
	private float delayBeforestart;
	[SerializeField] 
	private int delayBetweenSpawns;

	public int NumberOfEnemy { get => numberOfEnemy; set => numberOfEnemy = value; }
	public EnemyType EnemyType { get => enemyType; set => enemyType = value; }
	public int DelayBetweenSpawns { get => delayBetweenSpawns; set => delayBetweenSpawns = value; }
}
