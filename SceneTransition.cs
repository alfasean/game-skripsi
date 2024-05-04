using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public string sceneToLoad;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Trigger entered by: " + other.tag);

        if (other.CompareTag("Player") && !other.isTrigger)
        {
            PlayerPrefs.SetString("PreviousScene", SceneManager.GetActiveScene().name);
            
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
