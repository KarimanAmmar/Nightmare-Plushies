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
		LookAtPlayer(playerPosition);
	}

	private void MoveTowardsPlayer(Vector3 playerPosition)
	{
		Vector3 moveDirection = (playerPosition - rb.position).normalized;
		rb.MovePosition(rb.position + moveDirection * moveSpeed * Time.deltaTime);
	}

	private void LookAtPlayer(Vector3 playerPosition)
	{
		Vector3 lookDirection = (playerPosition - rb.position).normalized;
		Quaternion targetRotation = Quaternion.LookRotation(new Vector3(lookDirection.x, 0f, lookDirection.z));
		rb.rotation = Quaternion.Lerp(rb.rotation, targetRotation, Time.deltaTime);
	}
}
