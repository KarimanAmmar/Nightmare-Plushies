using UnityEngine;

public class Attack_Damage_Blow : MonoBehaviour
{
	[SerializeField] private int damage;
	[SerializeField] private Float_event DamageEvent;
	private bool hasDealtDamage = true;

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player") && hasDealtDamage)
		{
			DamageEvent.Raise(damage);
			hasDealtDamage = false;
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			hasDealtDamage = true; 
		}
	}
}
