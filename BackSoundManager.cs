using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BackSoundManager : MonoBehaviour
{
    public AudioSource backSound;
    public AudioClip backSoundAudio;
    public Slider volumeSlider;

    void Start()
    {
        volumeSlider.value = backSound.volume;
        backSound.clip = backSoundAudio;
        backSound.Play();
        Debug.Log("Scene loaded:");
        SceneManager.sceneLoaded += OnSceneLoaded; 
        // DontDestroyOnLoad(this.gameObject);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        
        // if (scene.name == "Lobby" || scene.name == "Stage2" || scene.name == "Goa" || scene.name == "Market" || scene.name == "KadesHome"|| scene.name == "ResidentHome")
        // {
            if (!backSound.isPlaying)
            {
                backSound.clip = backSoundAudio;
                backSound.Play();
            }
        // }
        // else
        // {
            if (backSound.isPlaying)
            {
                backSound.Stop();
            }
        // }
    }

    public void SetBackgroundVolume() 
    {
        backSound.volume = volumeSlider.value;
    }
}
