using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skelton_sfx : MonoBehaviour
{
    [SerializeField] private AudioSource walkSound;
    [SerializeField] private AudioClip playSound;

    
    public void PlayWalkClip()
    {
        if (walkSound != null)
        {
            //Logging.Log("played");
            walkSound.Play();
            //AudioSource.PlayClipAtPoint(playSound, transform.position);

        }
    }
}
