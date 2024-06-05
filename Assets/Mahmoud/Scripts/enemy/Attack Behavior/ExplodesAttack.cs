using UnityEngine;

public class ExplodesAttack : MonoBehaviour, IAttackBehavior
{
	[SerializeField] private Animator animator;
	[SerializeField] private ParticleSystem particleSystem;

	public void Attack(EnemyController enemy, Vector3 playerPosition)
	{
		
		Logging.Log("Performing explodes attack");


		if (particleSystem != null)
		{
			particleSystem.Play();
		}
	}
}
