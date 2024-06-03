using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class AttackingState : IEnemyState
{
	private IAttackBehavior attackBehavior;

	public AttackingState(IAttackBehavior initialAttackBehavior)
	{
		this.attackBehavior = initialAttackBehavior;
	}

	public void SetAttackBehavior(IAttackBehavior newAttackBehavior)
	{
		this.attackBehavior = newAttackBehavior;
	}

	public void EnterState(EnemyController enemy)
	{
		// Any initialization logic for entering the attacking state
	}

	public void ExitState(EnemyController enemy)
	{
		// Any cleanup logic for exiting the attacking state
	}

	public void UpdateState(EnemyController enemy, Vector3 playerPosition)
	{
		attackBehavior.Attack(enemy, playerPosition);
	}
}

