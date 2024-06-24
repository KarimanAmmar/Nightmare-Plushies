using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackVibration : MonoBehaviour
{
	[SerializeField] private Float_event playerDamage;
	private void OnEnable()
	{
		playerDamage.RegisterListener(PlayerAttack);
	}
	private void OnDisable()
	{
		playerDamage.UnregisterListener(PlayerAttack);
	}
		void PlayerAttack(float amount)
	{
		// Your attack logic here

		// Vibrate the phone for 500 milliseconds
		Vibration.Vibrate(500);
	}
}
