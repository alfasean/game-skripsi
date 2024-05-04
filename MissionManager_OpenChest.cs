using UnityEngine;
using UnityEngine.UI;

public class MissionManager_OpenChest : MonoBehaviour
{
    public GameObject completeMission; 
    private string chestMissionCompletedKey = "ChestMissionCompleted"; 

    void Start()
    {
        
        UpdateChestMission();
    }

    public void CompleteChestMission()
    {
        
        PlayerPrefs.SetInt(chestMissionCompletedKey, 1);
        
        UpdateChestMission();
    }

    private void UpdateChestMission()
    {
        
        if (PlayerPrefs.HasKey(chestMissionCompletedKey))
        {
            
            completeMission.SetActive(PlayerPrefs.GetInt(chestMissionCompletedKey) == 1);
        }
        else
        {
            
            PlayerPrefs.SetInt(chestMissionCompletedKey, 0);
            PlayerPrefs.Save();
            
            completeMission.SetActive(false);
        }
    }

    public void ResetChestMission()
    {
        
        PlayerPrefs.DeleteKey(chestMissionCompletedKey);
        PlayerPrefs.Save();
        
        UpdateChestMission();
        Debug.Log("Chest Mission Reset");
    }
}
