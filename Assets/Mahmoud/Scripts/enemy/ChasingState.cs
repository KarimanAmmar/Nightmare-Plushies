using UnityEngine;

[CreateAssetMenu(menuName = "Enemy States/Chasing State")]
public class ChasingState : ScriptableObject, IEnemyState
{
	[SerializeField] private float wanderingDistance;
	[SerializeField] private float moveSpeed = 5f;
	[SerializeField] private Transform playerTransform; 

	public void SetPlayerTransform(Transform player)
	{
		playerTransform = player;
	}

	public void EnterState(EnemyController enemy)
	{

	}

	public void ExitState(EnemyController enemy)
	{
		playerTransform = null;
	}

	public void UpdateState(EnemyController enemy)
	{
		if (playerTransform == null)
			return;

		MoveTowardsPlayer(enemy);
		RotateTowardsPlayer(enemy);
	}

	private void MoveTowardsPlayer(EnemyController enemy)
	{
		enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, playerTransform.position, moveSpeed * Time.deltaTime);
	}

	private void RotateTowardsPlayer(EnemyController enemy)
	{
		Vector3 direction = (playerTransform.position - enemy.transform.position).normalized;
		Quaternion lookRotation = Quaternion.LookRotation(direction);
		enemy.transform.rotation = Quaternion.Slerp(enemy.transform.rotation, lookRotation, Time.deltaTime * 5f);
	}
}
