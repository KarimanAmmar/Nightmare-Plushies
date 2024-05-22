using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Event/intEvent", order = 2)]
public class int_Event : ScriptableObject
{
    private event System.Action<int> listeners;

    public void Raise(int waveNumber)
    {
        listeners?.Invoke(waveNumber);
    }

    public void RegisterListener(System.Action<int> listener)
    {
        listeners += listener;
    }

    public void UnregisterListener(System.Action<int> listener)
    {
        listeners -= listener;
    }
}
