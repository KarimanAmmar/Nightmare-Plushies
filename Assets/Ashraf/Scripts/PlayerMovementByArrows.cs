using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementByArrows : MonoBehaviour
{
	public float speed = 5f;
	public float rotationSpeed = 100f;
	[SerializeField] private CharacterController controller;

	void Update()
	{
		// Get movement input
		float moveVertical = Input.GetAxis("Vertical");
		Vector3 movement = transform.forward * moveVertical;

		if (movement.magnitude > 1)
		{
			movement.Normalize();
		}

		controller.Move(movement * speed * Time.deltaTime);

		// Get rotation input
		float rotateHorizontal = Input.GetAxis("Horizontal");
		float rotation = rotateHorizontal * rotationSpeed * Time.deltaTime;

		// Rotate player around y-axis
		transform.Rotate(0, rotation, 0);
	}
}

