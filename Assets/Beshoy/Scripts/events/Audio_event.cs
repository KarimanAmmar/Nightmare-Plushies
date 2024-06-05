using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Event/audioEvent", order = 6)]
public class Audio_event : ScriptableObject
{
    private event System.Action<AudioClip> listeners;

    public void Raise(AudioClip waveNumber)
    {
        listeners?.Invoke(waveNumber);
    }

    public void RegisterListener(System.Action<AudioClip> listener)
    {
        listeners += listener;
    }

    public void UnregisterListener(System.Action<AudioClip> listener)
    {
        listeners -= listener;

    }
}
