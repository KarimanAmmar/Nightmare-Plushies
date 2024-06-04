using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
/// <summary>
/// todo:
/// handle sounds from other scripts (possibly using scriptable object);
/// using api of the audio mixer for future options menu(UI);
/// possible ideas:
/// make the manager A SINGLETON &and add it to don destroy onload;
/// check every scene if there is another audio manager 
/// to make sure that there is only one manager in every scene
/// create a script a script to handles UI for the setting window;
/// max sounds:12;
/// </summary>

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] private AudioMixer Mixer;
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource SfxSource;
    
    public void PlySfx(AudioClip clip)
    {
        
        SfxSource.PlayOneShot(clip);
    }
    public void PlyMusic(AudioClip clip)
    {
        musicSource.PlayOneShot(clip);
    }
    public void Mute()
    {
        
    }
}
