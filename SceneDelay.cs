using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneDelay : MonoBehaviour
{
    public string nextSceneName; 

    void Start()
    {
        StartCoroutine(DelayAndLoadNextScene());
    }

    IEnumerator DelayAndLoadNextScene()
    {
        yield return new WaitForSeconds(7f);
        SceneManager.LoadScene(nextSceneName);
    }
}
