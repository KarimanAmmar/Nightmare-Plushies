using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire_Slash_animation : MonoBehaviour
{
	[SerializeField] private GameEvent slashing;

	private void Start_Slashing()
	{
		slashing.GameAction?.Invoke();
	}
}
