using UnityEngine;

public class CharacterMovementManager : MonoBehaviour
{
	[SerializeField] VariableJoystick joystick;
	[SerializeField] CharacterController controller;
	[SerializeField] float movementSpeed;
	[SerializeField] Canvas inputCanvas;
	bool isJoystick;
	Vector3 defaultPosition;

	void Start()
	{
		EnableJoystickInput();
		defaultPosition = joystick.transform.position;
	}

	void EnableJoystickInput()
	{
		isJoystick = true;
		inputCanvas.gameObject.SetActive(true);
	}

	void Update()
	{
		if (isJoystick)
		{
			Vector3 movementDirection = new Vector3(joystick.Direction.x, 0.0f, joystick.Direction.y);
			MovePlayer(movementDirection);
			RotatePlayer(movementDirection);
		}
	}

	public void HandleTouchInput(Vector2 touchPosition)
	{
		Vector3 worldTouchPosition = Camera.main.ScreenToWorldPoint(touchPosition);
		worldTouchPosition.z = 0;

		Vector3 movementDirection = (worldTouchPosition - transform.position).normalized;
		MovePlayer(movementDirection);
		RotatePlayer(movementDirection);

		joystick.transform.position = worldTouchPosition;
	}

	void MovePlayer(Vector3 direction)
	{
		controller.SimpleMove(direction * movementSpeed);
	}

	void RotatePlayer(Vector3 direction)
	{
		if (direction != Vector3.zero)
		{
			Quaternion targetRotation = Quaternion.LookRotation(direction);
			controller.transform.rotation = targetRotation;
		}
	}
}
