using UnityEngine;

[System.Serializable]
public class WanderingState : IEnemyState
{
	[SerializeField] private float patrolRadius = 5f;
	[SerializeField] private float moveSpeed = 3f;
	[SerializeField] private float rotationSpeed = 5f;
	[SerializeField] private float obstacleDetectionDistance = 1f;
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
		if (IsObstacleDetected())
		{
			ChooseRandomTargetPosition();
		}

		MoveTowardsTarget();
		CheckReachedTarget();
	}

	public void ExitState(EnemyController enemy)
	{

	}

	private void ChooseRandomTargetPosition()
	{
		Vector2 randomPoint = Random.insideUnitCircle * patrolRadius;
		targetPosition = initialPosition + new Vector3(randomPoint.x, 0f, randomPoint.y);
	}

	private void MoveTowardsTarget()
	{
		Vector3 moveDirection = (targetPosition - rb.position).normalized;

		// Rotate towards the target position
		Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
		rb.rotation = Quaternion.Slerp(rb.rotation, targetRotation, rotationSpeed * Time.deltaTime);

		rb.MovePosition(rb.position + moveDirection * moveSpeed * Time.deltaTime);
	}

	private void CheckReachedTarget()
	{
		if (Vector3.Distance(rb.position, targetPosition) < 0.1f)
		{
			ChooseRandomTargetPosition();
		}
	}

	private bool IsObstacleDetected()
	{
		RaycastHit hit;
		if (Physics.Raycast(rb.position, rb.transform.forward, out hit, obstacleDetectionDistance))
		{
			return true;
		}
		return false;
	}

	private void OnCollisionEnter(Collision collision)
	{
		ChooseRandomTargetPosition();
	}
}
