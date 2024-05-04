using UnityEngine;
using UnityEngine.UI;

public class MissionManager_TakeOwl : MonoBehaviour
{
    public GameObject completeMission;
    private string owlMissionCompletedKey = "OwlMissionCompleted";

    void Start()
    {
        LoadOwlMissionProgress();
        UpdateOwlMissionText();
        // completeMission.SetActive(false);
    }

    public void TakeOwl()
    {
        PlayerPrefs.SetInt(owlMissionCompletedKey, 1);
        UpdateOwlMissionText();
    }

    private void UpdateOwlMissionText()
    {
        bool owlMissionCompleted = PlayerPrefs.GetInt(owlMissionCompletedKey, 0) == 1;
        if (owlMissionCompleted)
        {
            completeMission.SetActive(true);
        }
        else
        {
            completeMission.SetActive(false);
        }
    }

    private void SaveOwlMissionProgress()
    {
        PlayerPrefs.Save();
    }

    private void LoadOwlMissionProgress()
    {
        
    }

    public void ResetOwlMission()
    {
        PlayerPrefs.DeleteKey(owlMissionCompletedKey);
        SaveOwlMissionProgress();
        UpdateOwlMissionText();
        Debug.Log("Owl Mission Reset");
    }
}
