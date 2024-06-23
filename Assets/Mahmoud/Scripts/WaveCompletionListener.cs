using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveCompletionListener : MonoBehaviour
{
	[SerializeField] private int_Event waveCompletedEvent; // Event for each wave completed
	[SerializeField] private GameEvent wavesCompletedEvent; // Event for all waves completed

	private void OnEnable()
	{
		waveCompletedEvent.RegisterListener(OnWaveCompleted);
		wavesCompletedEvent.GameAction += OnAllWavesCompleted;
	}

	private void OnDisable()
	{
		waveCompletedEvent.UnregisterListener(OnWaveCompleted);
		wavesCompletedEvent.GameAction -= OnAllWavesCompleted;
	}

	private void OnWaveCompleted(int waveNumber)
	{
		////Logging.Log($"Wave {waveNumber} completed!");
	}

	private void OnAllWavesCompleted()
	{
		////Logging.Log("All waves completed!");
	}
}
