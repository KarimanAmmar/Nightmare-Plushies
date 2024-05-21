using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class leveltest : MonoBehaviour
{
    [SerializeField] GameEvent levelcomp;

    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Space))
        {
            levelcomp.GameAction?.Invoke();
        }
    }
}
