using UnityEngine;

/// <summary>
/// Controls the behavior of an enemy character.
/// </summary>
public class EnemyController : MonoBehaviour
{
	[SerializeField]
	private IEnemyState currentState;
	[SerializeField]
	private WanderingState wanderingState;
	[SerializeField]
	private ChasingState chasingState;
	[SerializeField]
	private AttackingState attackingState;
	// Reference to the player's transform
	[SerializeField]
	private Transform playerTransform;
	[SerializeField]
	private float chaseDistance = 10f;
	[SerializeField]
	private float attackDistance = 2f;

	/// <summary>
	/// Finds the player object and sets the player's transform.
	/// </summary>
	private void Awake()
	{
		GameObject player = GameObject.FindWithTag("Player");
		if (player != null)
		{
			playerTransform = player.transform;
		}
		
	}

	/// <summary>
	/// Sets the initial state of the enemy to wandering.
	/// </summary>
	private void Start()
	{
		TransitionToState(wanderingState);
	}

	/// <summary>
	/// Sets the player's transform.
	/// </summary>
	/// <param name="playerTransform">The transform of the player.</param>
	public void SetPlayerTransform(Transform playerTransform)
	{
		this.playerTransform = playerTransform;
	}

	/// <summary>
	/// Updates the enemy's state based on the distance to the player.
	/// </summary>
	private void Update()
	{
		float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);
		TransitionStateBasedOnDistance(distanceToPlayer);
		currentState.UpdateState(this);
	}

	/// <summary>
	/// Transitions the enemy's state based on the distance to the player.
	/// </summary>
	/// <param name="distanceToPlayer">The distance to the player.</param>
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

	/// <summary>
	/// Transitions the enemy to a new state.
	/// </summary>
	/// <param name="nextState">The next state to transition to.</param>
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
