using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    [SerializeField] Animator DoorAnimator;
    [SerializeField] string AnimName;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(GameConstant.PlayerTag))
        {
            DoorAnimator.SetBool(AnimName, true);
        }
    }
}
