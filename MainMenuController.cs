using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public Button newGameButton;
    public Button continueButton;
    public GameObject aboutObject;

    private void Start()
    {
        continueButton.gameObject.SetActive(PlayerPrefs.HasKey("HasSavedGame"));
        aboutObject.SetActive(false);
    }

    public void StartNewGame()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("HasSavedGame", 1);
        SceneManager.LoadScene("LoadingIntoStage1");
    }

    public void ContinueGame()
    {
        SceneManager.LoadScene("LoadingGoa");
    }

    public void OpenAbout()
    {
        aboutObject.SetActive(true);
    }

    public void CloseAbout()
    {
        aboutObject.SetActive(false);
    }
}
