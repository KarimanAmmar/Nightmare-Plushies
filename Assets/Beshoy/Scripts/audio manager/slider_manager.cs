using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class slider_manager : MonoBehaviour
{
    [SerializeField] private Slider Master_slider;
    [SerializeField] private Slider Sfx_slider;
    [SerializeField] private Slider Music_slider;

    private void Start()
    {
        Master_Setslider();
        Sfx_Setslider();
        Music_Setslider();  
    }
    public void Master_Setslider()
    {
        float volume = Master_slider.value;
        AudioManager.Instance.Master_SetVolume(volume);
    }
    public void Sfx_Setslider()
    {
        float volume = Sfx_slider.value;
        AudioManager.Instance.Sfx_SetVolume(volume);
    }
    public void Music_Setslider()
    {
        float volume = Music_slider.value;
        AudioManager.Instance.Music_SetVolume(volume);
    }
}
