using UnityEngine;

public class CharacterMovementManager : MonoBehaviour
{
	[SerializeField] VariableJoystick joystick;
	[SerializeField] CharacterController controller;
	[SerializeField] float movementSpeed;
	[SerializeField] Canvas inputCanvas;
	[SerializeField] private TransformEvent transformClosestEnemy;
	private bool isJoystick;
	private Vector3 defaultPosition;
	private Transform ClosestEnemy;

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

		if (ClosestEnemy != null)
		{
			Vector3 direction = ClosestEnemy.position - controller.transform.position;
			RotatePlayer(direction);
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

	void OnEnable()
	{
		transformClosestEnemy.RegisterListener(OnTransformEventRaised);
	}

	void OnDisable()
	{
		transformClosestEnemy.UnregisterListener(OnTransformEventRaised);
	}

	void OnTransformEventRaised(Transform transformReceived)
	{
		ClosestEnemy = transformReceived;
	}
}
