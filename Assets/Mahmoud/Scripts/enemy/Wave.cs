using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Wave
{
	public WaveData waveData;
	[SerializeField] private bool byTrigger;
	[SerializeField] private List<Transform> spawnPoints;

	public List<Transform> SpawnPoints { get => spawnPoints; set => spawnPoints = value; }
	public bool ByTrigger { get => byTrigger; set => byTrigger = value; }
}
