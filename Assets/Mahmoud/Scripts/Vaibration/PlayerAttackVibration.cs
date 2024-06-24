using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CandyCoded.HapticFeedback;
public class PlayerAttackVibration : MonoBehaviour
{
	[SerializeField] private GameEvent playerDamage;
	private void OnEnable()
	{
		playerDamage.GameAction += PlayerAttack;
	}
	private void OnDisable()
	{
		playerDamage.GameAction -= PlayerAttack;
	}
	void PlayerAttack()
	{
		Debug.Log("Player Attack");
		HapticFeedback.HeavyFeedback();
	}
}
