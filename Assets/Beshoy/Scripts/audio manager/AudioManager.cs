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

    /// <summary>
    /// for a poolable audio source
    /// </summary>
    [SerializeField] private GameObject SFXSource;
    [SerializeField] int poolSize;
    [SerializeField] GameObject parent;
    List<GameObject> pooledObjects;
    private bool MasterMute;

    private void Awake()
    {
        base.Awake();
        pooledObjects = new List<GameObject>();
        parent = this.gameObject;
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(SFXSource, parent.transform);
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }

    }
    public void PlySfx(AudioClip clip)
    {
        if (clip!=null)
        {
            SfxSource.PlayOneShot(clip);
            PlaYSoundEffects(clip);
            //AudioSource.PlayClipAtPoint(clip,);
        }
        
        //SfxSource.clip = clip;
        //SfxSource.Play();
    }
    public void PlaYSoundEffects(AudioClip clip)
    {
        StartCoroutine(PlaySound(clip));
    }
    IEnumerator PlaySound(AudioClip clip) 
    {
        Logging.Log("played");
        yield return new WaitForSeconds(clip.length);
        Logging.Log("finished");
    }  
    public void PlyMusic(AudioClip clip)
    {
        if (clip != null)
        {
            musicSource.PlayOneShot(clip);
        }
    }
    public void Mute()
    {
        MasterMute =!MasterMute;
        SfxSource.mute = !SfxSource.mute;
        musicSource.mute = !musicSource.mute;
    }
    public void Mute_sfx()
    {
        if (!MasterMute) 
        {
            SfxSource.mute = !SfxSource.mute;
        }
    }
    public void Mute_Music()
    {
        if (!MasterMute)
        {
            musicSource.mute = !musicSource.mute;
        }
    }
    public void Master_SetVolume(float volume) 
    {
        if (Mixer != null)
        {
            Mixer.SetFloat("MasterVolume",Mathf.Log10(volume)*20);
        }
    }
    public void Sfx_SetVolume(float volume)
    {
        if (Mixer != null)
        {
            Mixer.SetFloat("SfxVolume", Mathf.Log10(volume)*20);
        }
    }
    public void Music_SetVolume(float volume)
    {
        if (Mixer != null)
        {
            Mixer.SetFloat("MusicVolume", Mathf.Log10(volume)*20); 
        }
    }



}
