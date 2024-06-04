using UnityEngine;

public class WaveStartTrigger : MonoBehaviour
{
	public delegate void PlayerEnter(Collider trigger);
	public event PlayerEnter OnPlayerEnter;

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			OnPlayerEnter?.Invoke(GetComponent<Collider>());
			gameObject.SetActive(false); 
		}
	}
}
