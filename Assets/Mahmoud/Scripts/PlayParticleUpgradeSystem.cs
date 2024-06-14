using System.Collections;
using UnityEngine;

public class PlayParticleUpgradeSystem : MonoBehaviour
{
	[SerializeField] ParticleSystem particleSystem;
	[SerializeField] GameEvent UpgradeEvent;

	void Start()
	{
		particleSystem = GetComponent<ParticleSystem>();

		if (particleSystem == null)
		{
			Debug.LogError("No ParticleSystem found on the GameObject.");
			return;
		}

		particleSystem.Stop();
		particleSystem.Clear();
		gameObject.SetActive(false);
	}

	public void PlayParticle()
	{
		gameObject.SetActive(true);
		particleSystem.Play();
		StartCoroutine(DisableAfterCompletion());
	}

	private IEnumerator DisableAfterCompletion()
	{
		while (particleSystem.isPlaying)
		{
			yield return null;
		}

		gameObject.SetActive(false);
	}
}
