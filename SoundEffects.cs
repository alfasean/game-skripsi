using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundEffects : MonoBehaviour 
{
    public AudioSource audio1, audio2, audio3, audio4;
    public AudioClip sfx1;
    public Slider volumeSlider;

    // private void Awake()
    // {
    //     DontDestroyOnLoad(this.gameObject);
    // }

    void Start()
    {
        volumeSlider.value = audio1.volume;
    }

    public void PlaySFX()
    {
        audio1.clip = sfx1;
        audio2.clip = sfx1;
        audio3.clip = sfx1;
        audio4.clip = sfx1;
        audio1.Play();
        audio2.Play();
        audio3.Play();
        audio4.Play();
    }

    public void SetBackgroundVolume()
    {
        audio1.volume = volumeSlider.value;
        audio2.volume = volumeSlider.value;
        audio3.volume = volumeSlider.value;
        audio4.volume = volumeSlider.value;
    }
}
