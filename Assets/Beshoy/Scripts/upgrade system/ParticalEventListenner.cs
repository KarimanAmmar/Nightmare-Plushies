using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticalEventListenner : MonoBehaviour
{
    [SerializeField] private GameEvent UpgradeEffect;

    private void OnEnable()
    {
        UpgradeEffect.GameAction +=PlayEffect;
    }

    private void OnDisable()
    {
        UpgradeEffect.GameAction -=PlayEffect;
    }

    private void PlayEffect()
    {
        Logging.Log("partical played");
    }
}
