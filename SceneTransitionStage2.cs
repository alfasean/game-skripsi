using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionStage2 : MonoBehaviour
{
    public string sceneToLoad;
    private string chestOpenedKey = "swordandChest";

    private void OnTriggerEnter2D(Collider2D other)
    {
        bool isChestOpened = PlayerPrefs.GetInt(chestOpenedKey, 0) == 1;
        if (other.CompareTag("Player") && !other.isTrigger && isChestOpened)
        {
            PlayerPrefs.SetString("PreviousScene", SceneManager.GetActiveScene().name);
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
