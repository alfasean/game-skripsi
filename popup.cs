using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Popup : MonoBehaviour 
{
    public GameObject PopUp;
    private string sceneToLoad ="Lobby";
    private Button button;

    void Start()
    {
        button = GetComponent<Button>();

        if (button != null)
        {
            button.onClick.AddListener(TogglePopup);
        }
        else
        {
            Debug.LogError("Button component not found on the GameObject!");
        }

        PopUp.SetActive(false);
    }

    public void TogglePopup()
    {
        if (PopUp.activeSelf)
        {
            PopUp.SetActive(false);
            return;
        }

        PopUp.SetActive(true);
    }
    public void ClosePopUp()
    {
        PopUp.SetActive(false);
    }

    public void BackToLobby() {
        SceneManager.LoadScene(sceneToLoad);
    }

    public void LeaveGame() {
        Application.Quit();
    }
}
