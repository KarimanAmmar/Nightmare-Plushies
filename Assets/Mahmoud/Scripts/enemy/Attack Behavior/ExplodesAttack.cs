using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodesAttack : MonoBehaviour, IAttackBehavior
{
	[SerializeField] private Animator animator;
	[SerializeField] private ParticleSystem particleSystem;
	[SerializeField] private GameObject root;
	[SerializeField] private float scaleDuration = 3f;
	[SerializeField] private float maxScale = 1.3f;
	[SerializeField] private float scaleSpeed = 1.5f;
	[SerializeField] private GameEvent enemyDefeatedEvent;
	private bool isAttacking = false;
	private Transform playerTransform;
	[SerializeField] private List<Material> materials;
	//audio
	[SerializeField] private jackSFX jackSFX;
	void Awake()
	{
		playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
		if (particleSystem != null)
		{
			particleSystem.gameObject.SetActive(false);
		}
	}
	public void Update()
	{
		//Logging.Log("Performing explodes attack");

		float distance = Vector3.Distance(transform.position, playerTransform.position);

		if (distance <= 3f )
		{
			
			EnableParticleSystem();
		}
	}

	private void EnableParticleSystem()
	{
		if (particleSystem != null)
		{
			particleSystem.gameObject.SetActive(true);
			StartCoroutine(DisableParentAfterParticleSystem());
		}
	}

	private IEnumerator DisableParentAfterParticleSystem()
	{
		yield return new WaitForSeconds(particleSystem.main.duration/2);

		
		if (root != null)
		{
            jackSFX.PlayExploadeClip();
            particleSystem.gameObject.SetActive(false);
			root.SetActive(false);
			enemyDefeatedEvent.GameAction?.Invoke();
		}
	}

	public void Attack(EnemyController enemy, Vector3 playerPosition)
	{
		throw new System.NotImplementedException();
	}
}
