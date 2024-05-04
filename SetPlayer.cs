using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SetPlayer : MonoBehaviour
{
    public InputField nameInputField;
    public Text displayText; 
    public int maxCharacterLimit = 12;

    private void Start()
    {
        string savedName = PlayerPrefs.GetString("PlayerName", "");
        if (!string.IsNullOrEmpty(savedName))
        {
            SceneManager.LoadScene("Stage1");
        }
        else
        {
            nameInputField.text = savedName;
        }
        nameInputField.characterLimit = maxCharacterLimit;
    }

    public void SaveAndGoToGame()
    {
        string playerName = nameInputField.text;

        if (string.IsNullOrEmpty(playerName))
        {
            displayText.text = "Nama pemain tidak boleh kosong!";
            Debug.LogError("Player name cannot be empty!");
            return;
        }

        if (playerName.Length > maxCharacterLimit)
        {
            displayText.text = "Nama pemain tidak boleh lebih dari" + maxCharacterLimit + " karakter!"; 
            Debug.LogError("Player name exceeds the character limit of " + maxCharacterLimit + " characters!");
            return;
        }


        PlayerPrefs.SetString("PlayerName", playerName);
        PlayerPrefs.Save();

        displayText.text = "Player name saved: " + playerName;
        Debug.Log("Player name saved: " + playerName);

        SceneManager.LoadScene("LoadingIntoStage1");
    }

    public void DeletePlayerName()
    {
        PlayerPrefs.DeleteKey("PlayerName");
        PlayerPrefs.Save(); 

        displayText.text = "Player name deleted successfully."; 
        Debug.Log("Player name deleted successfully.");

        SceneManager.LoadScene("SetPlayer");
    }
    
    public void BackToLobby()
    {
        SceneManager.LoadScene("Lobby");
    }
}
