using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Lobby : MonoBehaviour
{
    public Text versionText; 

    private void Start()
    {
        string gameVersion = Application.version;
        versionText.text = "Version: " + gameVersion;
    }

    public void DeletePlayerName()
    {
        PlayerPrefs.DeleteKey("PlayerName");
        PlayerPrefs.Save(); 

        Debug.Log("Player name deleted successfully.");

        SceneManager.LoadScene("SetPlayer");
    }
}
