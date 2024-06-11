using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Wave
{
	public WaveData waveData;
	[SerializeField] private Collider waveCollider;
	[SerializeField] private bool IsTrigger;
	[SerializeField] private GameObject miniBoss;
	[SerializeField] private List<Transform> spawnPoints;
	public List<Transform> SpawnPoints { get => spawnPoints; set => spawnPoints = value; }
	public Collider WaveCollider { get => waveCollider; set => waveCollider = value; }
	public bool IsTrigger1 { get => IsTrigger; set => IsTrigger = value; }
}
