using UnityEngine;

public class AimsAttack : MonoBehaviour, IAttackBehavior
{
	[SerializeField] private Animator animator;
	[SerializeField] private ParticleSystem particleSystem;

	public void Attack(EnemyController enemy, Vector3 playerPosition)
	{
		Logging.Log("Performing aims attack");

		// Implement your aims attack logic here
		if (animator != null)
		{
			animator.SetTrigger("Aim");
		}

		if (particleSystem != null)
		{
			particleSystem.Play();
		}
	}
}
