using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaAndWaveManagerActive : MonoBehaviour
{
	[SerializeField] GameEvent startNextWave;
	[SerializeField] private float TimeBeforeDisable = 3f;
	[Header("Arena 1")]
	[SerializeField] GameEvent Arena1Complete;
	[SerializeField] GameObject arena1;
	[SerializeField] GameObject waveManager1;
	[SerializeField] bool Arena1Finished;

	[Header("Arena 2")]
	[SerializeField] GameEvent Arena2Complete;
	[SerializeField] GameObject arena2;
	[SerializeField] GameObject waveManager2;
	[SerializeField] bool Arena2Finished;

	[Header("Arena 3")]
	[SerializeField] GameEvent Arena3Complete;
	[SerializeField] GameObject arena3;
	[SerializeField] GameObject waveManager3;
	[SerializeField] bool Arena3Finished;

	private void OnEnable()
	{
		Arena1Complete.GameAction += OnArena1Complete;
		Arena2Complete.GameAction += OnArena2Complete;
		startNextWave.GameAction += OnArena2Start;
		startNextWave.GameAction += OnArena3Start;
	}

	private void OnDisable()
	{
		Arena1Complete.GameAction -= OnArena1Complete;
		Arena2Complete.GameAction -= OnArena2Complete;
		startNextWave.GameAction -= OnArena2Start;
		startNextWave.GameAction -= OnArena3Start;
	}

	private void OnArena1Complete()
	{
		Arena1Finished = true;
		arena2.SetActive(true);
		waveManager2.SetActive(true);
	}

	private void OnArena2Start()
	{
		/*if (Arena1Finished)
		{
			arena1.SetActive(false);
			waveManager1.SetActive(false);
			
		}*/
		StartCoroutine(WaitAndProceed(TimeBeforeDisable, () =>
		{
			if (Arena1Finished)
			{
				arena1.SetActive(false);
				waveManager1.SetActive(false);

			}
		}));
	}

	private void OnArena2Complete()
	{
		Arena2Finished = true;
		arena3.SetActive(true);
		waveManager3.SetActive(true);
	}

	private void OnArena3Start()
	{
		StartCoroutine(WaitAndProceed(TimeBeforeDisable, () =>
		{
			if (Arena2Finished)
			{
				arena2.SetActive(false);
				waveManager2.SetActive(false);

			}
		}));
	}

	private void OnArena3Complete()
	{
		Arena3Finished = true;
	}

	private IEnumerator WaitAndProceed(float delay, System.Action action)
	{
		yield return new WaitForSeconds(delay);
		action?.Invoke();
	}
}
