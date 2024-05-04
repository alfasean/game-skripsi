using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionMarketToStage2 : MonoBehaviour
{
    private string sceneToLoad1 = "LoadingBackToStage2";
    private string sceneToLoad2 = "TalkPrajuritAndKapten0";
    private string owlMissionCompletedKey = "OwlMissionCompleted"; 
    private bool isOwlComplete = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        bool isOwlComplete = PlayerPrefs.GetInt(owlMissionCompletedKey, 0) == 1;;

        if (other.CompareTag("Player") && !other.isTrigger && !isOwlComplete)
        {
            PlayerPrefs.SetString("PreviousScene", SceneManager.GetActiveScene().name);
            
            SceneManager.LoadScene(sceneToLoad1);
        }
        if (other.CompareTag("Player") && !other.isTrigger && isOwlComplete)
        {
            PlayerPrefs.SetString("PreviousScene", SceneManager.GetActiveScene().name);
            
            SceneManager.LoadScene(sceneToLoad2);
        }
    }
}
