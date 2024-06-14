using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashControl : MonoBehaviour
{
	[SerializeField] private GameEvent fireSlash;
	[SerializeField] private float slashDamage = 10f;
	[SerializeField] private List<ParticleSystem> slashEffects;
	[SerializeField] private GameObject slashControl;
	[SerializeField] private float speed = 1f;
	[SerializeField] private float moveDuration = 1f;
	[SerializeField] private Transform Player;
	[SerializeField] private CharacterMovementManager characterMovementManager;
	private bool canSlash = true;
	private Vector3 startPos;

	private void Awake()
	{
		Stop_Particle();
		fireSlash.GameAction += Slash_Logic;
	}

	private void OnDestroy()
	{
		fireSlash.GameAction -= Slash_Logic;
	}

	void Slash_Logic()
	{
		StartCoroutine(Play_ParticleAfterDelay(0.002f));
		StartCoroutine(MoveSlashControl(0.001f));
	}

	private IEnumerator MoveSlashControl(float delay)
	{
		yield return new WaitForSeconds(delay);
		float elapsedTime = 0f;
		startPos = slashControl.transform.localPosition;
		while (elapsedTime < moveDuration)
		{
			slashControl.transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.Self);
			elapsedTime += Time.deltaTime;
			yield return null;
		}

		slashControl.transform.localPosition = startPos;
		Stop_Particle();
	}

	void Stop_Particle()
	{
		
		foreach (ParticleSystem slashEffect in slashEffects)
		{
			slashEffect.Stop();
		}
		slashControl.transform.parent = Player;
		slashControl.transform.localPosition = new Vector3(0, 0, 1);
		slashControl.transform.localRotation = Quaternion.Euler(0, 0, 0);
		slashControl.SetActive(false);
	}

	IEnumerator Play_ParticleAfterDelay(float delay)
	{
		yield return new WaitForSeconds(delay); // Wait for the specified delay
		slashControl.transform.parent = null;
		slashControl.SetActive(true);
		characterMovementManager.IsSlashFire = false;
		foreach (ParticleSystem slashEffect in slashEffects)
		{
			slashEffect.Play();
		}
	}
}
