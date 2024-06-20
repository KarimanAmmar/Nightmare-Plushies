using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jackSFX : MonoBehaviour
{
    [SerializeField] private GameEvent jumpSfxEvent;

    [SerializeField] private GameEvent exploadeEvent;
    [SerializeField] private AudioSource jumpClip;

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
          jumpClip.Play();

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
