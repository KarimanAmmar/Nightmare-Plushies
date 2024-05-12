using UnityEngine;

public class CharacterMovementManager : MonoBehaviour
{
	public VariableJoystick joystick;
	public CharacterController controller;
	public float movementSpeed;
	public Canvas inputCanvas;
	public bool isJoystick;
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

				Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
				touchPosition.z = 0; 

				joystick.transform.position = touchPosition;
			}
		}
	}
}