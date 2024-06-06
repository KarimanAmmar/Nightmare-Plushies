using UnityEngine;

public class ParticleSystemController : MonoBehaviour
{
	[SerializeField] private Float_event DamageEvent;
	[SerializeField] private float damageValue = 10f;
	private bool eventFired = false;

	private void OnParticleCollision(GameObject other)
	{
		if (other.CompareTag("Player") && !eventFired)
		{

			DamageEvent.Raise(damageValue);
			eventFired = true;
		}
	}
}
