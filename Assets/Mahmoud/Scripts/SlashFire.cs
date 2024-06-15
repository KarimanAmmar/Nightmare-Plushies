using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SlashFire : MonoBehaviour
{
	private bool canFire = true;
	[SerializeField] private float fireDelay = 1f;
	[SerializeField] private Image fireCooldownImage;
	[SerializeField] private GameObject DisableFire;
	[SerializeField] CharacterController controllerPlayer;
	[SerializeField] private GameEvent fireSlash;
	[SerializeField] private Joystick PlayFire;
	[SerializeField] private GameObject DecalArraw;
	[SerializeField] private CharacterMovementManager characterMovementManager;
	private bool isFiring = false;
	Vector3 joystickDirection;
	private void Start()
	{
		if (fireCooldownImage != null)
		{
			fireCooldownImage.fillAmount = 0f;
		}
	}

	private void Update()
	{
		if (PlayFire.Horizontal != 0 || PlayFire.Vertical != 0)
		{
			isFiring = true;
			DecalArraw.SetActive(true);
			joystickDirection = new Vector3(-PlayFire.Horizontal, 0f, -PlayFire.Vertical);

			if (DecalArraw != null)
			{
				DecalArraw.transform.rotation = Quaternion.LookRotation(joystickDirection);
			}
		}

		if (isFiring && PlayFire.Horizontal == 0 && PlayFire.Vertical == 0)
		{
			isFiring = false;
			Fire();
			DisableFire.SetActive(true);
			StartCoroutine(LerpRotation(controllerPlayer.transform.rotation, Quaternion.LookRotation(joystickDirection), 0.25f));
			characterMovementManager.IsSlashFire = true;
		}
	}

	private IEnumerator LerpRotation(Quaternion startRotation, Quaternion targetRotation, float duration)
	{
		float elapsedTime = 0f;

		while (elapsedTime < duration)
		{
			elapsedTime += Time.deltaTime;
			controllerPlayer.transform.rotation = Quaternion.Lerp(startRotation, targetRotation, elapsedTime / duration);
			yield return null;
		}

		controllerPlayer.transform.rotation = targetRotation;
	}

	public void Fire()
	{
		if (canFire)
		{
			fireSlash.GameAction?.Invoke();
			StartCoroutine(FireDelay());
			DecalArraw.SetActive(false);
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
				fireCooldownImage.fillAmount = elapsedTime / fireDelay;
			}

			yield return null;
		}

		canFire = true;

		if (fireCooldownImage != null)
		{
			fireCooldownImage.fillAmount = 0f;
			DisableFire.SetActive(false);
		}
	}
}
