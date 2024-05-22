using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
	private bool isAvailable = true;

	public bool IsAvailable => isAvailable;

	public void SetAvailability(bool availability)
	{
		isAvailable = availability;
	}

	public void ResetAvailability()
	{
		isAvailable = true;
	}

	private void OnEnable()
	{
		EnemyWaveManager.RegisterSpawnPoint(this);
	}

	private void OnDisable()
	{
		EnemyWaveManager.UnregisterSpawnPoint(this);
	}
}
