using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Wave
{
	public WaveData waveData;
	[SerializeField] private List<Transform> spawnPoints;
	[SerializeField] private Collider startTrigger;
	[SerializeField] private bool startWavesByTrigger;

	public Collider StartTrigger { get => startTrigger; set => startTrigger = value; }
	public List<Transform> SpawnPoints { get => spawnPoints; set => spawnPoints = value; }
	public bool StartWavesByTrigger { get => startWavesByTrigger; set => startWavesByTrigger = value; }
}
