using UnityEngine;

public class CharacterMovementManager : MonoBehaviour
{
	
	[SerializeField] VariableJoystick joystick;
	[SerializeField] CharacterController controller;
	[SerializeField] float defaultSpeed;
	private float movementSpeed;
	[SerializeField]  Animator animatorPlayer;
	[SerializeField] private string moveAnimation = "Moving";
	[SerializeField]  private bool isSlashFire = false;
	[SerializeField] Canvas inputCanvas;
	[SerializeField] private TransformEvent transformClosestEnemy;
	[SerializeField] float rotationSpeed;
	[SerializeField] GameEvent Floating;
	private bool isJoystick;
	private Vector3 defaultPosition;
	private Transform closestEnemy;
	private bool isLerpingToEnemy = false;
	public bool IsSlashFire { get => isSlashFire; set => isSlashFire = value; }

	void Start()
	{
		movementSpeed = defaultSpeed;
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
		if (isJoystick && !IsSlashFire)
		{
			
			Vector3 movementDirection = new Vector3(-joystick.Direction.x, 0.0f, -joystick.Direction.y);
			
			animatorPlayer.SetInteger(moveAnimation, movementDirection != Vector3.zero ? 1 : 0);

			// Move the player
			MovePlayer(movementDirection);

			if (!isLerpingToEnemy)
			{
				RotatePlayerLerp(movementDirection, Time.deltaTime * rotationSpeed);
			}
		}

		if (closestEnemy != null && !IsSlashFire)
		{
			Vector3 direction = (closestEnemy.position - controller.transform.position).normalized;
			RotatePlayerToEnemy(direction);
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
		Vector3 newPosition = controller.transform.position + new Vector3(direction.x, 0.0f, direction.z) * movementSpeed * Time.deltaTime;
		controller.Move(newPosition - controller.transform.position);
	}


	void RotatePlayer(Vector3 direction)
	{
		if (direction != Vector3.zero)
		{
			Quaternion targetRotation = Quaternion.LookRotation(direction);
			targetRotation.eulerAngles = new Vector3(0, targetRotation.eulerAngles.y, 0);
			controller.transform.rotation = targetRotation;
		}
	}

	void RotatePlayerToEnemy(Vector3 direction)
	{
		if (direction != Vector3.zero)
		{
			Quaternion targetRotation = Quaternion.LookRotation(direction);
			targetRotation.eulerAngles = new Vector3(0, targetRotation.eulerAngles.y, 0);
			if (!isLerpingToEnemy)
			{
				controller.transform.rotation = targetRotation;
			}
			else
			{
				controller.transform.rotation = Quaternion.Lerp(controller.transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
			}
		}
	}

	void RotatePlayerLerp(Vector3 direction, float lerpSpeed)
	{
		if (direction != Vector3.zero)
		{
			Quaternion targetRotation = Quaternion.LookRotation(direction);
			targetRotation.eulerAngles = new Vector3(0, targetRotation.eulerAngles.y, 0);
			controller.transform.rotation = Quaternion.Lerp(controller.transform.rotation, targetRotation, lerpSpeed);
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
		closestEnemy = transformReceived;
		if (closestEnemy != null)
		{
			isLerpingToEnemy = true;
		}
		else
		{
			isLerpingToEnemy = false;
		}
	}

	public void UpgradeSpeed(float newSpeed)
	{
		movementSpeed += (newSpeed/ 100)*movementSpeed;
	}
}