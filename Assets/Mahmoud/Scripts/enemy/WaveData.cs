using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WaveData", menuName = "Enemy/WaveData", order = 1)]
public class WaveData : ScriptableObject
{
	[SerializeField] private int waveID;
	[SerializeField] private string waveName;
	[SerializeField] private List<NumberOfEnemies> typeOfEnemies;
	[SerializeField] private float delayBeforeWaveStarts;
	[SerializeField] public bool isWaveCompleted = false;

	public List<NumberOfEnemies> TypeOfEnemies => typeOfEnemies;
	public float DelayBeforeWaveStarts => delayBeforeWaveStarts;

	public int WaveID { get => waveID; set => waveID = value; }

	public int CalculateTotalEnemies()
	{
		int totalEnemies = 0;
		foreach (NumberOfEnemies enemyData in typeOfEnemies)
		{
			totalEnemies += enemyData.numberOfEnemy;
		}
		return totalEnemies;
	}
	public void MarkWaveCompleted()
	{
		isWaveCompleted = true;
	}

}

[System.Serializable]
public struct NumberOfEnemies
{
	public EnemyType enemyType;
	public int numberOfEnemy;
	public float delayBetweenSpawns;
}
