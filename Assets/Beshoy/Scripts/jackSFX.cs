using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jackSFX : MonoBehaviour
{
    [SerializeField] private GameEvent jumpSfxEvent;

    [SerializeField] private GameEvent exploadeEvent;
    [SerializeField] private AudioClip jumpClip;
    [SerializeField] private AudioClip exploadeClip;
    private void OnEnable()
    {
        jumpSfxEvent.GameAction += PlayJumpClip;
        exploadeEvent.GameAction += PlayExploadeClip;
    }
    private void OnDisable()
    {
        jumpSfxEvent.GameAction -= PlayJumpClip;
        exploadeEvent.GameAction -= PlayExploadeClip;

    }
    private void PlayJumpClip() 
    { 
        if (jumpClip != null)
        {
            AudioSource.PlayClipAtPoint(jumpClip,transform.position);
        }
    }

    private void PlayExploadeClip()
    {
        if (exploadeClip != null)
        {
            AudioManager.Instance.PlySfx(exploadeClip);
        }
    }
   
}
