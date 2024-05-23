using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "upgrades/UpgradeList Event", order = 6)]
public class Upgrade_list_event : ScriptableObject
{
    private event System.Action<Upgrade[]> Listeners;

    public void Raise(Upgrade[] waveNumber)
    {
        Listeners?.Invoke(waveNumber);
    }

    public void RegisterListener(System.Action<Upgrade[]> listener)
    {
        Listeners += listener;
    }

    public void UnregisterListener(System.Action<Upgrade[]> listener)
    {
        Listeners -= listener;
    }
}

