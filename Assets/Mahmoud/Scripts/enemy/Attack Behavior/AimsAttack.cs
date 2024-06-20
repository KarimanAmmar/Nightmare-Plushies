using System.Collections;
using UnityEngine;

public class AimsAttack : MonoBehaviour, IAttackBehavior
{
	[SerializeField] private float rotationSpeed = 5f;
	private Transform playerTransform;
	private Rigidbody rb;
	[SerializeField] private EnemyFire enemyFire;
	[SerializeField] private float fireInterval = 5f;
	private float fireTimer = 0f;
	private bool canFire = true;
	private bool isAttack = true;
	[SerializeField] private Animator animator;
	[SerializeField] private string attackAnimation = "Attack";
	[SerializeField] private float attackDistance = 10f;

	void Awake()
	{
		rb = GetComponent<Rigidbody>();
		playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
	}

	public void Attack(EnemyController enemy, Vector3 playerPosition)
	{
	}

	void Update()
	{
		if (playerTransform == null)
			return;

		float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

		if (distanceToPlayer <= attackDistance)
		{
			Vector3 directionToPlayer = playerTransform.position - transform.position;
			Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);
			transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

			if (canFire)
			{
				fireTimer += Time.deltaTime;
				if (fireTimer >= fireInterval)
				{
					if (fireTimer >= fireInterval / 2) animator.SetBool(attackAnimation, true);
					enemyFire.FireBullet();
					fireTimer = 0f;
				}
			}
		}
	}
}
