using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlowsAttack : IAttackBehavior
{
	public void Attack(EnemyController enemy, Vector3 playerPosition)
	{
		Logging.Log("Performing blows attack");
	}
}
