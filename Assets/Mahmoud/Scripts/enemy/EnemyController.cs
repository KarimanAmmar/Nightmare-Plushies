using UnityEngine;

public class EnemyController : MonoBehaviour
{
	public IEnemyState currentState;
	public WanderingState wanderingState;
	public ChasingState chasingState;
	public AttackingState attackingState;
	// Reference to the player's transform
	private Transform playerTransform;
	public float chaseDistance = 10f;
	public float attackDistance = 2f;

	private void Awake()
	{
		GameObject player = GameObject.FindWithTag("Player");
		if (player != null)
		{
			playerTransform = player.transform;
		}
		else
		{
			Debug.LogError("Player not found!");
		}
	}
	private void Start()
	{
		TransitionToState(wanderingState);
	}

	public void SetPlayerTransform(Transform playerTransform)
	{
		this.playerTransform = playerTransform;
	}
	private void Update()
	{
		float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);
		TransitionStateBasedOnDistance(distanceToPlayer);
		currentState.UpdateState(this);

	}

	private void TransitionStateBasedOnDistance(float distanceToPlayer)
	{
		if (currentState != attackingState && distanceToPlayer <= attackDistance)
		{
			TransitionToState(attackingState);
		}
		else if (currentState != chasingState && distanceToPlayer > attackDistance && distanceToPlayer <= chaseDistance)
		{
			TransitionToState(chasingState);
		}
		else if (currentState != wanderingState && distanceToPlayer > chaseDistance)
		{
			TransitionToState(wanderingState);
		}
	}

	public void TransitionToState(IEnemyState nextState)
	{
		if (currentState != null)
			currentState.ExitState(this);

		currentState = nextState;
		currentState.EnterState(this);

		if (nextState == chasingState)
		{
			chasingState.SetPlayerTransform(playerTransform);
		}
	}
}