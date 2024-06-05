using UnityEngine;

public class BlowsAttack : MonoBehaviour, IAttackBehavior
{
	[SerializeField] private Animator animator;
	[SerializeField] private ParticleSystem particleSystem;
	[SerializeField] private Collider attackCollider;
	[SerializeField] private int damage;

	public void Attack(EnemyController enemy, Vector3 playerPosition)
	{
		Logging.Log("Performing blows attack");

		// Implement your blows attack logic here
		if (animator != null)
		{
			animator.SetTrigger("Blow");
		}

		if (particleSystem != null)
		{
			particleSystem.Play();
		}

		if (attackCollider != null)
		{
			// Enable the collider for the duration of the attack
			attackCollider.enabled = true;

		}
	}

	public void EndAttack()
	{
		if (attackCollider != null)
		{
			attackCollider.enabled = false;
		}
	}
}
