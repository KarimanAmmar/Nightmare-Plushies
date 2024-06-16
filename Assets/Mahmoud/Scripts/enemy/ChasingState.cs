using UnityEngine;

[System.Serializable]
public class ChasingState : IEnemyState
{
	[SerializeField] private float moveSpeed = 5f;
	[SerializeField] private float rotationSpeed = 5f; 
	private Rigidbody rb;

	public void EnterState(EnemyController enemy)
	{
		rb = enemy.GetComponent<Rigidbody>();
	}

	public void ExitState(EnemyController enemy)
	{
		// Any cleanup when exiting the state can be done here
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
		Vector3 direction = (playerPosition - rb.position).normalized;
		rb.transform.forward = direction;
		/*Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z)); 
		// CHANGE FARWARD TO DIRECTION TO MAKE THE ENEMY LOOK AT THE PLAYER
		rb.rotation = Quaternion.Slerp(rb.rotation, lookRotation, rotationSpeed * Time.deltaTime);*/
	}

}
