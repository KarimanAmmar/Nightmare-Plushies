using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    [SerializeField] Animator DoorAnimator;
    [SerializeField] string AnimName;
    [SerializeField] AudioClip opensfx;
    private bool IsOpen = false;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(GameConstant.PlayerTag)&&!IsOpen)
        {
            AudioManager.Instance.PlySfx(opensfx);
            DoorAnimator.SetBool(AnimName, true);
            IsOpen = true;
            
        }
    }
}
