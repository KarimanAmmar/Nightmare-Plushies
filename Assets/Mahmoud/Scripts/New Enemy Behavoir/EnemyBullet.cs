using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
	[SerializeField] private Float_event damage_event;
	[SerializeField] private float damageValue;
	[SerializeField] private float lifetime = 5f;
	private Transform parentTransform;

	// Property to set parentTransform
	public Transform ParentTransform
	{
		get { return parentTransform; }
		set { parentTransform = value; }
	}

	private void OnEnable()
	{
		Invoke("Deactivate", lifetime);
	}

	private void OnDisable()
	{
		CancelInvoke();
		ResetTransform();

		if (gameObject.activeSelf && parentTransform != null)
		{
			transform.parent = parentTransform; 
		}
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
			gameObject.SetActive(false);
		}
	}

	private void ResetTransform()
	{
		transform.localPosition = Vector3.zero;
		transform.localRotation = Quaternion.identity;
	}
}
