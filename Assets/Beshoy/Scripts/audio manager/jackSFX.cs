using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jackSFX : MonoBehaviour
{
    [SerializeField] private GameEvent jumpSfxEvent;

    [SerializeField] private GameEvent exploadeEvent;
    [SerializeField] private AudioClip jumpClip;

    [SerializeField] private AudioSource exploadesource;
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
           AudioSource.PlayClipAtPoint(jumpClip,transform.position,0.7f);

        }
    }

    public void PlayExploadeClip()
    {
        if (exploadesource != null)
        {
            exploadesource.Play();
        }
    }
   
}
