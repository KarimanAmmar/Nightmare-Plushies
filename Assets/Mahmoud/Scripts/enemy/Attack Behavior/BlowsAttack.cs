using UnityEngine;

public class BlowsAttack : MonoBehaviour, IAttackBehavior
{
	[SerializeField] private Animator animator;
	
	private bool hasDealtDamage = true;
	[SerializeField] private string attackAnimation = "Attack";
	[SerializeField] private string idleAnimation = "idle";
	[SerializeField] private float attackMoveSpeed = 2f; // Speed at which the attacker moves towards the player

	private Transform playerTransform;
	private bool isAttacking = false;


	private void Update()
	{
		if (isAttacking && playerTransform != null)
		{
			// Move towards the player
			Vector3 direction = (playerTransform.position - transform.position).normalized;
			transform.position += direction * attackMoveSpeed * Time.deltaTime;

			// Face the player
			transform.LookAt(new Vector3(playerTransform.position.x, transform.position.y, playerTransform.position.z));
		}
	}

	public void Attack(EnemyController enemy, Vector3 playerPosition)
	{
		float distance = Vector3.Distance(enemy.transform.position, playerPosition);

		if (distance <= 4f && hasDealtDamage)
		{
			animator.SetBool("Attack", true);
		}
		isAttacking = true;
		playerTransform = enemy.transform;
	}


	
}
