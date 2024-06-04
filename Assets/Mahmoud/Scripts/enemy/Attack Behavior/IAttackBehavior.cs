using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttackBehavior
{
	void Attack(EnemyController enemy, Vector3 playerPosition);
}
  
