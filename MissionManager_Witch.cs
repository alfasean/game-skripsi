using UnityEngine;
using UnityEngine.UI;

public class MissionManager_Witch : MonoBehaviour
{
    public Text missionText;
    private string InteractionWitchmissionKey = "InteractionsCount"; 
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
            PlayerPrefs.SetInt(InteractionWitchmissionKey, interactionsCount);
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
        interactionsCount = PlayerPrefs.GetInt(InteractionWitchmissionKey, 0);
    }

    public void ResetMission()
    {
        interactionsCount = 0;
        PlayerPrefs.DeleteKey(InteractionWitchmissionKey);
        SaveMissionProgress();
        UpdateMissionText();
        Debug.Log("Mission Reset");
    }
}
