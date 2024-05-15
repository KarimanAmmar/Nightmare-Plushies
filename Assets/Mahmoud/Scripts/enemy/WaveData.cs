using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WaveData
{
	public int waveID;
	public string waveName;
	public List<NumberOfEnemies> TypeOfEnemies;
	public List<Transform> spawnPoints;
	public float delayBeforeWaveStarts;
	public bool isWaveCompleted;

	public int CalculateTotalEnemies()
	{
		int totalEnemies = 0;
		foreach (NumberOfEnemies enemyData in TypeOfEnemies)
		{
			totalEnemies += enemyData.numberOfEnemy;
		}
		return totalEnemies;
	}

}

[System.Serializable]
public struct NumberOfEnemies
{
	public EnemyType enemyType;
	public int numberOfEnemy;
	public float delayBeforestart;
	public int delayBetweenSpawns;
}
