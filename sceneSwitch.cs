using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class sceneSwitch : MonoBehaviour
{
    public void LoadToScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
