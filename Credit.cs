using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class Credit : MonoBehaviour
{
    public VideoPlayer videoPlayer; 
    public string nextSceneName; 

    void Start()
    {
        videoPlayer.loopPointReached += OnVideoFinished;
    }

    void OnVideoFinished(VideoPlayer vp)
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(nextSceneName);
    }
}
