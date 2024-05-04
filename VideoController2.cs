using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class VideoController2 : MonoBehaviour
{
    public VideoPlayer videoPlayer; 
    public string nextSceneName; 
    private const string videoPlayedKey2 = "videoPlayed2"; 

    void Start()
    {
        videoPlayer.loopPointReached += OnVideoFinished;

        if (!PlayerPrefs.HasKey(videoPlayedKey2))
        {
            videoPlayer.Play();
        }
        else
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }

    void OnVideoFinished(VideoPlayer vp)
    {
        PlayerPrefs.SetInt(videoPlayedKey2, 1);
        PlayerPrefs.Save();

        SceneManager.LoadScene(nextSceneName);
    }
}
