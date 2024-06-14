using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    [SerializeField] AudioClip opensfx;
    [SerializeField] Animator DoorAnimator;
    [SerializeField] int_Event waveEvent;
    [SerializeField] Collider doorCollider;
    [SerializeField] string AnimName;
    [SerializeField] int waveEventNum;
    private bool IsOpen = false;

    private void OnEnable()
    {
        if(waveEvent != null)
        {
            waveEvent.RegisterListener(enebleCollider);
        }
    }
    private void OnDisable()
    {
        if (waveEvent != null)
        {
            waveEvent.UnregisterListener(enebleCollider);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(GameConstant.PlayerTag)&&!IsOpen)
        {
            AudioManager.Instance.PlySfx(opensfx);
            DoorAnimator.SetBool(AnimName, true);
            IsOpen = true;            
        }
    }
    void enebleCollider(int wavenum)
    {
        if(wavenum == waveEventNum && doorCollider != null)
        {
            doorCollider.enabled = true;
        }
    }
}
