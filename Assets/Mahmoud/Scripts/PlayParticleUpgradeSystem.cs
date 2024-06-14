using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayParticleUpgradeSystem : MonoBehaviour
{
	[SerializeField] List<ParticleSystem> particleSystems;
	[SerializeField] GameEvent UpgradeEvent;
	private void Awake()
	{
		// Ensure particleSystems list is assigned via the inspector if not already done
		if (particleSystems == null || particleSystems.Count == 0)
		{
			particleSystems = new List<ParticleSystem>(GetComponents<ParticleSystem>());
			if (particleSystems.Count == 0)
			{
				Debug.LogError("No ParticleSystems found on the GameObject.");
				return;
			}
		}

		// Subscribe to the UpgradeEvent
		if (UpgradeEvent != null)
		{
			UpgradeEvent.GameAction += PlayParticle;
		}
		else
		{
			Debug.LogError("UpgradeEvent is not assigned.");
		}

		foreach (var ps in particleSystems)
		{
			ps.Stop();
			ps.Clear();
		}
	}

	void OnDestroy()
	{
		if (UpgradeEvent != null)
		{
			UpgradeEvent.GameAction -= PlayParticle;
		}
	}

	public void PlayParticle()
	{
		foreach (var ps in particleSystems)
		{
			ps.Play();
		}
		StartCoroutine(DisableAfterCompletion());
	}

	private IEnumerator DisableAfterCompletion()
	{
		bool anyPlaying;
		do
		{
			anyPlaying = false;
			foreach (var ps in particleSystems)
			{
				if (ps.isPlaying)
				{
					anyPlaying = true;
					break;
				}
			}
			yield return null;
		} while (anyPlaying);
	}
}
