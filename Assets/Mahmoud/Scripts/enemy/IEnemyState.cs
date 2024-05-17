using UnityEngine;

public interface IEnemyState
{
	void EnterState(EnemyController enemy);
	void UpdateState(EnemyController enemy, Vector3 playerPosition);
	void ExitState(EnemyController enemy);
}
