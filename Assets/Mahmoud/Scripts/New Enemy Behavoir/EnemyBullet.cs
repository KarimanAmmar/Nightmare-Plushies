using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
	[SerializeField] private Float_event damage_event;
	[SerializeField] private float damageValue;
	[SerializeField] private float lifetime = 5f;

	private void OnEnable()
	{
		Invoke("Deactivate", lifetime);
	}

	private void OnDisable()
	{
		CancelInvoke();
		ResetTransform();
	}

	void Deactivate()
	{
		ResetTransform();
		gameObject.SetActive(false);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.layer == GameConstant.PlayerLayer)
		{
			damage_event.Raise(damageValue);
			this.gameObject.SetActive(false);
		}
	}

	private void ResetTransform()
	{
		transform.localPosition = Vector3.zero;
		transform.localRotation = Quaternion.identity;
	}
}
