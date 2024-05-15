using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
	[SerializeField] private float moveSpeed = 8f;

	private Vector2 moveVector;
	private CharacterController characterController;

	private void Awake()
	{
		characterController = GetComponent<CharacterController>();
	}

	

	private void Update()
	{
		MovePlayer();
	}

	public void MovePlayer()
	{
		Vector3 movement = new Vector3(moveVector.x, 0f, moveVector.y);
		characterController.Move(moveSpeed * movement * Time.fixedDeltaTime);
		if (movement != Vector3.zero)
		{
			transform.rotation = Quaternion.LookRotation(movement);
		}
	}
	public void InputPlayer(InputAction.CallbackContext context)
	{
		moveVector = context.ReadValue<Vector2>().normalized;
	}
}
