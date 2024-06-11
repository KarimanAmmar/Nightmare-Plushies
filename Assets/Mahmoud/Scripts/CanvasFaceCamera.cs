using UnityEngine;

public class CanvasFaceCamera : MonoBehaviour
{
	// Reference to the camera
	private Camera cameraToFace;

	void Start()
	{
		// If no camera is set, use the main camera
		if (cameraToFace == null)
		{
			cameraToFace = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
		}
	}

	void LateUpdate()
	{
		// Make the canvas face the camera
		Vector3 direction = transform.position - cameraToFace.transform.position;
		transform.rotation = Quaternion.LookRotation(direction);
	}
}
