using UnityEngine;

public class WaveStartTrigger : MonoBehaviour
{
	public delegate void PlayerEnter(Collider trigger);
	public event PlayerEnter OnPlayerEnter;

	[SerializeField] private GameEvent startNextWave;

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			OnPlayerEnter?.Invoke(GetComponent<Collider>());
			startNextWave.GameAction?.Invoke();
			gameObject.SetActive(false);
		}
	}
}
