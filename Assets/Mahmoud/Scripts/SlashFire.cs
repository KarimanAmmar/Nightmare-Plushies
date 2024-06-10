using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SlashFire : MonoBehaviour
{
	private bool canFire = true;
	[SerializeField] float fireDelay = 1f;
	[SerializeField] Image fireCooldownImage;
	[SerializeField] private GameEvent fireSlash;
	

	void Start()
	{
		if (fireCooldownImage != null)
		{
			fireCooldownImage.fillAmount = 1f;
		}
	}

	public void Fire()
	{
		if (canFire)
		{
			fireSlash.GameAction?.Invoke();
			StartCoroutine(FireDelay());
		}
	}

	private IEnumerator FireDelay()
	{

		canFire = false;

		float elapsedTime = 0f;

		while (elapsedTime < fireDelay)
		{
			elapsedTime += Time.deltaTime;

			if (fireCooldownImage != null)
			{
				fireCooldownImage.fillAmount = Mathf.Clamp01(elapsedTime / fireDelay);
			}

			yield return null;
		}

		canFire = true;
	}
}
