using UnityEngine;

[CreateAssetMenu(menuName = "Event/TransformEvent", order = 2)]
public class TransformEvent : ScriptableObject
{
	private event System.Action<Transform> listeners;

	public void Raise(Transform transformToSend)
	{
		listeners?.Invoke(transformToSend);
	}

	public void RegisterListener(System.Action<Transform> listener)
	{
		listeners += listener;
	}

	public void UnregisterListener(System.Action<Transform> listener)
	{
		listeners -= listener;
	}
}
