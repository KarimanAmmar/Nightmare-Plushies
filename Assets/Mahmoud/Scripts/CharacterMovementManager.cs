using UnityEngine;

public class CharacterMovementManager : MonoBehaviour
{
	[SerializeField] private VariableJoystick joystick;
	[SerializeField] private CharacterController controller;
	[SerializeField] private float movementSpeed;
	[SerializeField] private Canvas inputCanvas;
	[SerializeField] private bool isJoystick;
	private Vector3 defaultPosition;

	private void Start()
	{
		EnableJoystickInput();
		defaultPosition = joystick.transform.position;
	}

	public void EnableJoystickInput()
	{
		isJoystick = true;
		inputCanvas.gameObject.SetActive(true);
	}

	private void Update()
	{
		if (isJoystick)
		{
			var movementDirection = new Vector3(joystick.Direction.x, 0.0f, joystick.Direction.y);
			controller.SimpleMove(movementDirection * movementSpeed);
		}

		if (Input.touchCount > 0)
		{
			Touch touch = Input.GetTouch(0);
			if (touch.phase == TouchPhase.Ended)
			{
				joystick.transform.position = defaultPosition;
			}
			else if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved)
			{
				// Convert touch position to world space
				Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
				touchPosition.z = 0; // Ensure z-axis is same as joystick's

				// Move joystick to touch position
				joystick.transform.position = touchPosition;
			}
		}
	}
}
