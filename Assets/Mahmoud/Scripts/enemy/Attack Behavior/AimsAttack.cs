using UnityEngine;

public class AimsAttack : MonoBehaviour, IAttackBehavior
{
	[SerializeField] private float rotationSpeed = 5f;
	private Transform playerTransform;
	private Rigidbody rb;
	[SerializeField] private EnemyShooting enemyShooting;
	public void EnterState(EnemyController enemy)
	{
		rb = enemy.GetComponent<Rigidbody>();
	}
	public void Attack(EnemyController enemy, Vector3 playerPosition)
	{
		playerTransform = enemy.GetPlayerTransform();
	}

	private void Update()
	{
		// Always look at the player
		if (playerTransform != null)
		{
			Vector3 directionToPlayer = playerTransform.position - transform.position;
			Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);
			transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
		}
	}

	private void LookAtPlayer(Vector3 playerPosition)
	{
		Vector3 direction = (playerPosition - rb.position).normalized;
		Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
		rb.rotation = Quaternion.Slerp(rb.rotation, lookRotation, rotationSpeed * Time.deltaTime);
	}
}
