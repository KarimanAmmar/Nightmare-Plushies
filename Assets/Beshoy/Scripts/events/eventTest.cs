using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] GameEvent upgradeTestEvent;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Comma))
        {
            upgradeTestEvent.GameAction.Invoke();
        }
    }
}
