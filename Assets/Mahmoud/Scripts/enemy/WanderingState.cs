using UnityEngine;

[System.Serializable]
public class WanderingState : IEnemyState
{
	[SerializeField] private float patrolRadius = 5f;
	[SerializeField] private float moveSpeed = 3f;
	private Vector3 initialPosition;
	private Vector3 targetPosition;
	private Rigidbody rb;

	public void EnterState(EnemyController enemy)
	{
		initialPosition = enemy.transform.position;
		ChooseRandomTargetPosition();
		rb = enemy.GetComponent<Rigidbody>();
	}

	public void UpdateState(EnemyController enemy, Vector3 playerPosition)
	{
		MoveTowardsTarget();
		CheckReachedTarget();
	}

	public void ExitState(EnemyController enemy)
	{

	}

	private void ChooseRandomTargetPosition()
	{
		Vector2 randomPoint = Random.insideUnitCircle.normalized * patrolRadius;
		targetPosition = initialPosition + new Vector3(randomPoint.x, 0f, randomPoint.y);
	}

	private void MoveTowardsTarget()
	{
		Vector3 moveDirection = (targetPosition - rb.position).normalized;
		rb.MovePosition(rb.position + moveDirection * moveSpeed * Time.deltaTime);
	}

	private void CheckReachedTarget()
	{
		if (Vector3.Distance(rb.position, targetPosition) < 0.1f)
		{
			ChooseRandomTargetPosition();
		}
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (!collision.gameObject.CompareTag("Ground"))
		{
			ChooseRandomTargetPosition();
		}
	}
}
