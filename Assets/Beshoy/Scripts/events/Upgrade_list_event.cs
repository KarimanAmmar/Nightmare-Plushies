using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Event/UpgradeListEvent")]
public class Upgrade_list_event : MonoBehaviour
{
    private event System.Action<Upgrade[]> listeners;

    public void Raise(Upgrade[] waveNumber)
    {
        listeners?.Invoke(waveNumber);
    }

    public void RegisterListener(System.Action<Upgrade[]> listener)
    {
        listeners += listener;
    }

    public void UnregisterListener(System.Action<Upgrade[]> listener)
    {
        listeners -= listener;
    }
}

