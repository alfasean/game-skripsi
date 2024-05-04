using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionGoaToStage1 : MonoBehaviour
{
    private string sceneToLoad1 = "LoadingGoaIntoStage1";
    private string sceneToLoad2 = "TalkKades";
    private string chestOpenedKey = "swordandChest";
    private bool isChestOpened = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        bool isChestOpened = PlayerPrefs.GetInt(chestOpenedKey, 0) == 1;;

        if (other.CompareTag("Player") && !other.isTrigger && !isChestOpened)
        {
            PlayerPrefs.SetString("PreviousScene", SceneManager.GetActiveScene().name);
            
            SceneManager.LoadScene(sceneToLoad1);
        }
        if (other.CompareTag("Player") && !other.isTrigger && isChestOpened)
        {
            PlayerPrefs.SetString("PreviousScene", SceneManager.GetActiveScene().name);
            
            SceneManager.LoadScene(sceneToLoad2);
        }
    }
}
