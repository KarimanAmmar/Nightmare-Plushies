using UnityEngine;

[System.Serializable]
public class ChasingState : IEnemyState
{
	[SerializeField] private float moveSpeed = 5f;
	private Rigidbody rb;

	public void EnterState(EnemyController enemy)
	{
		rb = enemy.GetComponent<Rigidbody>();
	}

	public void ExitState(EnemyController enemy)
	{

	}

	public void UpdateState(EnemyController enemy, Vector3 playerPosition)
	{
		MoveTowardsPlayer(playerPosition);
	}

	private void MoveTowardsPlayer(Vector3 playerPosition)
	{
		Vector3 moveDirection = (playerPosition - rb.position).normalized;
		rb.MovePosition(rb.position + moveDirection * moveSpeed * Time.deltaTime);
	}
}
