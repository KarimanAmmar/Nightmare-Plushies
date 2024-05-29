using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
	[SerializeField] private bool isAvailable = true;

	public bool IsAvailable
	{
		get { return isAvailable; }
	}

	public void SetAvailability(bool availability)
	{
		isAvailable = availability;
	}

	public void ResetAvailability()
	{
		isAvailable = true;
	}
}
