using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Event/floatEvent", order = 4)]
public class Float_event : ScriptableObject
{
    private event System.Action<float> listeners;

    public void Raise(float value)
    {
        listeners?.Invoke(value);
    }

    public void RegisterListener(System.Action<float> listener)
    {
        listeners += listener;
    }

    public void UnregisterListener(System.Action<float> listener)
    {
        listeners -= listener;
    }
}
