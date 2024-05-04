using UnityEngine;
using UnityEngine.UI;

public class MissionManager_InteractNPC : MonoBehaviour
{
    public Text missionText;
    private string InteractionNPCmissionKey = "InteractionsCount"; 
    private int interactionsCount = 0; 
    public GameObject CompleteMission;

    void Start()
    {
        LoadMissionProgress();
        UpdateMissionText();
    }

    public void InteractWithNPC()
    {
        if (interactionsCount < 1)
        {
            interactionsCount++;
            PlayerPrefs.SetInt(InteractionNPCmissionKey, interactionsCount);
            SaveMissionProgress();
            UpdateMissionText();
        }
    }

    private void UpdateMissionText()
    {
        if (interactionsCount >= 1)
        {
            CompleteMission.SetActive(true);
        }
        else
        {
            CompleteMission.SetActive(false);
        }
    }

    private void SaveMissionProgress()
    {
        PlayerPrefs.Save();
    }

    private void LoadMissionProgress()
    {
        interactionsCount = PlayerPrefs.GetInt(InteractionNPCmissionKey, 0);
    }

    public void ResetMission()
    {
        interactionsCount = 0;
        PlayerPrefs.DeleteKey(InteractionNPCmissionKey);
        SaveMissionProgress();
        UpdateMissionText();
        Debug.Log("Mission Reset");
    }
}
