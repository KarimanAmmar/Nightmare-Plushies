using UnityEngine;

public class EnemyController : MonoBehaviour
{
	[SerializeField] private WanderingState wanderingState;
	[SerializeField] private ChasingState chasingState;
	[SerializeField] private AttackingState attackingState;
	[SerializeField] private float attackDistance;
	[SerializeField] private float chaseDistance;
	private Transform playerTransform;
	private IEnemyState currentState;
	private Rigidbody rb;

	private void Awake()
	{
		rb = GetComponent<Rigidbody>();
	}

	private void Start()
	{
		TransitionToState(wanderingState);
	}

	private void Update()
	{
		if (playerTransform != null)
		{
			float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);
			TransitionStateBasedOnDistance(distanceToPlayer);
			currentState.UpdateState(this, playerTransform.position);
		}
		else
		{
			Debug.LogWarning("PlayerTransform is null. Make sure the player's transform is assigned in the Inspector.");
		}
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
	}

	public Rigidbody GetRigidbody()
	{
		return rb;
	}

	public void SetPlayerTransform(Transform player)
	{
		playerTransform = player;
	}
}
