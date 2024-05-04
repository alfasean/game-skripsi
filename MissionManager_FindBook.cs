using UnityEngine;
using UnityEngine.UI;

public class MissionManager_FindBook : MonoBehaviour
{
    public Text bookMissionText;
    private string bookMissionKey = "BookCount";
    public GameObject CompleteMission;
    private string bookMissionCompletedKey = "BookMissionCompleted";

    void Start()
    {
        LoadBookMissionProgress();
        UpdateBookMissionText();
        // CompleteMission.SetActive(false);
    }

    public void FindBook()
    {
        PlayerPrefs.SetInt(bookMissionKey, 1);
        PlayerPrefs.SetInt(bookMissionCompletedKey, 1); 
        UpdateBookMissionText();
    }

    private void UpdateBookMissionText()
    {
        int bookCount = PlayerPrefs.GetInt(bookMissionKey, 0);
        if (bookCount >= 1)
        {
            CompleteMission.SetActive(true);
        }
        else
        {
            CompleteMission.SetActive(false);
        }
    }

    private void SaveBookMissionProgress()
    {
        PlayerPrefs.Save();
    }

    private void LoadBookMissionProgress()
    {
        
    }

    public void ResetBookMission()
    {
        PlayerPrefs.DeleteKey(bookMissionKey);
        SaveBookMissionProgress();
        PlayerPrefs.DeleteKey(bookMissionCompletedKey);
        UpdateBookMissionText();
        Debug.Log("Mission Reset");
    }
}
