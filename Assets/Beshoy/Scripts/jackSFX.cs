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
    public void PlayJumpClip() 
    { 
        if (jumpClip != null)
        {
           AudioSource.PlayClipAtPoint(jumpClip,transform.position,1);

        }
    }

    public void PlayExploadeClip()
    {
        if (exploadeClip != null)
        {
            AudioManager.Instance.PlySfx(exploadeClip);
        }
    }
   
}
