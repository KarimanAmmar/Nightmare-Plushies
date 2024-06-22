using System;
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

	public event Action OnDefeated;

	[SerializeField] private EnemyType enemyType;
	[SerializeField] private MonoBehaviour attackBehavior;

	private IAttackBehavior attackBehaviorInstance;

	public IEnemyState CurrentState { get => currentState; set => currentState = value; }
	public WanderingState WanderingState { get => wanderingState; set => wanderingState = value; }

	private void OnEnable()
	{
		if (playerTransform != null)
		{
			InitializeState();
		}
	}

	private void OnDisable()
	{
		OnDefeated?.Invoke();
	}

	public void Defeat()
	{
		gameObject.SetActive(false);
		OnDefeated?.Invoke();
	}

	private void Awake()
	{
		rb = GetComponent<Rigidbody>();

		if (attackBehavior is IAttackBehavior)
		{
			attackBehaviorInstance = (IAttackBehavior)attackBehavior;
			attackingState = new AttackingState(attackBehaviorInstance);
		}
		else
		{
			Debug.LogError("Attack behavior must implement IAttackBehavior interface");
		}
	}

	private void Start()
	{
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		if (player != null)
		{
			playerTransform = player.transform;
			InitializeState();
		}
		else
		{
			Debug.LogError("Player not found! Make sure the player object has the 'Player' tag.");
		}
	}

	private void InitializeState()
	{
		TransitionToState(WanderingState);
	}

	private void Update()
	{
		if (playerTransform != null)
		{
			float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);
			TransitionStateBasedOnDistance(distanceToPlayer);
			if(currentState == chasingState || currentState == wanderingState) 
			CurrentState.UpdateState(this, playerTransform.position);
		}
	}

	private void TransitionStateBasedOnDistance(float distanceToPlayer)
	{
		if (CurrentState != attackingState && distanceToPlayer <= attackDistance)
		{
			TransitionToState(attackingState);
		}
		else if (CurrentState != chasingState && distanceToPlayer > attackDistance && distanceToPlayer <= chaseDistance)
		{
			TransitionToState(chasingState);
		}
		else if (CurrentState != WanderingState && distanceToPlayer > chaseDistance)
		{
			TransitionToState(WanderingState);
		}
	}

	public void TransitionToState(IEnemyState nextState)
	{
		if (CurrentState != null)
		{
			CurrentState.ExitState(this);
		}

		CurrentState = nextState;
		CurrentState.EnterState(this);
	}

	public Rigidbody GetRigidbody()
	{
		return rb;
	}

	public Transform GetPlayerTransform()
	{
		return playerTransform;
	}

	public void SetPlayerTransform(Transform player)
	{
		playerTransform = player;
	}
}
