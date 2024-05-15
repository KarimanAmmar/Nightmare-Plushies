using UnityEngine;

[CreateAssetMenu(menuName = "Enemy States/Wandering State")]
public class WanderingState : ScriptableObject, IEnemyState
{
	public float patrolRadius = 5f;
	public float moveSpeed = 3f;
	private Vector3 initialPosition;
	private Vector3 targetPosition;
	[SerializeField]float wanderingDistance;
	private Quaternion targetRotation;

	public void EnterState(EnemyController enemy)
	{
		initialPosition = enemy.transform.position;
		ChooseRandomTargetPosition();
	}

	public void UpdateState(EnemyController enemy)
	{
		MoveTowardsTarget(enemy);
		RotateTowardsTarget(enemy);
		CheckReachedTarget(enemy);
	}

	public void ExitState(EnemyController enemy)
	{
		
	}

	private void ChooseRandomTargetPosition()
	{
		Vector2 randomPoint = Random.insideUnitCircle.normalized * patrolRadius;
		targetPosition = initialPosition + new Vector3(randomPoint.x, 0f, randomPoint.y);
		CalculateTargetRotation();
	}

	private void CalculateTargetRotation()
	{
		Vector3 direction = (targetPosition - initialPosition).normalized;
		targetRotation = Quaternion.LookRotation(direction, Vector3.up);
	}

	private void MoveTowardsTarget(EnemyController enemy)
	{
		enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, targetPosition, moveSpeed * Time.deltaTime);
	}

	private void RotateTowardsTarget(EnemyController enemy)
	{
		Vector3 direction = (targetPosition - enemy.transform.position).normalized;
		Quaternion lookRotation = Quaternion.LookRotation(direction);
		enemy.transform.rotation = Quaternion.Slerp(enemy.transform.rotation, lookRotation, Time.deltaTime * 5f);
	}

	private void CheckReachedTarget(EnemyController enemy)
	{
		if (Vector3.Distance(enemy.transform.position, targetPosition) < 0.1f)
		{
			ChooseRandomTargetPosition();
		}
	}

	public void SetPlayerTransform(Transform player)
	{
		throw new System.NotImplementedException();
	}
}
